using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

// Questions

// 1 Send Email ?
// 2 Is Email is confirmed ?
// 2 Password on Google or Facebook ?


namespace Google_MVC.Controllers.API
{
    [Route("api/[controller]/[action]")]
    public partial class ValuesController : ControllerBase
    {
        private readonly IAuthenticationSchemeProvider provider;
        public ValuesController(IAuthenticationSchemeProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Return all schemes registered in startup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllSchemes()
        {
            return Ok(provider.GetAllSchemesAsync().Result);
        }


        /// <summary>
        /// Redirect user to Login action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CookieAuth()
        {
            return Challenge("Cookies");
        }


        /// <summary>
        /// Action for Cookies authentication scheme
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return Ok("Please login...");
        }

    }

    

    public partial class ValuesController:ControllerBase
    {

        /// <summary>
        /// Redirect to acounts.google.com to authenticate a user
        /// And after this, redicrect a request to /googlesignin action in GoogleCallback controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GoogleAuth()
        {
            return Challenge("Google");
            #region Backup 

            // Backup plan
            // 1) This also works
            //var properties = new AuthenticationProperties();
            //properties.RedirectUri = "/googlesignin";
            //properties.Items.Add("LoginProvider", "Google");
            //return Challenge(properties, "Google"); 
            #endregion
        }



        /// <summary>
        /// Same as above
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task GoogleAuth2()
        {
            return HttpContext.ChallengeAsync("Google");
        }

        public Task LogInExternal(string provider)
        {
            return HttpContext.ChallengeAsync(provider, new AuthenticationProperties()
            {
                RedirectUri = "auth/signin"
            });
        }


    }
}