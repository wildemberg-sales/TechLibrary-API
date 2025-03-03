using TechLibrary.API.Domain.Entities;
using TechLibrary.API.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.API.UseCases.Books.Filter
{
    public class FilterBookUseCase
    {
        private const int ItensPerPage = 10;

        public ResponseBooksJson Execute(RequestFilterBooksJson request)
        {

            var context = new TechLibraryDbContext();

            var skip = ItensPerPage * (request.PageNumber - 1);
             
            IQueryable<Book> query = context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(request.Title))
                query = context.Books.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));

            var books = query
                        .OrderBy(b => b.Title).ThenBy(b => b.Author)
                        .Skip(skip)
                        .Take(ItensPerPage)
                        .ToList();

            int totalCount = books.Count();

            return new ResponseBooksJson
            {
                Pagination = new ResponsePaginationJson
                {
                    PageNumber = request.PageNumber,
                    TotalCount = totalCount
                },
                Books = books.Select(b => new ResponseBookJson
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author
                }).ToList()
            };
        }
    }
}
