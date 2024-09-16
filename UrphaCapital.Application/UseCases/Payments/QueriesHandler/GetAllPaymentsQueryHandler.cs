using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Payments.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Payments.QueriesHandler
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<Payment>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllPaymentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Paymentss.ToListAsync(cancellationToken);
        }
    }
}
