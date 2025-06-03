using BookingApi.Data;
using BookingApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<BookingRepos>();
builder.Services.AddScoped<BookingServvie>();
var app = builder.Build();




app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
