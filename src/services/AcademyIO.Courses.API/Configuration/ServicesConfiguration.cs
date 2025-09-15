using AcademyIO.Core.Interfaces.Services;
using AcademyIO.Core.Notifications;
using AcademyIO.Courses.API.Application.Commands;
using AcademyIO.Courses.API.Application.Queries;
using AcademyIO.Courses.API.Data.Repository;
using AcademyIO.Courses.API.Models;
using AcademyIO.WebAPI.Core.User;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AcademyIO.Courses.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ILessonRepository, LessonRepository>();            

            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddScoped<INotifier, Notifier>();

            builder.Services.AddScoped<ICourseQuery, CourseQuery>();
            builder.Services.AddScoped<ILessonQuery, LessonQuery>();            
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddLessonCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<StartLessonCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<FinishLessonCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddCourseCommand>());
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateProgressByCourseCommand>());
            
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAspNetUser, AspNetUser>();

            return builder;
        }
    }
}
