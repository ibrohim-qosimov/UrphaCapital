﻿using MediatR;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.UseCases.Admins.Commands;
using UrphaCapital.Application.UseCases.GlobalIdServices;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Admins.Handlers.CommandHandlers
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IGlobalIdService _globalIdService;

        public CreateAdminCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IGlobalIdService globalIdService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _globalIdService = globalIdService;
        }

        public async Task<ResponseModel> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var salt = Guid.NewGuid().ToString();
            var hashedPassword = _passwordHasher.Encrypt(request.PasswordHash, salt);

            var admin = new Admin()
            {
                Id = await _globalIdService.GetNextGlobalIdAsync(),
                Name = request.Name,
                Email = request.Email,
                Salt = salt,
                PasswordHash = hashedPassword,
                PhoneNumber = request.PhoneNumber,
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
