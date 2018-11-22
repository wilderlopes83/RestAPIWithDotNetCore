using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;
using RestWithASPNetCoreUdemy.Data.VO;

namespace RestWithASPNetCoreUdemy.Controllers
{   
    
    [ApiVersion("1")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
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
        public IActionResult Post([FromBody] PersonVO person)
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
        public IActionResult Put([FromBody] PersonVO person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }

            var updatedPerson =  _personBusiness.Update(person);

            if (updatedPerson == null)
            {
                return NoContent();
            }

            return new ObjectResult(updatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}