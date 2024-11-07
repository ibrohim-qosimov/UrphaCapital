using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.StudentsCRUD.Commands;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.ClickTransactions.Commands.UpdateCommand;
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, ResponseModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public UpdateTransactionCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResponseModel> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _context.ClickTransactions
          .Where(c => c.ClickTransId == request.TransactionId && c.Status == EOrderPaymentStatus.Pending)
          .FirstOrDefaultAsync(cancellationToken);

        if (transaction == null)
        {
            return new ResponseModel()
            {
                IsSuccess = false,
                Message = "Not found",
                StatusCode = 404
            };
        }

        if (request.Status == EOrderPaymentStatus.Paid)
        {
            var command = new AddMyCourseCommand()
            {
                CourseId = int.Parse(transaction.MerchantTransId.Split(":")[0]),
                StudentId = long.Parse(transaction.MerchantTransId.Split(":")[1])
            };

            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                Console.WriteLine("!! Diqqat Studentga kurs qoshilmadi.St: {0} Cr: {1} Note :{2}", command.StudentId, command.CourseId, result.Message);
            }
        }

        transaction.Status = request.Status;
        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseModel()
        {
            Message = "Updated",
            IsSuccess = true,
            StatusCode = 200
        };
    }
}
