using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentsCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public CreateStudentCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseModel> Handle(CreateStudentsCommand request, CancellationToken cancellationToken)
        {

            var salt = Guid.NewGuid().ToString();
            var password = _passwordHasher.Encrypt(request.PasswordHash, salt);

            var student = new Student()
            {
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PasswordHash = password,
                Salt = salt,
                Role = "Student",
                CourseIds = request.CourseIds,
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                Message = "Created",
                StatusCode = 200,
                IsSuccess = true
            };
        }

    }
}

