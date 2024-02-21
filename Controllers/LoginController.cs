using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalkInDrive.Models;

namespace WalkInDrive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MydbContext _dbContext;

        public LoginController(MydbContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpPost("user/Add", Name = "CreateNewUser")]
        public async Task<ActionResult> CreateNewUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("user/edu/add", Name = "AddEduDetails")]
        public async Task<ActionResult> AddEduDetails([FromBody] EducationDetail educationDetail)
        {
            if (educationDetail == null)
            {
                return BadRequest();
            }

            await _dbContext.EducationDetails.AddAsync(educationDetail);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("user/prof/add", Name = "AddProfDetails")]
        public async Task<ActionResult> AddProfDetails([FromBody] ProfessionalDetail professionalDetail)
        {
            if (professionalDetail == null)
            {
                return BadRequest();
            }

            await _dbContext.ProfessionalDetails.AddAsync(professionalDetail);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("users_education_details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EducationDetail>>> GetAllUsersEduDetails()
        {
            var users_edu = await _dbContext.EducationDetails.ToListAsync();

            if (users_edu == null)
            {
                BadRequest();
            }

            return Ok(users_edu);
        }

        [HttpGet]
        [Route("users_professional_details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProfessionalDetail>>> GetAllUsersProfDetails()
        {
            var users_prof = await _dbContext.ProfessionalDetails.ToListAsync();

            if (users_prof == null)
            {
                BadRequest();
            }

            return Ok(users_prof);
        }

        [HttpGet]
        [Route("{Id}", Name = "GetUserByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Object>> GetUserByUserid(int Id)
        {
            var user = await _dbContext.Users.Where(u => u.UserId == Id).FirstOrDefaultAsync();
            var userEdu = await _dbContext.Users.Where(e => e.UserId == Id).FirstOrDefaultAsync();
            var userprof = await _dbContext.Users.Where(p => p.UserId == Id).FirstOrDefaultAsync();

            if (user == null || userEdu == null || userprof == null)
                BadRequest();

            return Ok(new {user, userEdu, userprof});
        }

        [HttpPost]
        [Route("{email}", Name = "GetUserByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<User>> GetUserByEmail(String email)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
                BadRequest();
             
            return Ok(user);
        }

    }
}
