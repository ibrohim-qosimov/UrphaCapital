using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Payments.Queries
{
    public class GetAllPaymentsQuery: IRequest<IEnumerable<Payment>>
    {
        public int Index {  get; set; }
        public int Count { get; set; }
    }
}
