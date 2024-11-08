using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.IdeasCrud.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Handlers
{
    public class CreateIdeaCommandHandler : IRequestHandler<CreateIdeaCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateIdeaCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private readonly IWebHostEnvironment _webHostEnvironment;
        public async Task<ResponseModel> Handle(CreateIdeaCommand request, CancellationToken cancellationToken)
        {
            var file = request.Picture;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "IdeaPhotos");
            string fileName = "";

            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Debug.WriteLine("Directory created successfully.");
                }

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "IdeaPhotos", fileName);
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

            var result = new Result()
            {
                PictureUrl = "/IdeaPhotos/" + fileName,
                Title = request.Title,
                Description = request.Description
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                Message = "Created",
                StatusCode = 201,
                IsSuccess = true
            };
        }
    }
}
