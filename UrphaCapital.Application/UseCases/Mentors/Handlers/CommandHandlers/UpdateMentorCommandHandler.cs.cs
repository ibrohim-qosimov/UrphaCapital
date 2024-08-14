using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Application.UseCases.Mentors.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.CommandHandlers
{
    public class UpdateMentorCommandHandler : IRequestHandler<UpdateMentorCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateMentorCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IPasswordHasher passwordHasher)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseModel> Handle(UpdateMentorCommand request, CancellationToken cancellationToken)
        {
            var mentor = await _context.Mentors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (mentor == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Not found",
                    StatusCode = 404,
                };
            }

            File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, mentor.Picture));

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

            mentor.Name = request.Name;
            mentor.Description = request.Description;
            mentor.Email = request.Email;
            mentor.PhoneNumber = request.PhoneNumber;
            mentor.Picture = "/MentorsPictures/" + fileName;
            mentor.PasswordHash = hashedPassword;
            mentor.Salt = salt;

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
