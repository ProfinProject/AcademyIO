using AcademyIO.Core.Interfaces.Services;
using AcademyIO.Core.Notifications;
using AcademyIO.Courses.API.Application.Commands;
using AcademyIO.Courses.API.Application.Queries;
using AcademyIO.Courses.API.Data.Repository;
using AcademyIO.Courses.API.Models;
using AcademyIO.WebAPI.Core.User;

namespace AcademyIO.Courses.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();            

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddScoped<INotifier, Notifier>();
            
            services.AddScoped<ICourseQuery, CourseQuery>();
            services.AddScoped<ILessonQuery, LessonQuery>();            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddLessonCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateCourseCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RemoveCourseCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<StartLessonCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<FinishLessonCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddCourseCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateProgressByCourseCommand>());
            
            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }
    }
}
