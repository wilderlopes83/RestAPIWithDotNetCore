using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;
using RestWithASPNetCoreUdemy.Data.VO;

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

        [HttpGet("v1")]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());            
        }

        [HttpGet("v1/{id}")]
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
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
    
}