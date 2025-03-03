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
		private readonly IMapper _mapper;

		public DailyLogController(IDailyLogRepository dailyLogRepository, IUserRepository userRepository, IMapper mapper)
		{
			_dailyLogRepository = dailyLogRepository;
			_userRepository = userRepository;
			_mapper = mapper;

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

		[HttpGet("{id}")] // hente en spesifikk log basert på iden dens.
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
			var logs = _mapper.Map<List<DailyLogDto>>(_dailyLogRepository.GetDailyLogsByUser(userId));

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_userRepository.UserExists(userId))
			{
				return NotFound($"User with the ID {userId} not found.");
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



			var DailyMap = _mapper.Map<DailyLog>(createDailyLog);
			DailyMap.User = _userRepository.GetUser(userId);

			if (!_userRepository.UserExists(userId))
			{
				return NotFound($"User with the ID {userId} not found.");
			}

			createDailyLog.Username = DailyMap.User.Username;


			if (!_dailyLogRepository.CreateDailyLog(DailyMap))
			{
				ModelState.AddModelError("", $"Something went wrong saving the daily log for user {userId}");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created dailylog ");

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
			log.TotalFat = updateDailyLog.TotalFat;
			log.TotalProtein = updateDailyLog.TotalProtein;
			log.TotalCarbs = updateDailyLog.TotalCarbs;

			var user = log.User;


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
				ModelState.AddModelError("", $"Something went wrong deleting the daily log for user ");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}


	}
}
