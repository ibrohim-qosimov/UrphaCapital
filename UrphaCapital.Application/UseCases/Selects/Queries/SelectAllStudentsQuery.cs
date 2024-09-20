using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Selects.Queries
{
    public class SelectAllStudentsQuery : IRequest<IEnumerable<StudentSelectModel>>
    {
    }
}
