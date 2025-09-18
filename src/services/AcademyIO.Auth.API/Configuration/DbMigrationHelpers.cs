using AcademyIO.Auth.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Auth.API.Configuration
{
    public static class DbMigrationHelpers
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            EnsureSeedData(app).Wait();
        }

        public static async Task EnsureSeedData(WebApplication application)
        {
            var service = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(service);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var contextIdentity = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                await contextIdentity.Database.EnsureDeletedAsync();

                await contextIdentity.Database.MigrateAsync();
                await SeedUsersAndRoles(contextIdentity);

            }

        }

        private static async Task SeedUsersAndRoles(ApplicationDbContext contextIdentity)
        {
            if (contextIdentity.Users.Any()) return;

            #region ADMIN SEED
            var ADMIN_ROLE_ID = Guid.NewGuid();
            await contextIdentity.Roles.AddAsync(new IdentityRole<Guid>
            {
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
            });

            var STUDENT_ROLE_ID = Guid.NewGuid();
            await contextIdentity.Roles.AddAsync(new IdentityRole<Guid>
            {
                Name = "STUDENT",
                NormalizedName = "STUDENT",
                Id = STUDENT_ROLE_ID,
                ConcurrencyStamp = STUDENT_ROLE_ID.ToString()
            });

            var ADMIN_ID = Guid.NewGuid();
            var adminUser = new IdentityUser<Guid>
            {
                Id = ADMIN_ID,
                Email = "admin@academyio.com",
                EmailConfirmed = true,
                UserName = "admin@academyio.com",
                NormalizedUserName = "admin@academyio.com".ToUpper(),
                NormalizedEmail = "admin@academyio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = ADMIN_ROLE_ID.ToString(),
            };

            //set user password
            PasswordHasher<IdentityUser<Guid>> ph = new PasswordHasher<IdentityUser<Guid>>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Teste@123");
            await contextIdentity.Users.AddAsync(adminUser);


            await contextIdentity.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            #endregion

            #region NON-ADMIN USERS SEED
            var user1Id = Guid.NewGuid();
            var user1 = new IdentityUser<Guid>
            {
                Id = user1Id,
                Email = "aluno1@academyio.com",
                EmailConfirmed = true,
                UserName = "aluno1@academyio.com",
                NormalizedUserName = "aluno1@academyio.com".ToUpper(),
                NormalizedEmail = "aluno1@academyio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = user1Id.ToString(),
            };
            user1.PasswordHash = ph.HashPassword(user1, "Teste@123");
            await contextIdentity.Users.AddAsync(user1);



            var user2Id = Guid.NewGuid();
            var user2 = new IdentityUser<Guid>
            {
                Id = user2Id,
                Email = "aluno2@academyio.com",
                EmailConfirmed = true,
                UserName = "aluno2@academyio.com",
                NormalizedUserName = "aluno2@academyio.com".ToUpper(),
                NormalizedEmail = "aluno2@academyio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = user2Id.ToString(),
            };
            user2.PasswordHash = ph.HashPassword(user2, "Teste@123");
            await contextIdentity.Users.AddAsync(user2);

            await contextIdentity.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = user1Id
            });

            await contextIdentity.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = user2Id
            });

            await contextIdentity.SaveChangesAsync();

            #endregion
        }

    }
}
