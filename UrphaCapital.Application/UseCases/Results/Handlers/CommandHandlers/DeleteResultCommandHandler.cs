using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests.Abstractions;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Results.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Results.Handlers.CommandHandlers
{
    public class DeleteResultCommandHandler(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : IRequestHandler<DeleteResultCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ResponseModel> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Results.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Not found",
                    StatusCode = 404,
                };
            }

            var filePath = Path.Combine("wwwroot", _webHostEnvironment.WebRootPath, result.PictureUrl);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.Results.Remove(result);
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
