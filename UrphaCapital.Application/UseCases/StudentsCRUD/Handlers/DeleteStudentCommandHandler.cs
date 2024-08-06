using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Handlers
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStudentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (student == null)
                    throw new Exception();


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
