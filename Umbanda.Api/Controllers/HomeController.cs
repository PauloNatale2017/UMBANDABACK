using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Umbanda.Api.Controllers
{
       

    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        public HomeController()
        {

        }


        [HttpGet]
        [Route("version")]    
        public async Task<IActionResult> Version()
        {
            return Ok("");

        }
    }
}
