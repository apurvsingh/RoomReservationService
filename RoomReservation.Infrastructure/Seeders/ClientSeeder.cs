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
                        StartTime = new DateTime(2024, 12, 10, 7, 30, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2024, 12, 10, 9, 0, 0, DateTimeKind.Utc),
                        Title = "Sprint planning - Team Delta",
                        ExternalService = ExternalServices.Google,
                        RoomId = "10"
                    },
                    new Booking()
                    {
                        StartTime = new DateTime(2024, 12, 31, 15, 30, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2024, 12, 31, 16, 0, 0, DateTimeKind.Utc),
                        Title = "Sprint planning - Team Alpha",
                        ExternalService = ExternalServices.Google,
                        RoomId = "10"
                    },
                    new Booking()
                    {
                        StartTime = new DateTime(2024, 12, 31, 9, 0, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2024, 12, 31, 9, 30, 0, DateTimeKind.Utc),
                        Title = "Sprint planning - Team Beta",
                        ExternalService = ExternalServices.Google,
                        RoomId = "33"
                    },
                    new Booking()
                    {
                        StartTime = new DateTime(2024, 12, 31, 23, 0, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2024, 12, 31, 23, 0, 0, DateTimeKind.Utc).AddMinutes(120),
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
                        StartTime = new DateTime(2024, 12, 18, 9, 0, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2024, 12, 18, 10, 30, 0, DateTimeKind.Utc),
                        Title = "HR - Performance Review",
                        ExternalService = ExternalServices.Microsoft365,
                        RoomId = "48"
                    },
                    new Booking()
                    {
                        StartTime = new DateTime(2024, 12, 20, 10, 30, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2024, 12, 20, 11, 30, 0, DateTimeKind.Utc),
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
                        StartTime = new DateTime(2025, 1, 4, 14, 30, 0, DateTimeKind.Utc),
                        EndTime = new DateTime(2025, 1, 4, 14, 30, 0, DateTimeKind.Utc).AddMinutes(60),
                        Title = "Sales Pitch",
                        RoomId = "5"
                    }
                ]

            }

        };
    }

}
