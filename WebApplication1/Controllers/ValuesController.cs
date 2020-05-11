using HttpClientHelper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpCaller _caller;

        public ValuesController(IHttpCaller caller)
        {
            _caller = caller;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _caller.CallFailApi();

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}