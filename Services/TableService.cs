using Azure.Data.Tables;

public class TableService : ITableService
{
    private const string TableName = "Bookings";
    private readonly IConfiguration _configuration;
    public TableService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DeleteBookingAsync(int year, int month, string id)
    {
        var tableClient = await GetTableClient();
        var result = await tableClient.DeleteEntityAsync($"{year}{month:D2}", id);
        return result.Status == 204;
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync(int year, int month)
    {
        List<Booking> result = new();
        var tableClient = await GetTableClient();
        var PartitionKey = $"{year}{month:D2}";
        var bookings = tableClient.QueryAsync<Booking>(b => b.PartitionKey == PartitionKey);
        await foreach (var booking in bookings)
        {
            result.Add(booking);
        }
        return result;
    }

    public async Task<Booking> UpsertBookingAsync(Booking entity)
    {
        var tableClient = await GetTableClient();
        await tableClient.UpsertEntityAsync(entity);
        return entity;
    }


    private async Task<TableClient> GetTableClient()
    {
        var serviceClient = new TableServiceClient(_configuration["TableStorage"]);
        var tableClient = serviceClient.GetTableClient(TableName);
        await tableClient.CreateIfNotExistsAsync();
        return tableClient;
    }
}