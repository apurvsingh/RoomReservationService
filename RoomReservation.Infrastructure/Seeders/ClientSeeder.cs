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
        return new List<Client>
        {
            new Client()
            {
                Name = "Dell",
                ExternalServiceId = ExternalServices.Google,
                Bookings = new List<Booking> {
                    new Booking()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(60),
                        Title = "Sprint planning - Team Alpha",
                        ExternalService = ExternalServices.Google,
                        RoomId = "10"
                    },
                    new Booking()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(30),
                        Title = "Sprint planning - Team Beta",
                        ExternalService = ExternalServices.Google,
                        RoomId = "33"
                    },
                    new Booking()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(90),
                        Title = "Sprint planning - Team Gamma",
                        ExternalService = ExternalServices.Google,
                        RoomId = "32"

                    }
                }

            },

            new Client()
            {
                Name = "Oracle",
                ExternalServiceId = ExternalServices.Microsoft365,
                Bookings = new List<Booking>() {
                    new Booking()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(60),
                        Title = "HR - Performance Review",
                        ExternalService = ExternalServices.Microsoft365,
                        RoomId = "48"
                    },
                    new Booking()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(30),
                        Title = "1:1 - Manager",
                        ExternalService = ExternalServices.Microsoft365,
                        RoomId = "4"
                    }
                }

            },

            new Client()
            {
                Name = "Atlas",
                Bookings = [
                    new Booking()
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMinutes(60),
                        Title = "Sales Pitch",
                        RoomId = "5"
                    }
                ]

            }

        };
    }

}
