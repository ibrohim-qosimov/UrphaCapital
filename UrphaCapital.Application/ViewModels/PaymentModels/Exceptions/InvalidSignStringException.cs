using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Application.ViewModels.PaymentModels.Exceptions
{
    public class InvalidSignStringException : Exception
    {
        public InvalidSignStringException()
            : base("Invalid sign string") { }
        public InvalidSignStringException(string message)
            : base(message) { }
    }
}
