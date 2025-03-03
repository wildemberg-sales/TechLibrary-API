using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.API.Infraestructure.Security.Cryptography;
using TechLibrary.API.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.API.UseCases.Login.DoLogin
{
    public class DoLoginUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestLoginJson request)
        {
            var dbContext = new TechLibraryDbContext();

            var user = dbContext.Users.FirstOrDefault(u => u.Email.Equals(request.Email));

            if (user is null)
                throw new InvalidLoginException();

            var cryptography = new BCryptAlgorithm();
            var passwordIsValid = cryptography.Verify(request.Password, user);
            if (!passwordIsValid)
                throw new InvalidLoginException();

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                AccessToken = tokenGenerator.GenerateToken(user)
            };
        }
    }
}
