using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame.Domain.Entities
{
    public class PlayerStatisticsEntity
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public string TrainingName { get; set; }
        public int Count { get; set; }
    }
}
