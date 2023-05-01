﻿namespace IdleGame.Domain.Entities
{
    public class AchievementsEntity
    {
        public int Id { get; set; }
        public string SkillType { get; set; }
        public int RequiredXP { get; set; }
        public int Reward { get; set; }
        public string Description { get; set; }
    }
}
