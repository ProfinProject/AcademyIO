using AcademyIO.Bff.Extensions;
using AcademyIO.Bff.Models;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface IStudentService
    {
        
    }


    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.StudentUrl);
        }

    }
}
