using BookstoreSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            return Ok();
        }
/*        [HttpGet]
        public ActionResult<List<Book>> GetByTitle(string title)
        {
            return Ok();
        }*/
    }
}
