namespace UrphaCapital.Application.ViewModels
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public short StatusCode { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
