public interface ITableService
{
    Task<IEnumerable<Booking>> GetBookingsAsync(int year, int month);
    Task<Booking> UpsertBookingAsync(Booking entity);
    Task<bool> DeleteBookingAsync(int year, int month, string id);
}