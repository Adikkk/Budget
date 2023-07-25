using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Budget.API.Controllers
{
    [Route("api/service")]
    [Produces("application/json")]
    public class ServiceController : ControllerBase
    {
        public ServiceController() { }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("");
        }
    }
}
