using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;

public class Booking : ITableEntity
{
    public string? Comment { get; set; }
    public string? Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    //set partition key as year and month
    [JsonIgnore]
    public string PartitionKey
    {
        get => $"{Start.Year}{Start.Month:D2}";
        set { }
    }
    public string RowKey { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset? Timestamp { get; set; }
    [JsonIgnore]
    public ETag ETag { get; set; }
}