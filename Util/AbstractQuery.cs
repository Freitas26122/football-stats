namespace FootballStatsAPI.Util
{
    public abstract class AbstractQuery<T>
    {
        public abstract Task<QueryResult<T>> ExecuteAsync();
        public abstract bool IsValid();
    }
}