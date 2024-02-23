using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WalkInDrive.Models;

namespace WalkInDrive.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrivesController : ControllerBase
    {
        private readonly MydbContext _dbContext;

        public DrivesController(MydbContext dbContext)
        {
            _dbContext = dbContext;
        }
      
        [HttpGet]
        [Route("techs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Technology>>> GetAllTechnology()
        {
            var technologies = await _dbContext.Technologies.ToListAsync();
           
            if(technologies == null)
            {
                BadRequest();
            }
            
            return Ok(technologies);
        }
        
        [HttpGet]
        [Route("drives")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<WalkInDrife>>> GetAllDrives()
        {
            var drives = await _dbContext.WalkInDrives.ToListAsync();

            if (drives == null)
            {
                BadRequest();
            }

            return Ok(drives);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<WalkInDrife> GetDriveById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var drive =  _dbContext.WalkInDrives.Where(drive => drive.DriveId == id).FirstOrDefault();

            return Ok(drive);
        }

        [HttpGet]
        [Route("timeslots")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Slot>> GetAllTimeSlots()
        {
            var slots = await _dbContext.Slots.ToListAsync();

            if(slots == null)
            {
                return BadRequest();
            }

            return Ok(slots);
        }

        [HttpGet]
        [Route("timeslotsbydriveId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DriveAvailableSlot>> DrivesHasSlots(int id)
        {
            var slots = await _dbContext.DriveAvailableSlots.Where(tp => tp.WalkInDrivesDriveId == id).ToListAsync();

            if (slots == null)
            {
                return BadRequest();
            }

            return Ok(slots);
        }

        [HttpGet]
        [Route("{Name}", Name = "GetStudentByName")]
        public ActionResult<WalkInDrife> GetDriveByName(string Name)
        {
            return _dbContext.WalkInDrives.Where(n => n.DriveTitle == Name).FirstOrDefault();
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            if (users == null)
            {
                BadRequest();
            }

            return Ok(users);
        }

        
        [HttpGet]
        [Route("All_Applied_Drive_By_User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DriveApplied>>> getalldriveappliedbyuser()
        {
            var users_prof = await _dbContext.DriveApplieds.ToListAsync();

            if (users_prof == null)
            {
                BadRequest();
            }

            return Ok(users_prof);
        }

        [HttpGet]
        [Route("roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Role>>> getAllRoles()
        {
            var roles = await _dbContext.Roles.ToListAsync();

            if (roles == null)
            {
                BadRequest();
            }

            return Ok(roles);
        }

        [HttpGet]
        [Route("prerequisite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PreRequisite>>> GetPrerequisite()
        {
            var prerequisite = await _dbContext.PreRequisites.ToListAsync();

            if (prerequisite == null)
            {
                BadRequest();
            }

            return Ok(prerequisite);
        }

        [HttpGet]
        [Route("driveapplied")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DriveApplied>>> getDriveApplied()
        {
            var prerequisite = await _dbContext.DriveApplieds.ToListAsync();

            if (prerequisite == null)
            {
                BadRequest();
            }

            return Ok(prerequisite);
        }

        [HttpGet]
        [Route("driveappliedhasRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<drive_applied_has_job_roles>>> driveappliedhasRoles()
        {
            var prerequisite = await _dbContext.roles_has_drive_applied.ToListAsync();

            if (prerequisite == null)
            {
                BadRequest();
            }

            return Ok(prerequisite);
        }

        [HttpGet]
        [Route("rolesbyid/{id}", Name = "GetDriveRolesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetDriveRolessById(int id)
        {
             var rolesIds =await  _dbContext.drives_has_roles.Where(n => n.walkin_drive_id == id).ToListAsync();
            /*
            var rolesIds = await _dbContext.WalkInDrives.Include(t => t.Roles).ToListAsync();
            */
            if (rolesIds == null)
            {
                BadRequest();
            }



            return Ok(rolesIds);
        }

        [HttpGet]
        [Route("slotnamebyid/{id}", Name = "slotnamebyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> slotnamebyid(int id)
        {
            var slot = await _dbContext.Slots.Where(n => n.Id == id).ToListAsync();
            /*
            var rolesIds = await _dbContext.WalkInDrives.Include(t => t.Roles).ToListAsync();
            */
            if (slot == null)
            {
                BadRequest();
            }



            return Ok(slot);
        }

        [HttpPost("drive/driveapplied/Add", Name = "AddtoDriveapplied")]
        public async Task<ActionResult> AddtoDriveapplied([FromBody] DriveApplied driveofuser)
        {
            if (driveofuser == null)
            {
                return BadRequest();
            }

            await _dbContext.DriveApplieds.AddAsync(driveofuser);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("drive/driveappliedjobroles/Add", Name = "AddtoDriveappliedjobrole")]
        public async Task<ActionResult> AddtoDriveappliedjobrole([FromBody] drive_applied_has_job_roles jobroleofapplieddrive)
        {
            if (jobroleofapplieddrive == null)
            {
                return BadRequest();
            }

            await _dbContext.roles_has_drive_applied.AddAsync(jobroleofapplieddrive);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }






    }
}
