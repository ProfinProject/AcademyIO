using AcademyIO.Bff.Extensions;
using AcademyIO.Bff.Models;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface ICourseService
    {
        Task<CourseViewModel> GetById(Guid id);
    }

    public class CourseService : Service, ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CourseUrl);
        }

        public Task<CourseViewModel> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
