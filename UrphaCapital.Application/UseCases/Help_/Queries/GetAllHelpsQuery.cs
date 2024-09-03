using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Help_.Queries
{
    public class GetAllHelpsQuery: IRequest<IEnumerable<Help>>
    {
    }
}
