using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Admins.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Admins.Handlers.CommandHandlers
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateAdminCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var salt = Guid.NewGuid().ToString();
            var hashedPassword = request.PasswordHash;

            var admin = new Admin()
            {
                Name = request.Name,
                Email = request.Email,
                Salt = salt,
                PasswordHash = hashedPassword,
                Role = "Admin"
            };

            await _context.Admins.AddAsync(admin, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Created",
                StatusCode = 201,
            };
        }
    }
}
