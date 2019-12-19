using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            return Ok(HttpContext.User.Claims.Select(x=> new { x.Type, x.Value}).ToList());
        }
    }

}