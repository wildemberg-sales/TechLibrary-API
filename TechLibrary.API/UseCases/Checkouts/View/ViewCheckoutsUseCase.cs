using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.Communication.Responses;

namespace TechLibrary.API.UseCases.Checkouts.View
{
    public class ViewCheckoutsUseCase
    {
        public ResponseCheckoutsJson Execute(int pageNumber)
        {
            var dbContext = new TechLibraryDbContext();

            var itensPerPage = 10;
            var skip = (pageNumber - 1) * itensPerPage;

            var checkouts = dbContext.Checkouts.OrderBy(c => c.CheckoutDate).Skip(skip).Take(itensPerPage).ToList();

            int totalCount = checkouts.Count();

            return new ResponseCheckoutsJson
            {
                Pagination = new ResponsePaginationJson
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber
                },
                Checkouts = checkouts.Select(c => new ResponseCheckoutJson
                {
                    Id = c.Id,
                    CheckoutDate = c.CheckoutDate,
                    ExpectedReturnDate = c.ExpectedReturnDate,
                    ReturnedDate = c.ReturnedDate,
                    BookId = c.BookId,
                    UserId = c.UserId
                }).ToList()
            };
        }
    }
}
