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
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var res = await _context.Students.ToListAsync(cancellationToken);
            return res;
        }
    }
}
