﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Selects.Queries;
using UrphaCapital.Application.ViewModels.SelectorModels;

namespace UrphaCapital.Application.UseCases.Selects.Handlers
{
    public class SelectAllStudentsQueryHandler : IRequestHandler<SelectAllStudentsQuery, IEnumerable<StudentSelectModel>>
    {
        private readonly IApplicationDbContext _context;

        public SelectAllStudentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentSelectModel>> Handle(SelectAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.OrderBy(x => x.FullName).Select(x => new StudentSelectModel()
            {
                FullName = x.FullName,
                Id = x.Id,
            }).ToListAsync(cancellationToken);
        }
    }
}
