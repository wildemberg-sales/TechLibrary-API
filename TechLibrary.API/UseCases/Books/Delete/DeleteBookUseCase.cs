using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.Exception;

namespace TechLibrary.API.UseCases.Books.Delete
{
    public class DeleteBookUseCase
    {
        public void Delete(Guid bookId)
        {
            var dbContext = new TechLibraryDbContext();

            var book = dbContext.Books.FirstOrDefault(b => b.Id == bookId);

            if (book is null)
                throw new NotFoundException("Nenhum livro encontrado. ");

            dbContext.Remove(book);
            dbContext.SaveChanges();
        }
    }
}
