using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
