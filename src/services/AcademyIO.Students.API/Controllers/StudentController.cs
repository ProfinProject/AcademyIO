using AcademyIO.Students.API.Application.Commands;
using AcademyIO.Students.API.Application.Queries;
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
        private readonly IRegistrationQuery _registrationQuery;

        public StudentController(IMediator mediator, IAspNetUser user, IRegistrationQuery registrationQuery)
        {
            _mediator = mediator;
            _user = user;
            _registrationQuery = registrationQuery;
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
        /// Retorna as matrículas do aluno logado 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>Lista de matriculas</returns>
        [HttpGet("get-registration/{studentId:guid}")]
        public IActionResult GetRegistration(Guid studentId)
        {
            var students = _registrationQuery.GetByStudent(studentId);
            return CustomResponse(students);
        }

        /// <summary>
        /// Retorna todas as matrículas
        /// </summary>
        /// <returns>Lista de matriculas</returns>
        [HttpGet("get-all-registrations")]
        public IActionResult GetAllRegistrations()
        {
            var students = _registrationQuery.GetAllRegistrations();
            return CustomResponse(students);
        }
    }
}
