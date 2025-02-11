using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : Controller
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public FoodItemController(IFoodItemRepository foodItemRepository, IUserRepository userRepository, IMapper mapper)
        {
            _foodItemRepository = foodItemRepository;
            _userRepository = userRepository;   
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItem>))]

        public IActionResult GetFoodItems()
        {
            var foodItems = _mapper
                .Map<List<FoodItemDto>>(_foodItemRepository.GetFoodItems());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(foodItems);
        }



        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItemDto>))]
        public IActionResult GetFoodItemsByUserId(int userId)
        {
            var items = _foodItemRepository.GetFoodItemsByUserId(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(items);
        }


        [HttpGet("{foodItemId}")]
        [ProducesResponseType(200, Type = typeof(FoodItemDto))]
        public IActionResult GetFoodItem(int foodItemId)
        {
            var foodItem = _mapper
                .Map<FoodItemDto>(_foodItemRepository.GetFoodItem(foodItemId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_foodItemRepository.FoodItemExists(foodItemId))
            {
                return NotFound();
            }

            return Ok(foodItem);
        }

  



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]

        public IActionResult CreateFoodItem([FromQuery] int userId, [FromBody] FoodItemDto createFoodItem)
        {
            if (createFoodItem == null)
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


            var foodMap = _mapper.Map<FoodItem>(createFoodItem);

            foodMap.User = user;

            if (!_foodItemRepository.CreateFoodItem(foodMap))
            {
                ModelState.AddModelError("", $"Something went wrong saving the food item {createFoodItem.FoodName}");
                return StatusCode(500, ModelState);
            }

            return Ok($"Successfully created {createFoodItem.FoodName}");
        }

        [HttpPatch("{foodItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateFoodItem(int foodItemId, int userId, [FromBody] FoodItemDto updateFoodItemDto)
        {
            if (updateFoodItemDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFoodItem = _foodItemRepository.GetFoodItem(foodItemId);
            if (existingFoodItem == null)
            {
                return NotFound($"Food item with the ID {foodItemId} not found.");
            }

            var userExists = _userRepository.UserExists(userId);
            if (!userExists)
            {
                return NotFound($"User with the ID {userId} not found.");
            }

            var foodItem = _mapper.Map<FoodItem>(updateFoodItemDto);
            foodItem.FoodItemId = foodItemId;
            foodItem.UserId = userId;

            if (!_foodItemRepository.UpdateFoodItem(foodItem))
            {
                ModelState.AddModelError("", $"Something went wrong updating the food item {updateFoodItemDto.FoodName}");
                return StatusCode(500, ModelState);
            }

            return Ok($"Successfully updated {updateFoodItemDto.FoodName}");
        }


        [HttpDelete("{foodItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFoodItem(int foodItemId)
        {
            if (!_foodItemRepository.FoodItemExists(foodItemId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foodItemDto = _foodItemRepository.GetFoodItem(foodItemId);
            if (foodItemDto == null)
            {
                return NotFound();
            }

            var foodItem = _mapper.Map<FoodItem>(foodItemDto);

            if (!_foodItemRepository.DeleteFoodItem(foodItem))
            {
                ModelState.AddModelError("", $"Something went wrong deleting food item with ID {foodItemId}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }










    }
}
