using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<PlayerItemModel> PlayerItems { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<MarketItemModel> MarketItems { get; set; }
        public DbSet<ShopItemsModel> ShopItems { get; set; }
        public DbSet<SkillModel> Skills { get; set; }
        public DbSet<TrainingSkillModel> TrainingSkills { get; set; }
        public DbSet<BattleModel> Battles { get; set; }
        public DbSet<MonsterModel> Monsters { get; set; }
    }
}
