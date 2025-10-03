using AcademyIO.Core.Messages.Integration;
using AcademyIO.MessageBus;
using AcademyIO.WebAPI.Core.Controllers;
using AcademyIO.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static AcademyIO.Auth.API.Models.UserViewModel;


namespace AcademyIO.Auth.API.Controllers
{
    [Route("api/auth")]

    public class AuthController : MainController
    {
        private readonly IMessageBus _bus;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public AuthController(
            JwtSettings jwtSettings,
            SignInManager<IdentityUser<Guid>> signInManager,
            UserManager<IdentityUser<Guid>> userManager,
            IMessageBus bus)
        {
            _jwtSettings = jwtSettings;
            _signInManager = signInManager;
            _userManager = userManager;
            _bus = bus;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser<Guid>
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await RegisterUserIdentity(registerUser, user);
            if (!result.Succeeded)
                return CustomResponse();

            var customerResult = await RegisterUser(registerUser);

            if (!customerResult.ValidationResult.IsValid)
                return CustomResponse(customerResult.ValidationResult);

            var token = await GetJwt(registerUser.Email!);
            return CustomResponse(token);
        }


        private async Task<IdentityResult> RegisterUserIdentity(RegisterUserViewModel registerUser, IdentityUser<Guid> user)
        {
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) AddErrorToStack(error.Description);
                return result;
            }

            await _userManager.AddToRoleAsync(user, registerUser.IsAdmin ? "ADMIN" : "STUDENT");

            return result;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<LoginResponseViewModel>> Login(LoginUserViewModel userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password,
                false, true);

            if (result.Succeeded)
            {
                var token = await GetJwt(userLogin.Email!);
                return CustomResponse(token);
            }

            if (result.IsLockedOut)
            {
                AddErrorToStack("User temporary blocked. Too many tries.");
                return CustomResponse();
            }

            AddErrorToStack("User or Password incorrect");
            return CustomResponse();
        }

        private async Task<ResponseMessage> RegisterUser(RegisterUserViewModel registerUser)
        {
            var user = await _userManager.FindByEmailAsync(registerUser.Email);
            ArgumentNullException.ThrowIfNull(user);

            var userRegistered = new UserRegisteredIntegrationEvent(user.Id, registerUser.FirstName, registerUser.LastName, registerUser.Email, registerUser.DateOfBirth, registerUser.IsAdmin);

            try
            {
                return await _bus.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegistered);
            }
            catch (Exception)
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        //TODO Fazer referesh?
        //[HttpPost("refresh-token")]
        //public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        //{
        //    if (string.IsNullOrEmpty(refreshToken))
        //    {
        //        AddErrorToStack("Invalid Refresh Token");
        //        return CustomResponse();
        //    }

        //    var token = await _jwtBuilder.ValidateRefreshToken(refreshToken);

        //    if (!token.IsValid)
        //    {
        //        AddErrorToStack("Expired Refresh Token");
        //        return CustomResponse();
        //    }

        //    var jwt = await _jwtBuilder
        //        .WithUserId(token.UserId)
        //        .WithJwtClaims()
        //        .WithUserClaims()
        //        .WithUserRoles()
        //        .WithRefreshToken()
        //        .BuildUserResponse();

        //    return CustomResponse(jwt);
        //}

        private async Task<LoginResponseViewModel> GetJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                userClaims.Add(new Claim(ClaimTypes.Role, role));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = signingConf,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(_jwtSettings.ExpirationHours)
            });

            var encodedJwt = handler.WriteToken(securityToken);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedJwt,
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Claims = userClaims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
         => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }

}
