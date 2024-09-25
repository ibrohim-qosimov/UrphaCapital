using MediatR;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Help_.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Help_.Handlers
{
    public class CreateHelpCommandHandler : IRequestHandler<CreateHelpCommand, ResponseModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateHelpCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel> Handle(CreateHelpCommand request, CancellationToken cancellationToken)
        {
            var help = new Help()
            {
                FullName = request.FullName,
                Address = request.Address,
                Email = request.Email,
                CourseType = request.CourseType,
            };
            await _context.Helps.AddAsync(help);
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
