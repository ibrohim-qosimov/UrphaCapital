using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Application.PaymentProcessing
{
    public interface IPaymentService
    {
        public Task<string> ConfirmPaymentAsync(string transactionId);
        public Task<string> PreparePaymentAsync(decimal amount, string transactionParam);
    }
}
