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
    public class GetAdminByEmailQueryHandler : IRequestHandler<GetAdminByEmailQuery, Admin>
    {
        private readonly IApplicationDbContext _context;

        public GetAdminByEmailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> Handle(GetAdminByEmailQuery request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (admin == null)
            {
                throw new Exception();
            }

            return admin;
        }
    }
}
