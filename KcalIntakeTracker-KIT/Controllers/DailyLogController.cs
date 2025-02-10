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

        public DailyLogController(IDailyLogRepository dailyLogRepository)
        {
            _dailyLogRepository = dailyLogRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DailyLog>))]

        public IActionResult GetDailyLogs()
        {
            var dailyLogs = _dailyLogRepository.GetDailyLogs();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(dailyLogs);
        }

        [HttpGet("{id}")]
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

    }
}
