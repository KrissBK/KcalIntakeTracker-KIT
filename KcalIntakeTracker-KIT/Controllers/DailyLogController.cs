using AutoMapper;
using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DailyLogController : Controller
    {
        private readonly IDailyLogRepository _dailyLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly DbContext _context; //usikker på om dette er riktig

        private readonly IMapper _mapper;

        public DailyLogController(IDailyLogRepository dailyLogRepository, IUserRepository userRepository, IMapper mapper, DbContext context) // Add context parameter
        {
            _dailyLogRepository = dailyLogRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context; //usikker på om dette er riktig
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DailyLog>))]

        public IActionResult GetDailyLogs()
        {
            var dailyLogs = _mapper
                .Map<List<DailyLogDto>>(_dailyLogRepository.GetDailyLogs());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dailyLogs);
        }

        [HttpGet("{id}")] // hvorfor lagde jeg egt denne?
        [ProducesResponseType(200, Type = typeof(IEnumerable<DailyLog>))]

            public IActionResult DailyLogs(int id)
            {
                var log = _dailyLogRepository.DailyLogs()
                    .Include(d => d.User)
                    .FirstOrDefault(d => d.LogId == id);
                if (log == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    log.LogId,
                    log.LogDate,
                    log.TotalCalories,
                    log.User.Username
                });
            }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DailyLogDto>))]
        public IActionResult GetDailyLogsByUserId(int userId)
        {
            var logs = _dailyLogRepository.GetDailyLogsByUserId(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(logs);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]

        public IActionResult CreateDailyLog([FromQuery] int userId, [FromBody] DailyLogDto createDailyLog)
        {
            if (createDailyLog == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return NotFound($" User with the ID {userId} not found.");
            }

            var dailyLog = _mapper.Map<DailyLog>(createDailyLog); // usikker på om dette er riktig
            dailyLog.UserId = user.UserId; // usikker på om dette er riktig
            _context.Entry(user).State = EntityState.Unchanged; // usikker på om dette er riktig

            if (!_dailyLogRepository.CreateDailyLog(dailyLog))
            {
                ModelState.AddModelError("", $"Something went wrong saving the daily log for user {user.Username}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]

        public IActionResult UpdateDailyLog(int id, [FromBody] DailyLogDto updateDailyLog)
        {
            if (updateDailyLog == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var log = _dailyLogRepository.DailyLogs()
                .Include(d => d.User)
                .FirstOrDefault(d => d.LogId == id);
            if (log == null)
            {
                return NotFound();
            }

            log.LogDate = updateDailyLog.LogDate;
            log.TotalCalories = updateDailyLog.TotalCalories;

            if (!_dailyLogRepository.UpdateDailyLog(log))
            {
                ModelState.AddModelError("", $"Something went wrong updating the daily log for user {log.User.Username}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public IActionResult DeleteDailyLog(int id)
        {
            var log = _dailyLogRepository.DailyLogs()
                .Include(d => d.User)
                .FirstOrDefault(d => d.LogId == id);
            if (log == null)
            {
                return NotFound();
            }
            if (!_dailyLogRepository.DeleteDailyLog(log))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the daily log for user {log.User.Username}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }
}
