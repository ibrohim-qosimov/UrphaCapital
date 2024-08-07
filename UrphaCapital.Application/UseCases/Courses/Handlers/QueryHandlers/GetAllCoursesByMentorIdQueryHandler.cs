using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.QueryHandlers
{
    public class GetAllCoursesByMentorIdQueryHandler : IRequestHandler<GetAllCoursesByMentorIdQuery, IEnumerable<Course>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCoursesByMentorIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> Handle(GetAllCoursesByMentorIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Courses
            .Where(l => l.MentorId == request.MentorId)
                .Select(x => new Course
                {
                    Id = x.Id,
                    Name = x.Name,
                    MentorId = x.MentorId
                })
                .ToListAsync(cancellationToken);

        }
    }
}
