using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Queries
{
    public class GetAllMentorsQuery : IRequest<IEnumerable<Mentor>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
