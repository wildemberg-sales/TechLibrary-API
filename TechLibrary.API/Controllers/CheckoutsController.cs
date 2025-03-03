using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.API.Services.LoggedUser;
using TechLibrary.API.UseCases.Checkouts.Register;
using TechLibrary.API.UseCases.Checkouts.ReturnBook;
using TechLibrary.API.UseCases.Checkouts.View;
using TechLibrary.Communication.Responses;

namespace TechLibrary.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutsController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")]
        public IActionResult BookCheckout(Guid bookId)
        {
            var loggedUser = new LoggedUserService(HttpContext);

            var useCase = new RegisterBookCheckoutUseCase(loggedUser);
            useCase.Execute(bookId); 

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseCheckoutsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetBooksCheckouts(int pageNumber)
        {
            var useCase = new ViewCheckoutsUseCase();
            var result = useCase.Execute(pageNumber);

            if (result.Checkouts.Count > 0)
                return Ok(result);

            return NoContent();
        }

        [HttpPut]
        [Route("{checkoutId}")]
        public IActionResult ReturnedBookCheckout(Guid checkoutId)
        {
            var useCase = new ReturnedBookCheckoutUseCase();
            useCase.Execute(checkoutId);

            return NoContent();
        }
    }
}
