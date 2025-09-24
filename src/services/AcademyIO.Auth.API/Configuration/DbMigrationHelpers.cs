using AcademyIO.Auth.API.Data;
using AcademyIO.Core.Utils;
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

            var admin = SeedStudentUserData.Users.FirstOrDefault(a => a.IsAdmin)!;
            var student1 = SeedStudentUserData.Users.FirstOrDefault(a => a.FirstName.Equals("Student1"))!;
            var student2 = SeedStudentUserData.Users.FirstOrDefault(a => a.FirstName.Equals("Student2"))!;

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

            var adminUser = new IdentityUser<Guid>
            {
                Id = admin.Id,
                Email = admin.Email,
                EmailConfirmed = true,
                UserName = admin.Email,
                NormalizedUserName = admin.Email.ToUpper(),
                NormalizedEmail = admin.Email.ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = ADMIN_ROLE_ID.ToString(),
            };

            //set user password
            PasswordHasher<IdentityUser<Guid>> ph = new PasswordHasher<IdentityUser<Guid>>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, admin.Password);
            await contextIdentity.Users.AddAsync(adminUser);

            await contextIdentity.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = admin.Id
            });

            #endregion

            #region NON-ADMIN USERS SEED
            var user1 = new IdentityUser<Guid>
            {
                Id = student1.Id,
                Email = student1.Email,
                EmailConfirmed = true,
                UserName = student1.Email,
                NormalizedUserName = student1.Email.ToUpper(),
                NormalizedEmail = student1.Email.ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = student1.Id.ToString(),
            };

            user1.PasswordHash = ph.HashPassword(user1, student1.Password);
            await contextIdentity.Users.AddAsync(user1);

            var user2 = new IdentityUser<Guid>
            {
                Id = student2.Id,
                Email = student2.Email,
                EmailConfirmed = true,
                UserName = student2.Email,
                NormalizedUserName = student2.Email.ToUpper(),
                NormalizedEmail = student2.Email.ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = student2.Email.ToString(),
            };
            user2.PasswordHash = ph.HashPassword(user2, student2.Password);
            await contextIdentity.Users.AddAsync(user2);

            await contextIdentity.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = student1.Id
            });

            await contextIdentity.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = student2.Id
            });

            await contextIdentity.SaveChangesAsync();

            #endregion
        }

    }
}
