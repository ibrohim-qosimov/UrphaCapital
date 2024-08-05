using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Admins.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Admins.Handlers.CommandHandlers
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAdminCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (admin == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Not found",
                    StatusCode = 404,
                };
            }

            var salt = Guid.NewGuid().ToString();
            var hashedPassword = request.PasswordHash;

            admin.Salt = salt;
            admin.PasswordHash = hashedPassword;
            admin.PhoneNumber = request.PhoneNumber;
            admin.Email = request.Email;
            admin.Name = request.Name;
            
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Updated",
                StatusCode = 201,
            };
        }
    }
}
