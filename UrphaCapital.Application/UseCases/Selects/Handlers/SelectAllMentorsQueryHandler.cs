using MediatR;
using Microsoft.EntityFrameworkCore;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Selects.Queries;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Selects.Handlers
{
    public class SelectAllMentorsQueryHandler : IRequestHandler<SelectAllMentorsQuery, IEnumerable<MentorSelectModel>>
    {
        private readonly IApplicationDbContext _context;

        public SelectAllMentorsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MentorSelectModel>> Handle(SelectAllMentorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Mentors
                .OrderBy(m => m.Name)
                    .Select(x => new MentorSelectModel
                    {
                        FullName = x.Name,
                        Id = x.Id
                    }).ToListAsync(cancellationToken);
        }
    }
}
