using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using log4net;


namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public MenuItemController(IMenuItemService menuItemService, IMapper mapper, ILog logger = null)
        {
            _menuItemService = menuItemService ?? throw new ArgumentNullException(nameof(menuItemService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<MenuItemDTO>> GetAllMenuItems()
        {
            try
            {
                var menuItems = _menuItemService.GetAllMenuItems();
                var menuItemDTOs = _mapper.Map<List<MenuItemDTO>>(menuItems);
                _logger?.Info("Retrieved all menu items successfully.");
                return StatusCode(200, menuItemDTOs);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetAllMenuItems: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{menuItemId}")]
     //   [Authorize(Roles = "Admin, User")]
        public ActionResult<MenuItemDTO> GetMenuItemById(int menuItemId)
        {
            try
            {
                var menuItem = _menuItemService.GetMenuItemById(menuItemId);

                if (menuItem == null)
                {
                    _logger?.Warn($"Menu item with ID {menuItemId} not found.");
                    return StatusCode(404);
                }

                var menuItemDTO = _mapper.Map<MenuItemDTO>(menuItem);
                _logger?.Info($"Retrieved menu item by ID successfully. Menu Item ID: {menuItemId}");
                return StatusCode(200, menuItemDTO);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetMenuItemById: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ByName/{menuItemName}")]
        // [Authorize(Roles = "Admin, User")]
        public ActionResult<MenuItemDTO> GetMenuItemByName(string menuItemName)
        {
            try
            {
                var menuItem = _menuItemService.GetMenuItemByName(menuItemName);

                if (menuItem == null)
                {
                    _logger?.Warn($"Menu item with name {menuItemName} not found.");
                    return StatusCode(404);
                }

                var menuItemDTO = _mapper.Map<MenuItemDTO>(menuItem);
                _logger?.Info($"Retrieved menu item by name successfully. Menu Item Name: {menuItemName}");
                return StatusCode(200, menuItemDTO);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetMenuItemByName: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("ByRestaurant/{restaurantName}")]
        //  [Authorize(Roles = "Admin, User, Restaurant")]
        public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsByRestaurantName(string restaurantName)
        {
            try
            {
                var menuItems = _menuItemService.GetMenuItemsByRestaurantName(restaurantName);
                var menuItemDTOs = _mapper.Map<List<MenuItemDTO>>(menuItems);
                _logger?.Info($"Retrieved menu items by restaurant name successfully. Restaurant Name: {restaurantName}");
                return StatusCode(200, menuItemDTOs);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in GetMenuItemsByRestaurantName: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        //[Authorize(Roles = "Admin, Restaurant")]
        public ActionResult CreateMenuItem([FromBody] MenuItemDTO menuItemDTO)
        {
            try
            {
                var menuItem = _mapper.Map<MenuItem>(menuItemDTO);
                _menuItemService.CreateMenuItem(menuItem);
                _logger?.Info("Menu item created successfully.");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in CreateMenuItem: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{menuItemId}")]
        //[Authorize(Roles = "Admin, Restaurant")]
        public ActionResult UpdateMenuItem(int menuItemId, [FromBody] MenuItemDTO menuItemDTO)
        {
            try
            {
                var existingMenuItem = _menuItemService.GetMenuItemById(menuItemId);

                if (existingMenuItem == null)
                {
                    _logger?.Warn($"Menu item with ID {menuItemId} not found.");
                    return StatusCode(404);
                }

                var updatedMenuItem = _mapper.Map<MenuItem>(menuItemDTO);
                _menuItemService.UpdateMenuItem(menuItemId, updatedMenuItem);

                _logger?.Info($"Menu item updated successfully. Menu Item ID: {menuItemId}");
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in UpdateMenuItem: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{menuItemId}")]
      //  [Authorize(Roles = "Admin, Restaurant")]
        public ActionResult DeleteMenuItem(int menuItemId)
        {
            var existingMenuItem = _menuItemService.GetMenuItemById(menuItemId);

            if (existingMenuItem == null)
            {
                _logger?.Warn($"Menu item with ID {menuItemId} not found.");
                return StatusCode(404);
            }

            _menuItemService.DeleteMenuItem(menuItemId);

            _logger?.Info($"Menu item deleted successfully. Menu Item ID: {menuItemId}");
            return StatusCode(204);
        }
        [HttpGet("Search/{query}")]
        [AllowAnonymous] // Adjust authorization as needed
        public ActionResult<IEnumerable<MenuItemDTO>> SearchMenuItems(string query)
        {
            try
            {
                var menuItems = _menuItemService.SearchMenuItems(query);
                var menuItemDTOs = _mapper.Map<List<MenuItemDTO>>(menuItems);
                _logger?.Info($"Retrieved menu items by search query successfully. Query: {query}");
                return StatusCode(200, menuItemDTOs);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in SearchMenuItems: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
