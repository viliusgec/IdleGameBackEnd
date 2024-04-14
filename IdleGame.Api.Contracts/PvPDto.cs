namespace IdleGame.Api.Contracts
{
    public class PvPDto
    {
        public int Id { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public int Bet { get; set; }
        public DateTime Date { get; set; }
        public string Winner { get; set; }
    }
}
