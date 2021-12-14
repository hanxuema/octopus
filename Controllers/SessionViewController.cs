using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Http.Description;

namespace AWSServerless1.Controllers
{
    [Route("api/SessionView")]
    public class SessionViewController : ControllerBase
    {
        // GET api/SessionView
        [HttpGet]
        [ResponseType(typeof(SessionViewViewModel))]
        public ActionResult<SessionViewViewModel> Get()
        {  
            var response = new SessionViewViewModel()
            {
                DisplayFamilyName = "Xie",
                DisplayGiveName = "Xavier",
                Internal = false,
                SessionId = Guid.NewGuid().ToString(),
                LastLoggedInDateTime = DateTime.UtcNow.ToString(),
            };

            Console.WriteLine($"response is {Newtonsoft.Json.JsonConvert.SerializeObject(response)}"); 

            return response;
        }

        #region
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Console.WriteLine("hahaha");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion
    }
}
