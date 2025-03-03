using TechLibrary.API.Domain.Entities;

namespace TechLibrary.API.Infraestructure.Security.Cryptography
{
    public class BCryptAlgorithm
    {
        public string CryptographyPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Verify(string password, User user) => BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}
