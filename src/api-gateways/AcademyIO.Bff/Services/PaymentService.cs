using AcademyIO.Bff.Extensions;
using AcademyIO.WebAPI.Core.User;
using Microsoft.Extensions.Options;

namespace AcademyIO.Bff.Services
{
    public interface IPaymentService
    {
        Task<bool> PaymentExists(Guid courseId);
    }


    public class PaymentService : Service, IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IAspNetUser _aspNetUser;

        public PaymentService(HttpClient httpClient, IOptions<AppServicesSettings> settings, IAspNetUser aspNetUser)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PaymentUrl);
            _aspNetUser = aspNetUser;
        }

        public async Task<bool> PaymentExists(Guid courseId)
        {
            var token = _aspNetUser.GetUserToken();

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/payments/exists?courseId={courseId}");

            var exists = false;
            if (!ManageHttpResponse(response)) exists = await response.Content.ReadFromJsonAsync<bool>();

            return exists;
        }
    }
}
