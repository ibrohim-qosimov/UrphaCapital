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
    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, Admin>
    {
        private readonly IApplicationDbContext _context;

        public GetAdminByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.Id == request.Id) ??
                 throw new Exception("Not found");
        }
    }
}
