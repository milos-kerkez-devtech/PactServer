using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PactServer.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return new JsonResult(new
            {
                Id = "Test",
                FirstName = "Milos",
                LastName = "Kerkez"
            });
            //var user = _userService.GetUser(id);

            //if (user != null)
            //{
            //    return Ok(user);
            //}
            //return NotFound();
        }

        private bool DataMissing()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"C:/DevTech/Repos/pacts/data");
            string pathWithFile = Path.Combine(path, "somedata.txt");

            return !System.IO.File.Exists(pathWithFile);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
