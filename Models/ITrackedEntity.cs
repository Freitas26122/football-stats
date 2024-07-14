namespace FootballStatsAPI.Models
{
    public interface ITrackedEntity
    {
        DateTimeOffset? LastModified { get; set; }
        bool IsOfflineCommand { get; set; }
    }
}
