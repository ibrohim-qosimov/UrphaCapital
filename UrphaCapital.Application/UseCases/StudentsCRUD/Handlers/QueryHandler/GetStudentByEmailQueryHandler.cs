using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.StudentsCRUD.Queries;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers.QueryHandler
{
    public class GetStudentByEmailQueryHandler : IRequestHandler<GetStudentByEmailQuery, Student>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentByEmailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> Handle(GetStudentByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                    ?? throw new Exception("Not found");
        }
    }
}
