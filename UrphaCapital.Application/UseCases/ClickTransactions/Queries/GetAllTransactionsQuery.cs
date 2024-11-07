using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.ClickTransactions.Queries;
public class GetAllTransactionsQuery : IRequest<IEnumerable<TransactionViewModel>>
{
    public long PageIndex { get; set; }
    public long TotalCount { get; set; }
}