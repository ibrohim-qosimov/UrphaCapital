using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Admins.Queries;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Admins.Handlers.QueryHandlers
{
    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, IEnumerable<Admin>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllAdminsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Admins
                .Skip(request.Index - 1)
                    .Take(request.Count)
                        .ToListAsync(cancellationToken);
        }
    }
}
