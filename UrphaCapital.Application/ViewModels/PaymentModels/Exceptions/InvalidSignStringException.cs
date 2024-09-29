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
