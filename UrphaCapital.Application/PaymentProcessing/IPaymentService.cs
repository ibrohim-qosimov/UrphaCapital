using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Application.PaymentProcessing
{
    public interface IPaymentService
    {
        public Task CreateInvoice(decimal amount, string phoneNumber, string merchantTransId);

        public Task CheckInvoiceStatus(long invoiceId);

        public Task CheckPaymentStatus(long paymentId);

        public Task CheckPaymentStatusByMerchantTransId(string merchantTransId);

        public Task ReversePayment(long paymentId);

        public Task PartialRefund(long paymentId, float amount);

        public Task CreateCardToken(string cardNumber, string expireDate, bool temporary);

        public Task VerifyCardToken(string cardToken, int smsCode);

        public Task MakePaymentWithToken(string cardToken, decimal amount, string merchantTransId);

        public Task DeleteCardToken(string cardToken);
    }
}
