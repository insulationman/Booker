using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddCors();

var app = builder.Build();

// app.UseIPFilter();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});
app.UseApiKeyFilter();


app.MapGet("/bookings", async (int year, int month, [FromServices] ITableService tableService) =>
{
    return await tableService.GetBookingsAsync(year, month);
})
.WithOpenApi();

app.MapPost("/bookings", async (Booking booking, [FromServices] ITableService tableService) =>
{
    return await tableService.UpsertBookingAsync(booking);
}).WithOpenApi();

app.MapDelete("/bookings", async (int year, int month, string id, [FromServices] ITableService tableService) =>
{
    return await tableService.DeleteBookingAsync(year, month, id);
}).WithOpenApi();

app.Run();