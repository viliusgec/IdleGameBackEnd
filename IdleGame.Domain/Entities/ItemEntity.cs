﻿namespace IdleGame.Domain.Entities
{
    public class ItemEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? SellPrice { get; set; }
        public bool isSellable { get; set; }
    }
}
