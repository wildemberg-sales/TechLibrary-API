using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.API.Domain.Entities;
using TechLibrary.API.UseCases.Books.Create;
using TechLibrary.API.UseCases.Books.Delete;
using TechLibrary.API.UseCases.Books.Filter;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet("Filter")]
        [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Filter(int pageNumber, string? title)
        {
            var useCase = new FilterBookUseCase();

            var result = useCase.Execute(new RequestFilterBooksJson
            {
                PageNumber = pageNumber,
                Title = title
            });

            if (result.Books.Count > 0)
                return Ok(result);

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult CreateBook(RequestCreateBookJson request)
        {
            var useCase = new CreateBookUseCase();
            var response = useCase.Create(request);

            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{bookId}")]
        [Authorize]
        public IActionResult DeleteBook(Guid bookId)
        {
            var useCase = new DeleteBookUseCase();
            useCase.Delete(bookId);

            return NoContent();
        }
    }
}
