using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Selects.Queries;
using UrphaCapital.Application.ViewModels.SelectorModels;

namespace UrphaCapital.Application.UseCases.Selects.Handlers
{
    public class SelectAllCoursesQueryHandler : IRequestHandler<SelectAllCoursesQuery, IEnumerable<CourseSelectModel>>
    {
        private readonly IApplicationDbContext _context;

        public SelectAllCoursesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseSelectModel>> Handle(SelectAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Courses.OrderBy(x => x.Name).Select(x => new CourseSelectModel()
            {
                Title = x.Name,
                Id = x.Id,
            }).ToListAsync(cancellationToken);
        }
    }
}
