using AcademyIO.Bff.Models;
using AcademyIO.Bff.Services;
using AcademyIO.WebAPI.Core.Controllers;
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
        [Route("get-courses")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAll();
            return CustomResponse(response);
        }

        [HttpGet]
        [Route("get-course")]
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

        [HttpPost]
        [Route("make-payment")]
        public async Task<IActionResult> MakePaymentCourse(Guid courseId, PaymentViewModel payment)
        {
            var response = await _courseService.MakePayment(courseId, payment);
            return CustomResponse(response);
        }
    }
}
