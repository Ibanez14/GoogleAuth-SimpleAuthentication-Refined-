using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Google_MVC.Controllers.API
{
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        //[HttpGet]
        //[Route("googlesignin")]
        //public IActionResult Get()
        //{
        //    var context = HttpContext;
        //    return Ok(HttpContext.User.Claims.Select(x=> new { x.Type, x.Value}).ToList());
        //}


        [HttpGet]
        public async Task<IActionResult> RegisterExternal()
        {
            // Here we get succees and UserClaims
            var result = await HttpContext.AuthenticateAsync("Cookies");
            return Ok("TempCookies worked out");
        }

    }

}