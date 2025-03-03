using System.IdentityModel.Tokens.Jwt;
using TechLibrary.API.Domain.Entities;
using TechLibrary.API.Infraestructure.DataAccess;

namespace TechLibrary.API.Services.LoggedUser
{
    public class LoggedUserService
    {
        private readonly HttpContext _httpContext;
        public LoggedUserService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }
    
        public User GetLoggedUser(TechLibraryDbContext dbContext)
        {
            var authentication = _httpContext.Request.Headers.Authorization.ToString();
            var token = authentication["Bearer ".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;

            var userId = Guid.Parse(identifier);

            var user = dbContext.Users.First(u => u.Id == userId);

            return user;
        }
    }
}
