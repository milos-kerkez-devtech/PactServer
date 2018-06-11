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

        // GET user/test
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(new UserModel
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
    }
}
