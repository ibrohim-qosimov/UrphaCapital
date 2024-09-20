using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UrphaCapital.Application.PaymentProcessing
{
    public class PaymentService : IPaymentService
    {
       private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        private const string SERVICE_ID = "37106";
        private const string MERCHANT_ID = "28080";
        private const string SECRET_KEY = "ICXx7BDBVd";

        // Prepare va Confirm URL-lari
        private const string PrepareUrl = "https://api.click.uz/v1/merchant/prepare/";
        private const string ConfirmUrl = "https://api.click.uz/v1/merchant/confirm/";

        public async Task<string> PreparePaymentAsync(decimal amount, string transactionParam)
        {
            var requestBody = new
            {
                service_id = SERVICE_ID,
                merchant_id = MERCHANT_ID,
                amount = amount,
                transaction_param = transactionParam
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(PrepareUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        // Confirm funksiyasi
        public async Task<string> ConfirmPaymentAsync(string transactionId)
        {
            var requestBody = new
            {
                merchant_id = MERCHANT_ID,
                transaction_id = transactionId,
                secret_key = SECRET_KEY // Secret key ni qo'shamiz
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ConfirmUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
