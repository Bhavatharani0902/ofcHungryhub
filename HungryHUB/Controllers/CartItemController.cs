using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using Microsoft.AspNetCore.Mvc;

namespace HungryHUB.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartItemController : ControllerBase
    {
        private readonly ILogger<CartItemController> _logger;
        private readonly IMapper _mapper;
        private readonly ICartItemService _cartItemService;

        public CartItemController(ILogger<CartItemController> logger, IMapper mapper, ICartItemService cartItemService)
        {
            _logger = logger;
            _mapper = mapper;
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public ActionResult<List<CartItemDTO>> GetAllCartItems()
        {
            try
            {
                var cartItems = _cartItemService.GetAllCartItems();
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving cart items: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("{Add}")]
        public ActionResult AddCartItem([FromBody] CartItemDTO cartItemDTO)
        {
            try
            {
                var cartItem = _mapper.Map<CartItem>(cartItemDTO);
                _cartItemService.AddCartItem(cartItem);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding cart item: {ex}");

                // Log inner exception details
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException}");
                }

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("{cartItemName}")]
        public ActionResult<CartItem> GetCartItemByName(string cartItemName)
        {
            try
            {
                var cartItem = _cartItemService.GetCartItemByName(cartItemName);
                if (cartItem != null)
                    return Ok(cartItem);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving cart item by name: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("user/{username}")]
        public ActionResult<List<CartItem>> GetCartItemsByUser(string username)
        {
            try
            {
                var cartItems = _cartItemService.GetCartItemsByUser(username);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving cart items by user: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{cartItemName}")]
        public ActionResult RemoveCartItem(string cartItemName)
        {
            try
            {
                _cartItemService.RemoveCartItem(cartItemName);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error removing cart item: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{cartItemName}")]
        public ActionResult UpdateCartItem(string cartItemName, [FromBody] CartItemDTO updatedCartItemDTO)
        {
            try
            {
                var existingCartItem = _cartItemService.GetCartItemByName(cartItemName);
                if (existingCartItem != null)
                {
                    var updatedCartItem = _mapper.Map<CartItem>(updatedCartItemDTO);
                    _cartItemService.UpdateCartItem(cartItemName, updatedCartItem);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating cart item: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}
