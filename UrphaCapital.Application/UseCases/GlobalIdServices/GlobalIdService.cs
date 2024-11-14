using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.GlobalIdServices
{
    public class GlobalIdService : IGlobalIdService
    {
        private readonly IApplicationDbContext _context;

        public GlobalIdService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> GetNextGlobalIdAsync()
        {
            // Tranzaksiya bilan ID ketma-ketlikda oshirish
            using (var transaction = await _context.BeginTransactionAsync())
            {
                var globalId = _context.GlobalIds.FirstOrDefault();
                if (globalId == null)
                {
                    globalId = new GlobalID { Id = 1, ForUsersid = 2 };
                    _context.GlobalIds.Add(globalId);
                }
                else
                {
                    globalId.ForUsersid += 1;
                }

                await _context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();

                return globalId.ForUsersid;
            }
        }
    }
}
