using RoomReservation.Domain.Entities;
using RoomReservation.Infrastructure.Persistence;

namespace RoomReservation.Infrastructure.Seeders;

internal class ClientSeeder(RoomReservationsDbContext dbContext) : IClientSeeder
{
    public async Task Seed()
    {
        if(await dbContext.Database.CanConnectAsync())
        {
            if(!dbContext.Clients.Any())
            {
                var clients = GetClients();
                dbContext.Clients.AddRange(clients);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Client> GetClients()
    {
        var clientId1 = Guid.NewGuid();
        var clientId2 = Guid.NewGuid();
        var clientId3 = Guid.NewGuid();


        return new List<Client>
        {
            new Client()
            {
                Id = clientId1,
                Name = "Dell",
                ExternalServiceId = ExternalServices.Microsoft365,
                Reservations = [
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(60),
                        ClientId = clientId1,
                        Title = "Sprint planning - Team Alpha"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(30),
                        ClientId = clientId1,
                        Title = "Sprint planning - Team Beta"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(90),
                        ClientId = clientId1,
                        Title = "Sprint planning - Team Gamma"
                    }
                ]

            },

            new Client()
            {
                Id = clientId2,
                Name = "Oracle",
                ExternalServiceId = ExternalServices.Google,
                Reservations = [
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(60),
                        ClientId = clientId2,
                        Title = "HR - Performance Review"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(30),
                        ClientId = clientId2,
                        Title = "1:1 - Manager"
                    }
                ]

            },

            new Client()
            {
                Id = clientId3,
                Name = "Atlas",
                ExternalServiceId = ExternalServices.Google,
                Reservations = [
                    new()
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(60),
                        ClientId = clientId2,
                        Title = "Sales Pitch"
                    }
                ]

            }

        };
    }
}
