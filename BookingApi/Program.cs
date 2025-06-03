using BookingApi.Data;
using BookingApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<BookingDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddScoped<BookingRepos>();
builder.Services.AddScoped<BookingServvie>();
builder.Services.AddCors(x =>
{
    x.AddPolicy("AllowAll", x =>
    {
        x.AllowAnyOrigin();
        x.AllowAnyHeader();
        x.AllowAnyMethod();
    });

});
var app = builder.Build();




app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
