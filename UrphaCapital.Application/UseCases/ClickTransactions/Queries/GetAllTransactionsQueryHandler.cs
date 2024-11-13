using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.ClickTransactions.Queries;
public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionViewModel>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTransactionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionViewModel>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        // ClickTransactionsni olish va kerakli student va course id'larini ajratib olish
        var transactions = await _context.ClickTransactions
    .Where(t => t.MerchantTransId.Contains(":"))
    .OrderByDescending(t => t.SignTime)
    .ToListAsync(cancellationToken);

        var result = new List<TransactionViewModel>();

        // Barcha studentlarni olish (faqat bir marta so'rov)
        var studentIds = transactions.Select(t => long.TryParse(t.MerchantTransId.Split(":")[1], out long studentId) ? studentId : (long?)null)
            .Where(studentId => studentId.HasValue).Distinct().ToList();

        var students = await _context.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToDictionaryAsync(s => s.Id, s => s.FullName, cancellationToken);

        // Barcha kurslarni olish (faqat bir marta so'rov)
        var courseIds = transactions.Select(t => int.TryParse(t.MerchantTransId.Split(":")[0], out int courseId) ? courseId : (int?)null)
            .Where(courseId => courseId.HasValue).Distinct().ToList();

        var courses = await _context.Courses
            .Where(c => courseIds.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id, c => c.Name, cancellationToken);

        foreach (var transaction in transactions)
        {
            var courseTypeCheck = int.TryParse(transaction.MerchantTransId.Split(":")[0], out int courseId);
            var studentTypeCheck = long.TryParse(transaction.MerchantTransId.Split(":")[1], out long studentId);

            if (!courseTypeCheck || !studentTypeCheck)
                continue;

            // Students va Courses dan mos yozuvni olish (dictionary orqali)
            if (students.TryGetValue(studentId, out var studentName) &&
                courses.TryGetValue(courseId, out var courseName))
            {
                var transactionMap = new TransactionViewModel()
                {
                    StudentId = studentId,
                    StudentName = studentName,
                    Status = transaction.Status.ToString(),
                    PaymentDate = transaction.SignTime,
                    Amount = transaction.Amount,
                    CourseName = courseName,
                    CourseId = courseId,
                };
                result.Add(transactionMap);
            }
        }

        return result;
    }

}
