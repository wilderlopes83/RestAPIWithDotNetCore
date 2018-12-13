using Microsoft.AspNetCore.Mvc;
using RestWithASPNetCoreUdemy.Model;
using RestWithASPNetCoreUdemy.Business;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestWithASPNetCoreUdemy.Controllers
{   
    
    [ApiVersion("1")]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class FileController : Controller
    {
        private IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            this._fileBusiness = fileBusiness;
        }


        // POST api/Login/teste123
        [HttpGet]
        [ProducesResponseType(typeof(byte[]), 200)]        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult GetPDFFile()
        {
           byte[] buffer = _fileBusiness.getPDFFile();

           if (buffer != null)
           {
               HttpContext.Response.ContentType = "application/pdf";
               HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
               HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
           }

           return new ContentResult();
        }       
        
    }
}