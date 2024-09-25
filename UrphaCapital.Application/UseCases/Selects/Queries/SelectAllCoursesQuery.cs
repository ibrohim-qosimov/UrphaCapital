using MediatR;
using UrphaCapital.Application.ViewModels.SelectorModels;

namespace UrphaCapital.Application.UseCases.Selects.Queries
{
    public class SelectAllCoursesQuery : IRequest<IEnumerable<CourseSelectModel>>
    {
    }
}
