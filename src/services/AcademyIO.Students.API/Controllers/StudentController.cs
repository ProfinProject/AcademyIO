//using AcademyIO.Core.Interfaces.Services;
//using AcademyIO.ManagementCourses.Application.Commands;
//using AcademyIO.ManagementCourses.Application.Queries;
//using AcademyIO.ManagementPayments.Application.Query;
//using AcademyIO.ManagementStudents.Aplication.Commands;
//using MediatR;
using AcademyIO.Core.Interfaces.Services;
using AcademyIO.WebAPI.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FabianoIO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator _mediator,
                                //ICourseQuery courseQuery,
                                //IPaymentQuery paymentQuery,
                                INotifier notifier) : MainController()
    {
        /// <summary>
        /// Matrícula o aluno ao curso, e as aulas referente a esse curso
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Se o curso existe e o aluno já pagou o curso, retorna 201 aluno registrado</returns>
        [Authorize(Roles = "STUDENT")]
        [HttpPost("register-to-course/{courseId:guid}")]
        public async Task<IActionResult> RegisterToCourse(Guid courseId)
        {
            //var course = await courseQuery.GetById(courseId);
            //if (course == null)
            //    return NotFound("Curso não encontrado.");

            //var paymentExists = await paymentQuery.PaymentExists(UserId, courseId);
            //if (!paymentExists)
                return UnprocessableEntity("Você não possui acesso a esse curso.");

            //var commandRegistration = new AddRegistrationCommand(UserId, courseId);
            //await _mediator.Send(commandRegistration);

           // var commandCreationProgress = new CreateProgressByCourseCommand(courseId, UserId);
           // await _mediator.Send(commandCreationProgress);

            return CustomResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Matrícula o aluno ao curso, e as aulas referente a esse curso
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Se o curso existe e o aluno já pagou o curso, retorna 201 aluno registrado</returns>
        [Authorize(Roles = "STUDENT")]
        [HttpGet("register-to-course/{courseId:guid}")]
        public async Task<IActionResult> GetRegistration(Guid courseId)
        {
            //var course = await courseQuery.GetById(courseId);
            //if (course == null)
            //    return NotFound("Curso não encontrado.");

            //var paymentExists = await paymentQuery.PaymentExists(UserId, courseId);
            //if (!paymentExists)
            //    return UnprocessableEntity("Você não possui acesso a esse curso.");

           // var commandRegistration = new AddRegistrationCommand(UserId, courseId);
           // await _mediator.Send(commandRegistration);

            //var commandCreationProgress = new CreateProgressByCourseCommand(courseId, UserId);
            //await _mediator.Send(commandCreationProgress);

            return CustomResponse(HttpStatusCode.Created);
        }
    }
}
