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


    }
}
