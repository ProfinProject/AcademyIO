using AcademyIO.Bff.Extensions;
using AcademyIO.Bff.Models;
using AcademyIO.Core.Communication;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetAll();
        Task<CourseViewModel> GetById(Guid id);
        Task<ResponseResult> Create(CourseViewModel course);

        Task<ResponseResult> MakePayment(Guid courseId, PaymentViewModel payment);
        Task<IEnumerable<LessonViewModel>> GetLessonByCourse(Guid courseId);
        Task<IEnumerable<LessonProgressViewModel>> GetProgressLesson();
        Task<ResponseResult> CreateLesson(LessonViewModel lesson);
        Task<ResponseResult> StartLesson(Guid lessonId);
        Task<ResponseResult> FinishLesson(Guid lessonId);
        Task<ResponseResult> Update(CourseViewModel course);
        Task<ResponseResult> Remove(Guid id);
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

        public async Task<ResponseResult> Update(CourseViewModel course)
        {
            var itemContent = GetContent(course);

            var response = await _httpClient.PutAsync("api/courses/update/", itemContent);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/courses/remove/{id}");

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

        public async Task<IEnumerable<LessonViewModel>> GetLessonByCourse(Guid courseId)
        {
            var response = await _httpClient.GetAsync($"/api/Lessons/get-by-courseId/{courseId}");

            ManageHttpResponse(response);

            return await DeserializeResponse<IEnumerable<LessonViewModel>>(response);
        }

        public async Task<IEnumerable<LessonProgressViewModel>> GetProgressLesson()
        {
            var response = await _httpClient.GetAsync($"/api/Lessons/get-progress");

            ManageHttpResponse(response);

            return await DeserializeResponse<IEnumerable<LessonProgressViewModel>>(response);
        }

        public async Task<ResponseResult> CreateLesson(LessonViewModel lesson)
        {
            var itemContent = GetContent(lesson);

            var response = await _httpClient.PostAsync("/api/Lessons", itemContent);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> StartLesson(Guid lessonId)
        {
            var response = await _httpClient.PostAsync($"/api/Lessons/{lessonId}/start-class", null);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> FinishLesson(Guid lessonId)
        {
            var response = await _httpClient.PostAsync($"/api/Lessons/{lessonId}/finish-class", null);

            if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

            return Ok();
        }
    }
}
