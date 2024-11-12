using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.UseCases.Mentors.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Handlers.CommandHandlers;
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

        if (request.Picture != null)
        {
            var relativePath = mentor.Picture.TrimStart('/');
            var deleteFilePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            if (File.Exists(deleteFilePath))
                File.Delete(deleteFilePath);

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
            mentor.Picture = "/MentorsPictures/" + fileName;
        }

        if (request.Name != null)
            mentor.Name = request.Name;

        if (request.Email != null)
            mentor.Email = request.Email;

        if (request.Description != null)
            mentor.Description = request.Description;

        if (request.PhoneNumber != null)
            mentor.PhoneNumber = request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseModel()
        {
            IsSuccess = true,
            Message = "Updated",
            StatusCode = 201,
        };
    }
}
