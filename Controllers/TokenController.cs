using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalkInDrive.Models;

namespace WalkInDrive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly MydbContext _dbContext;
        public IConfiguration _configuration;

        public TokenController(MydbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<Object>> Post(LoginInfo _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                      _configuration["Jwt:Issuer"],
                      null,
                      expires: DateTime.Now.AddMinutes(120),
                      signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                    

                    var res = new  { tt = token };
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string email, string password)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
#pragma warning restore CS8603 // Possible null reference return.
        }


    }
}
