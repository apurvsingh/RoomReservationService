using RoomReservation.API.Middleware;
using RoomReservation.Application.Extensions;
using RoomReservation.Common.Extensions;
using RoomReservation.Infrastructure.Extensions;
using RoomReservation.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddSwaggerGen();

builder.Services.AddCommonServices();   
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => {
    configuration
    .ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();


// Extra code to seed data to db
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IClientSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
