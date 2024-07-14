public class LogChangedEntry
{
    public string Id { get; set; }
    public string EntityName { get; set; }
    public string EntityState { get; set; }
    public List<LogChangedProperty> Properties { get; set; } = new List<LogChangedProperty>();
}

public class LogChangedProperty
{
    public string PropertyName { get; set; }
    public object OriginalValue { get; set; }
    public object CurrentValue { get; set; }
}
