using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.Exception;

namespace TechLibrary.API.UseCases.Checkouts.ReturnBook
{
    public class ReturnedBookCheckoutUseCase
    {
        public void Execute(Guid checkoutId)
        {
            var dbContext = new TechLibraryDbContext();

            var checkout = dbContext.Checkouts.FirstOrDefault(c => c.Id == checkoutId);

            if (checkout is null)
                throw new NotFoundException("Não foi encontrado nenhum checkout. ");

            checkout.ReturnedDate = DateTime.UtcNow;

            dbContext.Update(checkout);
            dbContext.SaveChanges();
        }
    }
}
