using MediatR;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.ClickTransactions.Commands.CreateCommand;
public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ResponseModel>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var clickTransaction = new ClickTransaction
            {
                ClickTransId = request.ClickTransId,
                MerchantTransId = request.MerchantTransId,
                Amount = request.Amount,
                SignTime = request.SignTime,
                Status = EOrderPaymentStatus.Pending,
            };

            _context.ClickTransactions.Add(clickTransaction);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                Message = "Created",
                IsSuccess = true,
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel()
            {
                Message = "Error while creating",
                IsSuccess = false,
                StatusCode = 500
            };
        }

    }
}
