using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Google_MVC.Controllers.API
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : ControllerBase
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

            // Backup plan
            // 1) This also works
            //var properties = new AuthenticationProperties();
            //properties.RedirectUri = "/googlesignin";
            //properties.Items.Add("LoginProvider", "Google");
            //return Challenge(properties, "Google");
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
}