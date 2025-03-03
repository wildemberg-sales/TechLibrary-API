using Microsoft.AspNetCore.Mvc;
using TechLibrary.API.UseCases.Login.DoLogin;
using TechLibrary.Communication.Requests;

namespace TechLibrary.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult DoLogin(RequestLoginJson request)
        {
            var useCase = new DoLoginUseCase();
            var response = useCase.Execute(request);

            return Ok(response);
        }
    }
}
