using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;

namespace RestWithASPNetCoreUdemy.Controllers
{   
    
    [ApiVersion("1")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class LoginController : Controller
    {
        private ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            this._loginBusiness = loginBusiness;
        }


        // POST api/Login/teste123
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            try
            {
                if (user==null) return BadRequest();
                return new ObjectResult(_loginBusiness.FindByLogin(user));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }       
    }
}