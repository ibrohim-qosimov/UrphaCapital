using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Results.Queries
{
    public class GetResultsQuery : IRequest<IEnumerable<Result>>
    {
        public short Size { get; set; }
    }
}
