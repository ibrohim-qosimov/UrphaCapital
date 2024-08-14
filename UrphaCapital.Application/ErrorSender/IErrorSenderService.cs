using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Application.ErrorSender
{
    public interface IErrorSenderService
    {
        public Task SendError(string message, CancellationToken cancellationToken = default);
    }
}
