using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Homework.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Handlers
{
    public class DeleteHomeworkCommandHandler : IRequestHandler<DeleteHomeworkCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteHomeworkCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteHomeworkCommand request, CancellationToken cancellationToken)
        {
            var hw = await _context.Homeworks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hw == null)
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Homework not found!",
                    StatusCode = 404
                };

            _context.Homeworks.Remove(hw);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Homework successfully deleted"
            };
        }
    }
}
