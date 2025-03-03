using Microsoft.EntityFrameworkCore;
using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.API.Services.LoggedUser;
using TechLibrary.Exception;

namespace TechLibrary.API.UseCases.Checkouts.Register
{
    public class RegisterBookCheckoutUseCase
    {
        private const int MAX_LOAN_DAYS = 7;
        private readonly LoggedUserService _loggedUser;

        public RegisterBookCheckoutUseCase(LoggedUserService loggedUser)
        {
            _loggedUser = loggedUser;
        }

        public void Execute(Guid bookId)
        {
            var dbContext = new TechLibraryDbContext();

            Validate(dbContext, bookId);

            var user = _loggedUser.GetLoggedUser(dbContext);

            var entity = new Domain.Entities.Checkouts
            {
                BookId = bookId,
                UserId = user.Id,
                ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
            };

            dbContext.Checkouts.Add(entity);

            dbContext.SaveChanges();
        }

        private async void Validate(TechLibraryDbContext dbContext, Guid bookId)
        {
            var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book is null)
                throw new NotFoundException("Livro não encontrado. ");

            var amountBooksNotReturned = dbContext.Checkouts
                                            .Count(c => c.BookId == bookId && c.ReturnedDate == null);

            if (amountBooksNotReturned >= book.Amount)
                throw new ConflictException("Livro não está disponível para empréstimo. ");
        }
    }
}
