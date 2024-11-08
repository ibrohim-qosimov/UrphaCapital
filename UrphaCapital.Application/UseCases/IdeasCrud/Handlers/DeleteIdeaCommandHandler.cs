using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.IdeasCrud.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Handlers
{
    public class DeleteIdeaCommandHandler : IRequestHandler<DeleteIdeaCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteIdeaCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private readonly IWebHostEnvironment _webHostEnvironment;
        public async Task<ResponseModel> Handle(DeleteIdeaCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Ideass.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Not found",
                    StatusCode = 404,
                };
            }

            var relativePath = result.PictureUrl.TrimStart('/');
            var deleteFilePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            if (File.Exists(deleteFilePath))
                File.Delete(deleteFilePath);

            _context.Ideass.Remove(result);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                Message = "Deleted",
                StatusCode = 201,
            };
        }
    }
}
