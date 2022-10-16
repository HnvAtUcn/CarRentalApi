using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarRentalApi.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarRentalApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalApiContext") ?? throw new InvalidOperationException("Connection string 'CarRentalApiContext' not found.")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
