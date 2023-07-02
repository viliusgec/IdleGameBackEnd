﻿namespace IdleGame.Api.Contracts
{
    public class ItemDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? SellPrice { get; set; }
        public bool isSellable { get; set; }
        public string Type { get; set; }
    }
}
