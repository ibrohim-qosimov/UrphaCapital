using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.IdeasCrud.Queries;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.IdeasCrud.QueryHandlers
{
    public class GetAllIdeasQueryHandler : IRequestHandler<GetAllIdeasQuery, IEnumerable<Ideas>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllIdeasQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ideas>> Handle(GetAllIdeasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Ideass.ToListAsync();
        }
    }
}
