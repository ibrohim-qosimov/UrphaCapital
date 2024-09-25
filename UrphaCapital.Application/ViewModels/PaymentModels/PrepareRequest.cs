using System.Text.Json.Serialization;

namespace UrphaCapital.Application.ViewModels.PaymentModels;

public class PrepareRequest
{
    [JsonPropertyName("click_trans_id")]
    public long ClickTransId { get; set; }

    [JsonPropertyName("service_id")]
    public int ServiceId { get; set; }

    [JsonPropertyName("click_paydoc_id")]
    public long ClickPayDocId { get; set; }

    [JsonPropertyName("merchant_trans_id")]
    public string MerchantTransId { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("action")]
    public int Action { get; set; }

    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("error_note")]
    public string ErrorNote { get; set; }

    [JsonPropertyName("sign_time")]
    public string SignTime { get; set; }

    [JsonPropertyName("sign_string")]
    public string SignString { get; set; }


}

