using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using IdleGame.Domain.Entities;
using Moq;
using IdleGame.Api.Contracts;

namespace UnitTests
{
    public class ItemServiceUnitTests
    {
        private readonly ItemService itemService;
        private readonly Mock<IPlayerRetrievalService> _playerServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;

        private readonly List<ItemEntity> items = [new() { Name = "TestItem", Price = 10, SellPrice = 10, isSellable = true, Description = "TestItem", Type = "tool" }];
        private readonly List<PlayerItemEntity> playerItems = [new() { PlayerUsername = "TestPlayer", Item = new ItemEntity { Name = "TestItem", Price = 100, isSellable = true, Description = "TestItem", Type = "tool" }, Amount = 1 }, new() { PlayerUsername = "TestPlayer", Item = new ItemEntity { Name = "TestItem3", Price = 100, isSellable = false, Description = "TestItem3", Type = "tool" }, Amount = 1 }];
        private readonly List<PlayerItemDto> playerItemDtos = [new() { Item = new ItemDto { Name = "TestItem" }, ItemName = "TestItem" }];
        private readonly PlayerEntity player = new() { Username = "TestPlayer", Money = 100, IsInAction = false, IsInBattle = false, InventorySpace = 10 };
        private readonly PlayerEntity player2 = new() { Username = "TestPlayer2", Money = 100, IsInAction = false, IsInBattle = false, InventorySpace = 10 };
        private readonly List<MarketItemEntity> marketItems = [new() { Player = "TestPlayer", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }, new() { Player = "TestPlayer2", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }, new() { Player = "TestPlayer2", ItemName = "TestItem2", Amount = 2, Id = 0, Price = 10 }];
        private readonly List<EquippedItemsEntity> equippedItems = [new() { Id = 0, Item = new() { Name = "TestItemEquip" }, ItemPlace = "tool", PlayerUsername = "TestPlayer" }, new() { Id = 0, Item = new() { Name = "TestItem" }, ItemPlace = "tool", PlayerUsername = "TestPlayer" }];
        public ItemServiceUnitTests()
        {
            _playerServiceMock = new Mock<IPlayerRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            itemService = new ItemService(_itemServiceMock.Object, _playerServiceMock.Object, _mapRetrievalServiceMock.Object);

            _itemServiceMock.Setup(x => x.GetItems()).ReturnsAsync(items.AsEnumerable());
            _itemServiceMock.Setup(x => x.GetShopItems()).ReturnsAsync(items.AsEnumerable());
            _itemServiceMock.Setup(x => x.GetPlayerItems("TestPlayer")).ReturnsAsync(playerItems.AsEnumerable());
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "TestItem")).ReturnsAsync(playerItems[0]);
            _itemServiceMock.Setup(x => x.PutPlayerItem(It.IsAny<PlayerItemEntity>())).Returns(playerItems[0]);
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer")).ReturnsAsync(player);
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer2")).ReturnsAsync(player2);
            _itemServiceMock.Setup(x => x.GetMarketItems()).ReturnsAsync(marketItems.AsEnumerable());
            _itemServiceMock.Setup(x => x.GetPlayerMarketItems("TestPlayer2")).ReturnsAsync(marketItems.AsEnumerable());
            _itemServiceMock.Setup(x => x.GetPlayerMarketItems("TestPlayer")).ReturnsAsync(marketItems.AsEnumerable());
            _itemServiceMock.Setup(x => x.PostMarketItem(It.IsAny<MarketItemEntity>())).ReturnsAsync(marketItems[0]);
            _itemServiceMock.Setup(x => x.DeleteMarketItem(marketItems[0])).Returns(marketItems[0]);
            _mapRetrievalServiceMock.Setup(x => x.Map<MarketItemEntity>(It.IsAny<MarketItemDto>())).Returns(marketItems[0]);
            _itemServiceMock.Setup(x => x.GetPlayerEquippedItems("TestPlayer")).ReturnsAsync(equippedItems.AsEnumerable());
            _itemServiceMock.Setup(x => x.GetPlayerEquippedItem("TestPlayer", "tool")).ReturnsAsync(equippedItems[0]);
            _itemServiceMock.Setup(x => x.PutEquippedItem(equippedItems[0])).Returns(equippedItems[0]);
            _itemServiceMock.Setup(x => x.PutEquippedItem(equippedItems[1])).Returns(equippedItems[1]);
        }

        [Fact]
        public async void Test_GetItems_Should_Return_Items()
        {
            var result = await itemService.GetItems();
            Assert.NotNull(result);
            Assert.Equal(items, result);
        }

        [Fact]
        public async void Test_GetShopItems_Should_Return_Items()
        {
            var result = await itemService.GetShopItems();
            Assert.NotNull(result);
            Assert.Equal(items, result);
        }
        [Fact]
        public async void Test_GetPlayerItem_Should_Return_Item()
        {
            var result = await itemService.GetPlayerItems("TestPlayer");
            Assert.NotNull(result);
            Assert.Equal(playerItems, result);
        }
        [Fact]
        public async void Test_SellPlayerItems_Should_Return_PlayerItem()
        {
            var result = await itemService.SellPlayerItems("TestPlayer", playerItemDtos[0], 1);
            Assert.NotNull(result);
            Assert.Equal(playerItems[0], result);
        }
        [Fact]
        public async void Test_SellPlayerItems_Should_Return_Null()
        {
            playerItemDtos[0].ItemName = "TestItem2";
            var result = await itemService.SellPlayerItems("TestPlayer", playerItemDtos[0], 1);
            Assert.Null(result);
        }
        [Fact]
        public async void Test_SellPlayerItems_Should_Return_Null_When_AmountIsLess()
        {
            var result = await itemService.SellPlayerItems("TestPlayer", playerItemDtos[0], 5);
            Assert.Null(result);
        }
        [Fact]
        public async void Test_SellPlayerItems_Should_Return_Null_When_ItemIsNotSellable()
        {
            playerItemDtos[0].ItemName = "TestItem3";
            var result = await itemService.SellPlayerItems("TestPlayer", playerItemDtos[0], 1);
            Assert.Null(result);
        }
        [Fact]
        public async void Test_BuyItem_Should_Return_PlayerItem()
        {
            var result = await itemService.BuyItems("TestPlayer", "TestItem", 1);
            Assert.NotNull(result);
            Assert.Equal(player, result);
        }
        [Fact]
        public async void Test_BuyItem_Should_Return_Null_When_PlayerMoneyIsLess()
        {
            player.Money = 0;
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer")).ReturnsAsync(player);
            var result = await itemService.BuyItems("TestPlayer", "TestItem", 1);
            Assert.Null(result);
        }
        [Fact]
        public async void Test_BuyItem_Should_Return_Null_When_ItemIsNotInShop()
        {
            var result = await itemService.BuyItems("TestPlayer", "TestItem2", 1);
            Assert.Null(result);
        }
        [Fact]
        public async void Test_BuyItem_Should_Return_Null_When_ItemPriceIsZero()
        {
            items[0].SellPrice = null;
            _itemServiceMock.Setup(x => x.GetShopItems()).ReturnsAsync(items.AsEnumerable());
            var result = await itemService.BuyItems("TestPlayer", "TestItem", 1);
            Assert.Null(result);
        }
        [Fact]
        public async void Test_BuyItem_Should_Return_Null_When_ItemAmountIsLess()
        {
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "TestItem")).Returns(Task.FromResult<PlayerItemEntity>(null));
            var result = await itemService.BuyItems("TestPlayer", "TestItem", 5);
            Assert.NotNull(result);
            Assert.Equal(player, result);
        }
        [Fact]
        public async void Test_GetMarketItems_Should_Return_MarketItems()
        {
            var result = await itemService.GetMarketItems();
            Assert.NotNull(result);
            Assert.Equal(marketItems, result);
        }
        [Fact]
        public async void Test_GetPlayerMarketItems_Should_Return_MarketItems()
        {
            var result = await itemService.GetPlayerMarketItems("TestPlayer2");
            Assert.NotNull(result);
            Assert.Equal(marketItems, result);
        }
        [Fact]
        public async void Test_SellMarketItems_Should_Return_MarketItem()
        {
            var result = await itemService.SellMarketItems(new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer", Price = 1});
            Assert.NotNull(result);
            Assert.Equal("TestItem", result.ItemName);
        }
        [Fact]
        public async void Test_SellMarketItems_Should_Return_Null_When_PlayerItemIsNull()
        {
            var result = await itemService.SellMarketItems(new MarketItemDto { Amount = 1, ItemName = "TestItem2", Player = "TestPlayer", Price = 1 });
            Assert.Null(result);
        }
        [Fact]
        public async void Test_SellMarketItems_Should_Return_Null_When_PlayerMarketItemsCountIsMoreThanFive()
        {
             List<MarketItemEntity> marketItems2 = [new() { Player = "TestPlayer", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }, new() { Player = "TestPlayer", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }, new() { Player = "TestPlayer", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }, new() { Player = "TestPlayer", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }, new() { Player = "TestPlayer", ItemName = "TestItem", Amount = 1, Id = 0, Price = 10 }];
            _itemServiceMock.Setup(x => x.GetPlayerMarketItems("TestPlayer")).ReturnsAsync(marketItems2.AsEnumerable());
            var result = await itemService.SellMarketItems(new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer", Price = 1 });
            Assert.Null(result);
        }
        [Fact]
        public async void Test_BuyMarketItems_Should_Return_MarketItem()
        {
            
            var result = await itemService.BuyMarketItems("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer2", Price = 1 });
            Assert.NotNull(result);
            Assert.Equal("TestItem", result.ItemName);
        }
        [Fact]
        public async void Test_BuyMarketItems_Should_Return_Null_When_PlayerItemIsNull()
        {
            var result = await itemService.BuyMarketItems("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem2", Player = "TestPlayer2", Price = 1 });
            Assert.NotNull(result);
        }
        [Fact]
        public async void Test_BuyMarketItems_Should_Return_Null_When_MarketItemIsNull()
        {
            var result = await itemService.BuyMarketItems("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem55", Player = "TestPlayer2", Price = 1 });
            Assert.Null(result);
        }
        [Fact]
        public async void Test_BuyMarketItems_Should_Return_Null_When_PlayerMoneyIsLess()
        {
            player2.Money = 0;
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer")).ReturnsAsync(player2);
            var result = await itemService.BuyMarketItems("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer2", Price = 1 });
            Assert.Null(result);
        }
        [Fact]
        public async void Test_CancelMarketListing_Should_Return_DeletedListing()
        {
            var result = await itemService.CancelMarketListing("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer", Price = 1 });
            Assert.NotNull(result);
            Assert.Equal(marketItems[0], result);
        }
        [Fact]
        public async void Test_CancelMarketListing_Should_Return_Null_When_MarketItemIsNull()
        {
            var result = await itemService.CancelMarketListing("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem5", Player = "TestPlayer", Price = 1 });
            Assert.Null(result);
        }
        [Fact]
        public async void Test_CancelMarketListing_Should_Return_Null_When_PlayerNameDoesNotMatch()
        {
            var result = await itemService.CancelMarketListing("TestPlayer2", new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer", Price = 1 });
            Assert.Null(result);
        }
        [Fact]
        public async void Test_CancelMarketListing_Should_Return_Null_When_PlayerItemIsNull()
        {
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "TestItem")).Returns(Task.FromResult<PlayerItemEntity>(null));
            var result = await itemService.CancelMarketListing("TestPlayer", new MarketItemDto { Amount = 1, ItemName = "TestItem", Player = "TestPlayer", Price = 1 });
            Assert.NotNull(result);
        }
        [Fact]
        public async void Test_GetPlayerEquippedItems_Should_Return_PlayerEquippedItems()
        {
            var result = await itemService.GetPlayerEquippedItems("TestPlayer");
            Assert.NotNull(result);
            Assert.Equal(equippedItems, result);
        }
        [Fact]
        public async void Test_GetPlayerEquippedItem_Should_Return_PlayerEquippedItem()
        {
            var result = await itemService.GetPlayerEquippedItem("TestPlayer", "tool");
            Assert.NotNull(result);
            Assert.Equal("TestItemEquip", result.Item?.Name);
        }
        [Fact]
        public async void Test_UnEquipItem_Should_Return_PlayerUnEquippedItem()
        {
            var result = await itemService.UnEquipItem("TestPlayer", "tool");
            Assert.NotNull(result);
            Assert.Equal("tool", result.ItemPlace);
        }
        [Fact]
        public async void Test_EquipItem_Should_Return_PlayerEquippedItem()
        {
            var result = await itemService.EquipItem("TestPlayer", "TestItem");
            Assert.NotNull(result);
            Assert.Equal("TestItem", result.Item?.Name);
        }
        [Fact]
        public async void Test_EquipItem_Should_Return_Null_When_ItemIsNotInInventory()
        {
            var result = await itemService.EquipItem("TestPlayer", "TestItemEquip2");
            Assert.Null(result);
        }
        [Fact]
        public async void Test_EquipItem_Should_Return_Null_When_ItemPositionInWrong()
        {
            _itemServiceMock.Setup(x => x.GetPlayerEquippedItem("TestPlayer", "tool")).Returns(Task.FromResult<EquippedItemsEntity>(null));
            var result = await itemService.EquipItem("TestPlayer", "TestItem");
            Assert.Null(result);
        }
    }
}