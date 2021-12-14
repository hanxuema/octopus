using System; 
using Microsoft.AspNetCore.Mvc;

namespace AWSServerless1.Controllers
{
    [Route("api/Ping")]
    public class PingController : ControllerBase
    {
        // GET api/Ping
        [HttpGet] 
        public String Get()
        {
            Console.WriteLine("getting Ping, returning Pong");
            return "Pong";
        }
    }
}
