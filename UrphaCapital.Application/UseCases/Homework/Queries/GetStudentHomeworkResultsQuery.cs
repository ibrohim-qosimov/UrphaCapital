using MediatR;
using UrphaCapital.Domain.DTOs;

namespace UrphaCapital.Application.UseCases.Homework.Queries
{
    public class GetStudentHomeworkResultsQuery : IRequest<IEnumerable<HomeworkResultView>>
    {
        public long StudentId { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
