using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.UseCases.Mentors.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.CommandHandlers
{
    public class CreateMentorCommandHandler : IRequestHandler<CreateMentorCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPasswordHasher _passwordHasher;

        public CreateMentorCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IPasswordHasher passwordHasher)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseModel> Handle(CreateMentorCommand request, CancellationToken cancellationToken)
        {
            var file = request.Picture;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "MentorsPictures");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "MentorsPictures", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    IsSuccess = false
                };
            }

            var salt = Guid.NewGuid().ToString();
            var hashedPassword = _passwordHasher.Encrypt(request.PasswordHash, salt);

            var mentor = new Mentor()
            {
                Name = request.Name,
                Description = request.Description,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Picture = "/MentorsPictures/" + fileName,
                PasswordHash = hashedPassword,
                Salt = salt,
                Role = "Mentor"
            };

            await _context.Mentors.AddAsync(mentor, cancellationToken);
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
