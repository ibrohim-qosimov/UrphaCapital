using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ViewModels.PaymentModels;
using UrphaCapital.Application.ViewModels.PaymentModels.Exceptions;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.ExternalServices.PaymentProcessing;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly IApplicationDbContext _context;

    public PaymentService(HttpClient httpClient, IApplicationDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    private readonly string MerchantId = "hi";
    private readonly string ServiceId = "hi";
    private readonly string MerchantUserId = "hi";
    private readonly string SecretKey = "hi";
    private readonly string BaseUrl = "https://api.click.uz/v2/merchant/";


    #region yagni ni chetlab o'tilgan holat
    public async Task CreateInvoice(decimal amount, string phoneNumber, string merchantTransId)
    {
        string url = BaseUrl + "invoice/create";

        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var requestData = new
            {
                service_id = ServiceId,
                amount,
                phone_number = phoneNumber,
                merchant_trans_id = merchantTransId
            };

            string jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Invoice created successfully:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error creating invoice:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task CheckInvoiceStatus(long invoiceId)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}invoice/status/{ServiceId}/{invoiceId}";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            HttpResponseMessage response = await client.GetAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Invoice status check successful:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error checking invoice status:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task CheckPaymentStatus(long paymentId)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}payment/status/{ServiceId}/{paymentId}";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            HttpResponseMessage response = await client.GetAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment status check successful:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error checking payment status:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task CheckPaymentStatusByMerchantTransId(string merchantTransId)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}payment/status_by_mti/{ServiceId}/{merchantTransId}";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            HttpResponseMessage response = await client.GetAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment status by merchant_trans_id check successful:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error checking payment status by merchant_trans_id:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task ReversePayment(long paymentId)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}payment/reversal/{ServiceId}/{paymentId}";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            HttpResponseMessage response = await client.DeleteAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment reversal successful:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error reversing payment:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task PartialRefund(long paymentId, float amount)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}payment/partial_reversal/{ServiceId}/{paymentId}/{amount}";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            HttpResponseMessage response = await client.DeleteAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Partial refund successful:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error processing partial refund:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task CreateCardToken(string cardNumber, string expireDate, bool temporary)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}card_token/request";

        var requestData = new
        {
            service_id = ServiceId,
            card_number = cardNumber,
            expire_date = expireDate,
            temporary = temporary ? 1 : 0
        };

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, jsonContent);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Card token created successfully:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error creating card token:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task VerifyCardToken(string cardToken, int smsCode)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}card_token/verify";

        var requestData = new
        {
            service_id = ServiceId,
            card_token = cardToken,
            sms_code = smsCode
        };

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, jsonContent);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Card token verified successfully:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error verifying card token:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task MakePaymentWithToken(string cardToken, decimal amount, string merchantTransId)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}card_token/payment";

        var requestData = new
        {
            service_id = ServiceId,
            card_token = cardToken,
            amount,
            transaction_parameter = merchantTransId
        };

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, jsonContent);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment processed successfully:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error processing payment:");
                Console.WriteLine(responseString);
            }
        }
    }

    public async Task DeleteCardToken(string cardToken)
    {
        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string digest = GenerateDigest(timestamp);

        string url = $"{BaseUrl}card_token/{ServiceId}/{cardToken}";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Auth", $"{MerchantUserId}:{digest}:{timestamp}");
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            HttpResponseMessage response = await client.DeleteAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Card token deleted successfully:");
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Error deleting card token:");
                Console.WriteLine(responseString);
            }
        }
    }
    private string GenerateDigest(long timestamp)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            string data = timestamp + SecretKey;
            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    #endregion


    public async Task<PrepareResponse> Prepare(PrepareRequest prepareRequest)
    {
        var generatedSignSrting = GenerateSignString(
            prepareRequest.ClickTransId,
            prepareRequest.ServiceId,
            SecretKey,
            prepareRequest.MerchantTransId,
            prepareRequest.Amount,
            prepareRequest.Action,
            prepareRequest.SignTime);

        if (prepareRequest.SignString != generatedSignSrting)
            throw new InvalidSignStringException();


        var clickTransaction = new ClickTransaction()
        {
            ClickTransId = prepareRequest.ClickTransId,
            MerchantTransId = prepareRequest.MerchantTransId,
            Amount = prepareRequest.Amount,
            SignTime = DateTime.Parse(prepareRequest.SignTime),
            Status = "prepare"
        };
        _context.ClickTransactions.Add(clickTransaction);
        await _context.SaveChangesAsync();

        var response = new PrepareResponse()
        {
            ClickTransId = clickTransaction.ClickTransId,
            MerchantTransId = clickTransaction.MerchantTransId,
            MerchantPrepareId = 1,//orderId bervardik testga bu korib chiqish keray
            Error = 0,
            ErrorNote = "Payment prepared successfully"
        };

        return response;
    }

    public async Task<ComplareResponse> Complate(ComplateRequest complateRequest)
    {
        //there are some issues
        var generatedSignString = GenerateSignString(
            complateRequest);


        if (generatedSignString != null)
        {
            throw new InvalidSignStringException();
        }

        if (0 == 0)
        {
            var clickTransaction = _context.ClickTransactions.FirstOrDefault(c => c.ClickTransId == 1);
            if (clickTransaction != null)
            {
                clickTransaction.Situation = 1;
                clickTransaction.Status = "success";
            }

            await _context.SaveChangesAsync();
        }

        var response = new ComplareResponse() { };

        return response;
    }
    private string GenerateSignString(params object[] parameters)
    {
        var input = string.Join("", parameters);
        using (var md5 = MD5.Create())
        {
            var inputBytes = Encoding.UTF8.GetBytes(input + SecretKey);
            var hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
