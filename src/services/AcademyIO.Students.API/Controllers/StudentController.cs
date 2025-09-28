using AcademyIO.Students.API.Application.Commands;
using AcademyIO.WebAPI.Core.Controllers;
using AcademyIO.WebAPI.Core.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AcademyIO.Students.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IAspNetUser _user;

        public StudentController(IMediator mediator, IAspNetUser user)
        {
            _mediator = mediator;
            _user = user;
        }

        /// <summary>
        /// Matrícula o aluno ao curso, e as aulas referente a esse curso
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Se o curso existe e o aluno já pagou o curso, retorna 201 aluno registrado</returns>
        [HttpPost("register-to-course/{courseId:guid}")]
        public async Task<IActionResult> RegisterToCourse(Guid courseId)
        {
            var userId = _user.GetUserId();

            var commandRegistration = new AddRegistrationCommand(userId, courseId);
            await _mediator.Send(commandRegistration);

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
