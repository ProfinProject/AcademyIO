using AcademyIO.Students.API.Data;
using AcademyIO.Students.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Students.API.Configuration
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelper.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication application)
        {
            var services = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var studentsContext = scope.ServiceProvider.GetRequiredService<StudentsContext>();

            if (env.IsDevelopment())
            {
                await studentsContext.Database.MigrateAsync();
                await EnsureSeedData(studentsContext);
            }
        }

        private static async Task EnsureSeedData(StudentsContext studentsContext)
        {
            await SeedUsers(studentsContext);
        }

        public static async Task SeedUsers(StudentsContext context)
        {
            if (context.Users.Any()) return;

            #region ADMIN SEED
            var ADMIN_ROLE_ID = Guid.NewGuid();
            await context.Roles.AddAsync(new IdentityRole<Guid>
            {
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
            });

            var STUDENT_ROLE_ID = Guid.NewGuid();
            await context.Roles.AddAsync(new IdentityRole<Guid>
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
                Email = "admin@fabianoio.com",
                EmailConfirmed = true,
                UserName = "admin@fabianoio.com",
                NormalizedUserName = "admin@fabianoio.com".ToUpper(),
                NormalizedEmail = "admin@fabianoio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = ADMIN_ROLE_ID.ToString(),
            };

            //set user password
            PasswordHasher<IdentityUser<Guid>> ph = new PasswordHasher<IdentityUser<Guid>>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Teste@123");
            await context.Users.AddAsync(adminUser);

            var user = new StudentUser(adminUser.Id, adminUser.UserName, "Admin", "Admin", adminUser.Email, DateTime.Now.AddYears(-20), true);
            await context.SystemUsers.AddAsync(user);

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            context.SaveChanges();
            #endregion

            #region NON-ADMIN USERS SEED
            var user1Id = Guid.NewGuid();
            var user1 = new IdentityUser<Guid>
            {
                Id = user1Id,
                Email = "user1@fabianoio.com",
                EmailConfirmed = true,
                UserName = "user1@fabianoio.com",
                NormalizedUserName = "user1@fabianoio.com".ToUpper(),
                NormalizedEmail = "user1@fabianoio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = user1Id.ToString(),
            };
            user1.PasswordHash = ph.HashPassword(user1, "Teste@123");
            await context.Users.AddAsync(user1);

            var systemUser1 = new StudentUser(user1.Id, user1.UserName, "User1", "User1", user1.Email, DateTime.Now.AddYears(21), false);
            await context.SystemUsers.AddAsync(systemUser1);

            var user2Id = Guid.NewGuid();
            var user2 = new IdentityUser<Guid>
            {
                Id = user2Id,
                Email = "user2@fabianoio.com",
                EmailConfirmed = true,
                UserName = "user2@fabianoio.com",
                NormalizedUserName = "user2@fabianoio.com".ToUpper(),
                NormalizedEmail = "user2@fabianoio.com".ToUpper(),
                LockoutEnabled = true,
                SecurityStamp = user2Id.ToString(),
            };
            user2.PasswordHash = ph.HashPassword(user2, "Teste@123");
            await context.Users.AddAsync(user2);

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = user1Id
            });

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
            {
                RoleId = STUDENT_ROLE_ID,
                UserId = user2Id
            });


            var systemUser2 = new StudentUser(user2.Id, user2.UserName, "User2", "User2", user2.Email, DateTime.Now.AddYears(-22), false);
            await context.SystemUsers.AddAsync(systemUser2);

            context.SaveChanges();
            #endregion
        }


    }
}
