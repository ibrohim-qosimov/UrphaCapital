using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Courses.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Handlers.QueryHandlers
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Course>
    {
        private readonly IApplicationDbContext _context;

        public GetCourseByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                 throw new Exception("Not found");
        }
    }
}
