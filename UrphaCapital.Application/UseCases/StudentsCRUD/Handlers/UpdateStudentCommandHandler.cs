using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateStudentCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseModel> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var res = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (res != null)
            {
                if (request.PasswordHash != null)
                {
                    var salt = Guid.NewGuid().ToString();
                    var password = _passwordHasher.Encrypt(request.PasswordHash, salt);
                    res.PasswordHash = password;
                    res.Salt = salt;
                }

                if (request.FullName != null)
                    res.FullName = request.FullName;
                if (request.Address != null)
                    res.Address = request.Address;
                if (request.PhoneNumber != null)
                    res.PhoneNumber = request.PhoneNumber;
                if (request.Email != null)
                    res.Email = request.Email;


                _context.Students.Update(res);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseModel()
                {
                    Message = "Changes saved!",
                    StatusCode = 200,
                    IsSuccess = true
                };
            }

            return new ResponseModel()
            {
                Message = "Error while updating!",
                StatusCode = 401
            };
        }
    }
}
