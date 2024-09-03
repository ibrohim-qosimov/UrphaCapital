using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Help_.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Help_.QueryHandlers
{
    public class GetAllHelpsQueryHandler : IRequestHandler<GetAllHelpsQuery, IEnumerable<Help>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllHelpsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Help>> Handle(GetAllHelpsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Helps.ToListAsync(cancellationToken);
        }
    }
}
