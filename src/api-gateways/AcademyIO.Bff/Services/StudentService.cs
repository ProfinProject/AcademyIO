using AcademyIO.Bff.Extensions;
using AcademyIO.Core.Communication;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface IStudentService
    {
        Task<ResponseResult> RegisterToCourse(Guid courseId);
    }

    public class StudentService : Service, IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.StudentUrl);
        }

        public async Task<ResponseResult> RegisterToCourse(Guid courseId)
        {
            var response = await _httpClient.PostAsync($"/api/student/register-to-course/{courseId}", null);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }
    }
}
