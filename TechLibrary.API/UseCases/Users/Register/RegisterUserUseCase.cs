using FluentValidation.Results;
using TechLibrary.API.Domain.Entities;
using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.API.Infraestructure.Security.Cryptography;
using TechLibrary.API.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.API.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestUserJson request)
        {
            var dbContext = new TechLibraryDbContext();

            Validate(request, dbContext);

            var cryptography = new BCryptAlgorithm();

            var entity = new User
            {
                Email = request.Email,
                Name = request.Name,
                Password = cryptography.CryptographyPassword(request.Password)
            };

            dbContext.Users.Add(entity);
            dbContext.SaveChanges();

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisteredUserJson
            {
                Name = entity.Name,
                AccessToken = tokenGenerator.GenerateToken(entity)
            };
        }

        private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var existUserWithEmail = dbContext.Users.Any(u => u.Email.Equals(request.Email));
            if (existUserWithEmail)
                result.Errors.Add(new ValidationFailure("Email", "Email já registrado na plataforma"));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
