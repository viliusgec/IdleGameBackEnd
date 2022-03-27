using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMappingRetrievalService _mappingService;

        public ItemsController(IItemService itemService, IMappingRetrievalService mappingService)
        {
            _itemService = itemService;
            _mappingService = mappingService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItem()
        {
            var result = await _itemService.GetItems();
            return Ok(_mappingService.Map< IEnumerable<ItemDto>>(result));
        }
        
        [HttpGet]
        [Route("GetPlayerItems")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PlayerItemDto>>> GetPlayerItems()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.GetPlayerItems(username);
            return Ok(_mappingService.Map<IEnumerable<PlayerItemDto>>(result));
        }

        [HttpPost]
        [Route("SellPlayerItems")]
        [Authorize]
        public async Task<ActionResult<PlayerItemDto>> SellPlayerItems(PlayerItemDto playerItem, int sellAmmount)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.SellPlayerItems(username, playerItem, sellAmmount);
            return Ok(_mappingService.Map<PlayerItemDto>(result));
        }
    }
}
