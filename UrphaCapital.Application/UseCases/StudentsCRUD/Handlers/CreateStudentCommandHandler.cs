using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.Models;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentsCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(CreateStudentsCommand request, CancellationToken cancellationToken)
        {
            var student = new Student()
            {
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                Salt = request.Salt,
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

