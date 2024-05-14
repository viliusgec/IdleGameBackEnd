using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IItemService itemService, IMappingRetrievalService mappingService) : ControllerBase
    {
        private readonly IItemService _itemService = itemService;
        private readonly IMappingRetrievalService _mappingService = mappingService;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItem()
        {
            var result = await _itemService.GetItems();
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map< IEnumerable<ItemDto>>(result));
        }

        [HttpGet]
        [Route("ShopItems")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetShopItem()
        {
            var result = await _itemService.GetShopItems();
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<ItemDto>>(result));
        }

        [HttpGet]
        [Route("GetPlayerItems")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PlayerItemDto>>> GetPlayerItems()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.GetPlayerItems(username);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<PlayerItemDto>>(result));
        }

        [HttpPost]
        [Route("SellPlayerItems")]
        [Authorize]
        public async Task<ActionResult<PlayerItemDto>> SellPlayerItems(PlayerItemDto playerItem, int sellAmount)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.SellPlayerItems(username, playerItem, sellAmount);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<PlayerItemDto>(result));
        }

        [HttpPost]
        [Route("BuyItems")]
        [Authorize]
        public async Task<ActionResult<PlayerDto>> BuyItems(string itemName, int buyAmount)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;

            var result = await _itemService.BuyItems(username, itemName, buyAmount);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<PlayerDto>(result));
        }

        [HttpPost]
        [Route("SellMarketItem")]
        [Authorize]
        public async Task<ActionResult<MarketItemDto>> SellMarketItem(MarketItemDto playerItem)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            playerItem.Player = username;
            var result = await _itemService.SellMarketItems(playerItem);
            return Ok(_mappingService.Map<MarketItemDto>(result));
        }

        [HttpPost]
        [Route("BuyMarketItems")]
        [Authorize]
        public async Task<ActionResult<MarketItemDto>> BuyMarkteItems(MarketItemDto itemName)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;

            var result = await _itemService.BuyMarketItems(username, itemName);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<MarketItemDto>(result));
        }

        [HttpPost]
        [Route("CancelMarketListing")]
        [Authorize]
        public async Task<ActionResult<MarketItemEntity>> CancelMarketListing(MarketItemDto marketItem)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.CancelMarketListing(username, marketItem);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<MarketItemDto>(result));
        }

        [HttpGet]
        [Route("MarketItems")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MarketItemDto>>> GetMarketItems()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = (await _itemService.GetMarketItems()).Where(x => !x.Player.Equals(username));
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<MarketItemDto>>(result));
        }

        [HttpGet]
        [Route("PlayerMarketItems")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MarketItemDto>>> GetPlayerMarketItems()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.GetPlayerMarketItems(username);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<MarketItemDto>>(result));
        }

        [HttpGet]
        [Route("PlayerEquippedItems")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EquippedItemsDto>>> GetPlayerEquippedItems()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.GetPlayerEquippedItems(username);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<EquippedItemsDto>>(result));
        }

        [HttpPost]
        [Route("EquipItem")]
        [Authorize]
        public async Task<ActionResult<EquippedItemsDto>> EquipItem(string itemName)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.EquipItem(username, itemName);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<EquippedItemsDto>(result));
        }

        [HttpPost]
        [Route("UnEquipItem")]
        [Authorize]
        public async Task<ActionResult<EquippedItemsDto>> UnEquipItem(string itemPlace)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _itemService.UnEquipItem(username, itemPlace);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<EquippedItemsDto>(result));
        }
    }
}
