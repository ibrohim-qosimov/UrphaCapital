using MediatR;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.ClickTransactions.Commands.UpdateCommand;
public class UpdateTransactionCommand : IRequest<ResponseModel>
{
    public long TransactionId { get; set; }
    public EOrderPaymentStatus Status { get; set; }
}