namespace UrphaCapital.Application.ExternalServices.HasherServices
{
    public interface IPasswordHasher
    {
        public string Encrypt(string password, string salt);
        public bool Verify(string hash, string password, string salt);
    }
}
