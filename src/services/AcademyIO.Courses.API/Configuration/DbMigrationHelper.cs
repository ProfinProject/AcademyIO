using AcademyIO.Courses.API.Data;
using AcademyIO.Courses.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Courses.API.Configuration
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

            var courseContext = scope.ServiceProvider.GetRequiredService<CoursesContext>();

            if (env.IsDevelopment())
            {
                await courseContext.Database.MigrateAsync();
                await EnsureSeedData(courseContext);
            }
        }

        private static async Task EnsureSeedData(CoursesContext courseContext)
        {
            await SeedCourses(courseContext);
            await SeedLessons(courseContext);
        }

        public static async Task SeedCourses(CoursesContext context)
        {
            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new()
                    {
                        Id = new Guid("55555555-5555-5555-5555-555555555555"),
                        Name = "Modelagem de Dominios Ricos",
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now,
                        Description = "Este e um curso sobre modelagem de dominios ricos",
                        Price = 500
                    },
                   new()
                    {
                        Id = new Guid("66666666-6666-6666-6666-666666666666"),
                        Name = "Dominando Testes de Software",
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                        UpdatedDate = DateTime.Now,
                        Description = "Este e um curso sobre testes de software",
                        Price = 350
                    },
                };

                await context.Courses.AddRangeAsync(courses);
                context.SaveChanges();
            }
        }

        public static async Task SeedLessons(CoursesContext context)
        {
            if (!context.Lessons.Any())
            {
                var courseDominios = context.Courses.FirstOrDefault(u => u.Name.Equals("Modelagem de Dominios Ricos"));
                var courseTests = context.Courses.FirstOrDefault(u => u.Name.Equals("Dominando Testes de Software"));
                if (courseDominios != null)
                {
                    var lessons = new List<Lesson>
                    {
                        new("Lesson 1", "Aula de dominios ricos 1", 80, courseDominios.Id),
                       new("Lesson 2", "Aulas de dominios ricos 2", 75, courseDominios.Id),
                    };

                    await context.Lessons.AddRangeAsync(lessons);
                    context.SaveChanges();
                }

                if (courseTests != null)
                {
                    var lessons = new List<Lesson>
                    {
                       new("Lesson 1", "Aula de testes 1", 60, courseTests.Id),
                       new("Lesson 2", "Aula de testes 2", 55, courseTests.Id)
                    };

                    await context.Lessons.AddRangeAsync(lessons);
                    context.SaveChanges();
                }
            }
        }
    }
}
