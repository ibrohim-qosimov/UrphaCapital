using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Application.UseCases.Lessons.Queries
{
    public class GetLessonVideoQuery : IRequest<Stream?>
    {
        public string Id { get; set; }
    }
}
