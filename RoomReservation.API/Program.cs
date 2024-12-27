using RoomReservation.Application.Extensions;
using RoomReservation.Infrastructure.Extensions;
using RoomReservation.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


// Extra code to seed data to db
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IClientSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
