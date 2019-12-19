using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Google_MVC.Controllers.API
{
    [Route("")]
    public class GoogleCallback : ControllerBase
    {
        [HttpGet]
        [Route("googlesignin")]
        public IActionResult Get()
        {
            var context = HttpContext;
            return Ok("Ok.");
        }
    }

}