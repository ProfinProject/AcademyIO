using AcademyIO.Core.Enums;
using AcademyIO.Courses.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Courses.API.Configuration
{
    public static class AddEF
    {
        public static WebApplicationBuilder AddContext(this WebApplicationBuilder builder, EDatabases databases)
        {
            switch (databases)
            {
                case EDatabases.SQLServer:
                    builder.Services.AddDbContext<CoursesContext>(opt =>
                    {
                        opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
                    });
                    break;

                case EDatabases.SQLite:
                    builder.Services.AddDbContext<CoursesContext>(opt =>
                    {
                        opt.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
                    });                    
                    break;

                default:
                    throw new ArgumentException($"Banco de dados {databases} não suportado.");
            }

            return builder;
        }
    }
}
