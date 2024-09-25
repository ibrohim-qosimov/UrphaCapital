using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers.QueryHandler
{
    public class GetStudentCoursesQueryHandler : IRequestHandler<GetStudentCoursesQuery, IEnumerable<Course>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentCoursesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (student == null || student.CourseIds == null)
            {
                return Enumerable.Empty<Course>();
            }

            var courses = await _context.Courses
                .Where(x => student.CourseIds.Contains(x.Id))
                .ToListAsync();

            return courses;
        }
    }
}
