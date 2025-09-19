using AcademyIO.Core.Interfaces.Services;
using AcademyIO.Courses.API.Application.Commands;
using AcademyIO.Courses.API.Application.Queries;
using AcademyIO.Courses.API.Application.Queries.ViewModels;
using AcademyIO.Courses.API.Models.ViewModels;
using AcademyIO.WebAPI.Core.Controllers;
using AcademyIO.WebAPI.Core.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AcademyIO.Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(IMediator _mediator,
                                ICourseQuery courseQuery, 
                                IAspNetUser aspNetUser) : MainController
    {
        /// <summary>
        /// Retorna todos os cursos registrados
        /// </summary>
        /// <returns><see cref="IEnumerable{CourseViewModel}"/> Retorna uma lista de CourseViewModel</returns>
        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<CourseViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetAll()
        {
            var courses = await courseQuery.GetAll();
            return CustomResponse(courses);
        }

        /// <summary>
        /// Retorna curso referente ao Id do parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="CourseViewModel"/>Retorna os dados do curso</returns>
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<CourseViewModel>> GetById(Guid id)
        {
            var course = await courseQuery.GetById(id);
            return CustomResponse(course);
        }

        /// <summary>
        /// Cria um novo curso
        /// </summary>
        /// <param name="course"></param>
        /// <returns>Retorna que o curso foi criado, status 201</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(CourseViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            var command = new AddCourseCommand(course.Name, course.Description, aspNetUser.GetUserId(), course.Price);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Faz o pagamento do curso referenciado nos parametro 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="paymentViewModel"></param>
        /// <returns>Retorna que o pagamento foi feito, status 201</returns>
        [Authorize(Roles = "STUDENT")]
        [HttpPost("{courseId:guid}/make-payment")]
        public async Task<IActionResult> MakePayment(Guid courseId, [FromBody] PaymentViewModel paymentViewModel)
        {
            var command = new ValidatePaymentCourseCommand(courseId, aspNetUser.GetUserId(), paymentViewModel.CardName,
                                                        paymentViewModel.CardNumber, paymentViewModel.CardExpirationDate,
                                                        paymentViewModel.CardCVV);
            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created);
        }
    }
}
