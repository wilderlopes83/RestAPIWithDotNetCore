using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;
using RestWithASPNetCoreUdemy.Data.VO;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        //comando conforme o curso no Udemy, mas API foi atualizada e comando modificado para ProducesResponseType...
        //[SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        //////
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]    
        [Authorize("Bearer")]    
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_personBusiness.FindAll());
        }

        [HttpGet("find-by-name")]
        //comando conforme o curso no Udemy, mas API foi atualizada e comando modificado para ProducesResponseType...
        //[SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        //////
        [ProducesResponseType(typeof(List<PersonVO>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]    
        [Authorize("Bearer")]    
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByName([FromQuery] string firstName, string lastName)
        {
            return new OkObjectResult(_personBusiness.FindByName(firstName, lastName));
        }

        // GET api/Person/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonVO), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]    
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
        [ProducesResponseType(typeof(PersonVO), 201)]                
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]       
        [Authorize("Bearer")]     
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
        [ProducesResponseType(typeof(PersonVO), 202)]                
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]       
        [Authorize("Bearer")]     
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
        [ProducesResponseType(204)]                
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]    
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}