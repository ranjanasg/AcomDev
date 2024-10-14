using AcomDev.Features;
using AcomDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcomDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpGet("getusersbycompanyid")]
        public async Task<IActionResult> GetUsersByCompanyId(string CompanyId)
        {
            var users = await _userService.GetUsersByCompanyId(CompanyId);
            return Ok(users);
        }        

        [HttpGet("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Password == _userService.ComputeSha256HashWithSalt(password, "AcomDev"))
            {
                return Ok(user);
            }
            else
            {
                return Ok("Invalid password");
            }
        }

        [HttpGet("getuserbyemail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _userService.AddUserAsync(user);
            return Ok(createdUser);
        }
    }
}
