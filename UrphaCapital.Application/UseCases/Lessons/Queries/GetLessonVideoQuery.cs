using MediatR;

namespace UrphaCapital.Application.UseCases.Lessons.Queries
{
    public class GetLessonVideoQuery : IRequest<Stream?>
    {
        public string Id { get; set; }
    }
}
