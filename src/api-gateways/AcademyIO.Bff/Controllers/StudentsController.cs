using AcademyIO.Bff.Services;
using AcademyIO.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AcademyIO.Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : MainController
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IPaymentService _paymentService;
        public StudentsController(
        IStudentService studentService,
        ICourseService courseService,
        IPaymentService paymentService)
        {
            _studentService = studentService;
            _courseService = courseService;
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("register-to-course")]
        public async Task<IActionResult> RegisterToCourse(Guid courseId)
        {
            var course = await _courseService.GetById(courseId);
            if (course == null)
                return NotFound("Curso não encontrado.");

            var paymentExists = await _paymentService.PaymentExists(courseId);
            if (!paymentExists)
               return UnprocessableEntity("Você não possui acesso a esse curso.");

            var response = await _studentService.RegisterToCourse(courseId);

            return CustomResponse(response);
        }
    }
}
