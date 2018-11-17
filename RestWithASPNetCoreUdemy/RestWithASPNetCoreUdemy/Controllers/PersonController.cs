using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Services.Implementation;

namespace RestWithASPNetCoreUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/Person/1
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            var person = _personService.FindById(Id);
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
                return new ObjectResult(_personService.Create(person));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Person person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(_personService.Update(person));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }

    }
}