using AcademyIO.Bff.Services;
using AcademyIO.Core.Models;
using AcademyIO.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AcademyIO.Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : MainController
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        public StudentsController(
        IStudentService studentService,
        ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        [HttpPost]
        [Route("register-to-course")]
        public async Task<IActionResult> RegisterToCourse(Guid courseId)
        {
            var course = await _courseService.GetById(courseId);
            if (course == null)
                return NotFound("Curso não encontrado.");

            //TO DO LUIS, criar payment exists endpoint
            //var paymentExists = await paymentQuery.PaymentExists(userId, courseId);
            //if (!paymentExists)
            //    return UnprocessableEntity("Você não possui acesso a esse curso.");
               
           var response = await _studentService.RegisterToCourse(courseId);

            return CustomResponse(response);
        }
    }
}
