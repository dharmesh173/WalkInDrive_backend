using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        [HttpPost("add", Name = "CreateNewUser")]
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

        [HttpPost("tech_fam/add", Name = "AddTechFamilliar")]
        public async Task<ActionResult> AddTechFamilliar([FromBody] technology_familliar tf)
        {
            if (tf == null)
            {
                return BadRequest();
            }

            await _dbContext.technology_famillier.AddAsync(tf);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("tech_exp/add", Name = "AddTechExpertise")]
        public async Task<ActionResult> AddTechExpertise([FromBody] technology_expert te)
        {
            if (te == null)
            {
                return BadRequest();
            }

            await _dbContext.technology_expertise.AddAsync(te);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }



        [HttpPost("edu/add", Name = "AddEduDetails")]
        public async Task<ActionResult> AddEduDetails([FromBody] EducationDetail educationDetail)
        {
            if (educationDetail == null)
            {
                Console.WriteLine("hbch");
                return BadRequest();
            }

            await _dbContext.EducationDetails.AddAsync(educationDetail);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("prof/add", Name = "AddProfDetails")]
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

        

        [HttpPost]
        [Route("{email}", Name = "GetUserByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Object>> GetUserByEmail(String email)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
                BadRequest();
            var res = new { user_id = user.UserId };
            return Ok(res);
        }

    }
}
