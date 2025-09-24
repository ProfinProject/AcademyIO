using AcademyIO.Payments.API.Data;
using AcademyIO.Payments.API.Business;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Payments.API.Configuration
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
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var paymentsContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            if (env.IsDevelopment())
            {
                await paymentsContext.Database.MigrateAsync();
                await EnsureSeedData(paymentsContext);
            }
        }

        private static async Task EnsureSeedData(PaymentsContext paymentsContext)
        {
            await SeedPayments(paymentsContext);
        }

        public static async Task SeedPayments(PaymentsContext context)
        {
            if (!context.Payments.Any())
            {
                var payments = new List<Payment>
                {
                    new()
                    {
                        CourseId = Guid.NewGuid(),
                        StudentId = Guid.NewGuid(),
                        Value = 500,
                        CardName = "Aluno Teste",
                        CardNumber = "1234567890123456",
                        CardExpirationDate = "12/28",
                        CardCVV = "123",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Deleted = false
                    },
                    new()
                    {
                        CourseId = Guid.NewGuid(),
                        StudentId = Guid.NewGuid(),
                        Value = 350,
                        CardName = "Aluno Teste 2",
                        CardNumber = "6543210987654321",
                        CardExpirationDate = "11/27",
                        CardCVV = "321",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Deleted = false
                    }
                };

                await context.Payments.AddRangeAsync(payments);
                context.SaveChanges();
            }
        }
    }
}
