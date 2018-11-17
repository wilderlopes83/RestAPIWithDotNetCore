using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNetCoreUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET api/Person/1
        [HttpGet("{id}")]
        public ActionResult<string> Sum(string firstNumber, string secondNumber)
        {
            //
        }
    }
}