namespace FootballStatsAPI.Models
{
    public class TeamsStatistics
    {
        public string StatisticId { get; set; }
        public string TeamId { get; set; }
        public string Season { get; set; }
        public int Goals { get; set; }
        public int Fouls { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int ShotsOnGoal { get; set; }
    }
}
