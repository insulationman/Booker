using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;

public class Booking : ITableEntity
{
    public string? Comment { get; set; }
    public string? Name { get; set; }
    private DateTime _start;
    public DateTime Start
    {
        get
        {
            return DateTime.SpecifyKind(_start, DateTimeKind.Utc);
        }
        set
        {
            _start = value;
        }
    }
    private DateTime _end;
    public DateTime End
    {
        get
        {
            return DateTime.SpecifyKind(_end, DateTimeKind.Utc);
        }
        set
        {
            _end = value;
        }
    }

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