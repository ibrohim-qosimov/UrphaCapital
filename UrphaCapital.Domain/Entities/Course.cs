using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public string Picture { get; set; }
        public string Price { get; set; }
        public long MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
