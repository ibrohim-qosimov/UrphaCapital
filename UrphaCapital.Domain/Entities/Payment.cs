using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Domain.Entities
{
    public class Payment
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }

        public DateTimeOffset PaymentDate = DateTimeOffset.UtcNow;
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
