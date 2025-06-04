using BookingApi.Data;
using BookingApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<BookingDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddScoped<BookingRepos>();
builder.Services.AddScoped<BookingServvie>();

builder.Services.AddAuthentication("BookingToken")
    .AddJwtBearer("BookingToken", options =>
    {
        options.Authority = "https://accountprovider.azurewebsites.net/api/account/verify"; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
