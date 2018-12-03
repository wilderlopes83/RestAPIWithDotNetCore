using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;
using RestWithASPNetCoreUdemy.Data.VO;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNetCoreUdemy.Controllers
{   
    
    [ApiVersion("1")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class BooksController : Controller
    {
        private IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness)
        {
            this._bookBusiness = bookBusiness;
        }

        [HttpGet]
        [Authorize("Bearer")]    
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());            
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]    
        public IActionResult Get(long id)
        {

            var book = Ok(_bookBusiness.FindById(id));
            if (book == null)
            {
                return NotFound();
            }
            
            return Ok(book);
        }
              
        [HttpPost]
        [Authorize("Bearer")]    
        public IActionResult Post([FromBody] BookVO book)
        {            
            if (book == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(_bookBusiness.Create(book));
            }
        }

        [HttpPut]
        [Authorize("Bearer")]    
        public IActionResult Put([FromBody] BookVO book)
        {
            
            if (book == null)
            {
                return BadRequest();
            }

            var updatedBook =  _bookBusiness.Update(book);

            if (updatedBook == null)
            {
                return NoContent();
            }

            return new ObjectResult(updatedBook);
        }

        [HttpDelete("{id}")]
        [Authorize("Bearer")]    
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
    
}