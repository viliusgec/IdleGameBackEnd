namespace IdleGame.Api.Contracts
{
    public class PlayerIdleTrainingDto
    {
        public int Id { get; set; }
        public IdleTrainingDto IdleTraining { get; set; }
        public DateTime StartTime { get; set; }
        public bool Active { get; set; }
    }
}
