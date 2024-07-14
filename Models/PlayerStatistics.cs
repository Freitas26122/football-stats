namespace FootballStatsAPI.Models
{
    public class PlayersStatistics
    {
        public string PlayerStatisticId { get; set; }
        public string PlayerId { get; set; }
        public string Season { get; set; }
        public int Goals { get; set; }
        public int Fouls { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int ShotsOnGoal { get; set; }
    }
}
