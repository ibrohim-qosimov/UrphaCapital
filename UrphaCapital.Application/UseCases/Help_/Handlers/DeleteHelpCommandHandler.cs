using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Help_.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Help_.Handlers
{
    public class DeleteHelpCommandHandler : IRequestHandler<DeleteHelpCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteHelpCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteHelpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var help = await _context.Helps.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (help == null)
                    throw new Exception();

                _context.Helps.Remove(help);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseModel()
                {
                    Message = "Deleted",
                    StatusCode = 200,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Message = "Error",
                    StatusCode = 200,
                    IsSuccess = false
                };
            }
        }
    }
}
