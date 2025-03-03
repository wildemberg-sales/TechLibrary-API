using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechLibrary.API.Domain.Entities;

namespace TechLibrary.API.Infraestructure.Security.Tokens.Access
{
    public class JwtTokenGenerator
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            //descreve os dados do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(60),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            // usa um handler para criar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            // retorna como string
            return tokenHandler.WriteToken(securityToken);
        }

        public SymmetricSecurityKey SecurityKey()
        {
            var signingKey = "çalskdjfpqowieurçalskdjfDzZxmcnv";
            var symmmetricKey = Encoding.UTF8.GetBytes(signingKey);

            return new SymmetricSecurityKey(symmmetricKey);
        }
    }
}
