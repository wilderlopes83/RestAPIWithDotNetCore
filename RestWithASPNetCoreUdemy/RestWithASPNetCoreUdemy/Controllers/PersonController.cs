using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;

namespace RestWithASPNetCoreUdemy.Controllers
{    
    [ApiController]
    //[ApiVersion("1")]
    //[Route("api/[controller]/v{version.apiVersion}")]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            this._personBusiness = personBusiness;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/Person/1
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            var person = _personBusiness.FindById(Id);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {            
            if (person == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(_personBusiness.Create(person));
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(_personBusiness.Update(person));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}