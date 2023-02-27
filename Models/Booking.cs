using Azure;
using Azure.Data.Tables;

public class Booking : ITableEntity
{
    public string Comment { get; set; }
    public string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}