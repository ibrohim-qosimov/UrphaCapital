using System.Text.Json.Serialization;

namespace UrphaCapital.Application.ViewModels;
public class TransactionViewModel
{
    [JsonPropertyName("student_id")]
    public long StudentId { get; set; }

    [JsonPropertyName("student_name")]
    public string StudentName { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("course_name")]
    public string CourseName { get; set; }

    [JsonPropertyName("course_id")]
    public int CourseId { get; set; }

    [JsonPropertyName("payment_date")]
    public string PaymentDate { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}
