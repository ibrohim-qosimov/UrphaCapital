namespace UrphaCapital.Domain.Entities
{
    public class Test
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
