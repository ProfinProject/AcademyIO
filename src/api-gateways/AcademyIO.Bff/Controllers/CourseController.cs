using AcademyIO.Bff.Models;
using AcademyIO.Bff.Services;
using AcademyIO.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyIO.Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : MainController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("courses")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAll();
            return CustomResponse(response);
        }

        [HttpGet]
        [Route("course")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _courseService.GetById(id);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("create-course")]
        public async Task<IActionResult> CreateCourse(CourseViewModel course)
        {
            var response = await _courseService.Create(course);
            return CustomResponse(response);
        }

        [HttpPut]
        [Route("update-course")]
        public async Task<IActionResult> UpdateCourse(CourseViewModel course)
        {
            var response = await _courseService.Update(course);
            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("remove-course")]
        public async Task<IActionResult> RemoveCourse(Guid id)
        {
            var response = await _courseService.Remove(id);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("make-payment")]
        public async Task<IActionResult> MakePaymentCourse(Guid courseId, PaymentViewModel payment)
        {
            var response = await _courseService.MakePayment(courseId, payment);
            return CustomResponse(response);
        }

        [HttpGet]
        [Route("lesson-course")]
        public async Task<IActionResult> GetLessonByCourse(Guid courseId)
        {
            var response = await _courseService.GetLessonByCourse(courseId);
            return CustomResponse(response);
        }

        [HttpGet]
        [Route("progress-lesson")]
        public async Task<IActionResult> GetProgressLesson()
        {
            var response = await _courseService.GetProgressLesson();
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("create-lesson")]
        public async Task<IActionResult> CreateLesson(LessonViewModel lesson)
        {
            var response = await _courseService.CreateLesson(lesson);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("start-lesson")]
        public async Task<IActionResult> StartLesson(Guid lessonId)
        {
            var response = await _courseService.StartLesson(lessonId);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("finish-lesson")]
        public async Task<IActionResult> FinishLesson(Guid lessonId)
        {
            var response = await _courseService.FinishLesson(lessonId);
            return CustomResponse(response);
        }
    }
}
