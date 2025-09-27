using AcademyIO.Bff.Extensions;
using AcademyIO.Bff.Models;
using AcademyIO.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetAll();
        Task<CourseViewModel> GetById(Guid id);
        Task<ResponseResult> Create(CourseViewModel course);

        Task<ResponseResult> MakePayment(Guid courseId, PaymentViewModel payment);
    }

    public class CourseService : Service, ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CourseUrl);
        }

        public async Task<IEnumerable<CourseViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/courses");

            ManageHttpResponse(response);

            return await DeserializeResponse<IEnumerable<CourseViewModel>>(response);
        }

        public async Task<CourseViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/courses/{id}");

            ManageHttpResponse(response);

            return await DeserializeResponse<CourseViewModel>(response);
        }

        public async Task<ResponseResult> Create(CourseViewModel course)
        {
            var itemContent = GetContent(course);

            var response = await _httpClient.PostAsync("api/courses/create/", itemContent);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> MakePayment(Guid courseId, PaymentViewModel payment)
        {
            var itemContent = GetContent(payment);

            var response = await _httpClient.PostAsync($"api/courses/{courseId}/make-Payment/", itemContent);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }
    }
}
