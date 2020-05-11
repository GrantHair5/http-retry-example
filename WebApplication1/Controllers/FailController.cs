using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FailController : ControllerBase
    {  // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            Console.Write("I am here");
            return StatusCode(500, "I have failed you");
        }
    }
}