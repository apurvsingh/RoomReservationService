using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Commands.CreateClient;
using RoomReservation.Application.Queries.GetAllClients;
using RoomReservation.Application.Queries.GetClientById;

namespace RoomReservation.API.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await mediator.Send(new GetAllClientsQuery());
        return Ok(clients);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var client = await mediator.Send(new GetClientByIdQuery(id));

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand createClientCommand)
    {
        int id = await mediator.Send(createClientCommand);
        return CreatedAtAction(nameof(GetById), new {id}, null);
    }
}
