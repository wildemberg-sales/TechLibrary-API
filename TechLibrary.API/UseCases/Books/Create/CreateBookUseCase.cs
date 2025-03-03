using TechLibrary.API.Domain.Entities;
using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Exception;

namespace TechLibrary.API.UseCases.Books.Create
{
    public class CreateBookUseCase
    {
        public Book Create(RequestCreateBookJson request)
        {
            var dbContext = new TechLibraryDbContext();

            Validate(request);

            var entity = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Amount = request.Amount
            };

            dbContext.Books.Add(entity);
            dbContext.SaveChanges();

            return entity;
        }

        private void Validate(RequestCreateBookJson request)
        {
            var validator = new CreateBookValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erros);
            }
        }
    }
}
