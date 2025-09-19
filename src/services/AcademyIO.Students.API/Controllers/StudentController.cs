using AcademyIO.Core.Interfaces.Services;
using AcademyIO.ManagementStudents.Application.Commands;
using AcademyIO.WebAPI.Core.Controllers;
using AcademyIO.WebAPI.Core.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FabianoIO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMediator _mediator,
                                //ICourseQuery courseQuery,
                                //IPaymentQuery paymentQuery,
                                IAspNetUser user,
                                INotifier notifier) : MainController()
    {
        /// <summary>
        /// Matrícula o aluno ao curso, e as aulas referente a esse curso
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Se o curso existe e o aluno já pagou o curso, retorna 201 aluno registrado</returns>
        [HttpPost("register-to-course/{courseId:guid}")]
        public async Task<IActionResult> RegisterToCourse(Guid courseId)
        {
            var userId = user.GetUserId();
            //var course = await courseQuery.GetById(courseId);
            //if (course == null)
            //    return NotFound("Curso não encontrado.");

            //var paymentExists = await paymentQuery.PaymentExists(userId, courseId);
            //if (!paymentExists)
            //    return UnprocessableEntity("Você não possui acesso a esse curso.");

            var commandRegistration = new AddRegistrationCommand(userId, courseId);
            await _mediator.Send(commandRegistration);

            //var commandCreationProgress = new CreateProgressByCourseCommand(courseId, userId);
            //await _mediator.Send(commandCreationProgress);

            return CustomResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Matrícula o aluno ao curso, e as aulas referente a esse curso
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Se o curso existe e o aluno já pagou o curso, retorna 201 aluno registrado</returns>
        [HttpGet("get-registration/{courseId:guid}")]
        public async Task<IActionResult> GetRegistration(Guid courseId)
        {
            //to do get registration FABIANO
            
            return CustomResponse(HttpStatusCode.OK);
        }
    }
}
