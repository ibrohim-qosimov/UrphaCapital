using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers.QueryHandler
{
    public class GetAllStudentsByIdQueryHandler : IRequestHandler<GetAllStudentsByIdQuery, Student>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentsByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> Handle(GetAllStudentsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                    throw new NotImplementedException();
        }
    }
}
