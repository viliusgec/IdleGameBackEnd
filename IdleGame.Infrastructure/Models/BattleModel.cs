﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdleGame.Infrastructure.Models
{
    [Table("Battles", Schema = "Clicker")]
    public class BattleModel
    {
        [Key]
        public int ID { get; set; }
        public string Player { get; set; }
        public string Monster { get; set; }
        public int PlayerHP { get; set; }
        public int MonsterHP { get; set; }
        public bool BattleFinished { get; set; }
        public bool ItemGiven { get; set; }
    }
}
