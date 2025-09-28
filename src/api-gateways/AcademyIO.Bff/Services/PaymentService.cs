using AcademyIO.Bff.Extensions;
using AcademyIO.Core.Communication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public PaymentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.StudentUrl);
        }

        public async Task<bool> PaymentExists(Guid courseId)
        {
            var response = await _httpClient.GetAsync($"exists?courseId={courseId}");

            var exists = false;
            if (!ManageHttpResponse(response)) exists = await response.Content.ReadFromJsonAsync<bool>();

            return exists;
        }
    }
}
