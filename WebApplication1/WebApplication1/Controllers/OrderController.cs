using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data;

namespace ShopAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Add CORS policy name
    [EnableCors("AllowAll")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private AppDbContext _context;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var allOrders = _context.Orders.ToList();
            return Ok(allOrders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var orderById = _context.Orders.FirstOrDefault(n => n.Id == id);

            if (orderById == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(orderById);
            }
        }

        [HttpGet("TotalOrdersSummary")]
        public IActionResult GetTotalOrdersSummary()
        {
            var totalOrders = _context.Orders.Count();

            return Ok($"Total orders = {totalOrders}");
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            var newOrder = new Order()
            {
                Email = orderDto.Email,
                ProducName = orderDto.ProductName,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return Ok(orderDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            return Ok("UpdateOrder");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            return Ok($"Deleted order with id {id}");
        }
    }
}