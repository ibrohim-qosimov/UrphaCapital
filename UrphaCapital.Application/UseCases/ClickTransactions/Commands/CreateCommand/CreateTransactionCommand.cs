using MediatR;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.ClickTransactions.Commands.CreateCommand;
public class CreateTransactionCommand : IRequest<ResponseModel>
{
    public long ClickTransId { get; set; }
    public string MerchantTransId { get; set; }
    public decimal Amount { get; set; }
    public string SignTime { get; set; }
    public int? Situation { get; set; }
    public EOrderPaymentStatus Status { get; set; }
}
