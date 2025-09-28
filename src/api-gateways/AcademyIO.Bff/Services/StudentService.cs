using AcademyIO.Bff.Extensions;
using AcademyIO.Core.Communication;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface IStudentService
    {
        Task<ResponseResult> RegisterToCourse(Guid courseId);
    }

    public class StudentService : Service, IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;

        public StudentService(HttpClient httpClient, IOptions<AppServicesSettings> settings, IPaymentService paymentService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.StudentUrl);
        }

        public async Task<ResponseResult> RegisterToCourse(Guid courseId)
        {
            if (await _paymentService.PaymentExists(courseId))
            {
                var response = await _httpClient.PostAsync($"register-to-course/{courseId}", null);

                if (!ManageHttpResponse(response)) return await DeserializeResponse<ResponseResult>(response);

                return Ok();
            }
            else
            {
                var paymentNotFound = new ResponseResult
                {
                    Status = 400,
                    Errors = new ResponseErrorMessages { Messages = new List<string> { "Pagamento inexiste para este curso." } }
                };
                return paymentNotFound;
            }
        }
    }
}
