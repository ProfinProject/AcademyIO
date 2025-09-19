using AcademyIO.Bff.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcademyIO.Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
    }
}
