namespace UrphaCapital.Domain.Entities
{
    public class Answer
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool isCorrect { get; set; }
        public long TestId { get; set; }
        public Test Test { get; set; }
    }
}
