using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : Controller
    {
        private readonly IFoodItemRepository _foodItemRepository;

        public FoodItemController(IFoodItemRepository foodItemRepository)
        {
            _foodItemRepository = foodItemRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItem>))]

        public IActionResult GetFoodItems()
        {
            var foodItems = _foodItemRepository.GetFoodItems();
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


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItem>))]
        public IActionResult GetFoodItems(int id)
        {
            var foodItem = _foodItemRepository.GetFoodItem()
                .FirstOrDefault(f => f.FoodItemId == id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return Ok(foodItem);
        }
    }
}
