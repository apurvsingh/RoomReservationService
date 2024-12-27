using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Clients;
using RoomReservation.Application.Dtos.Client;

namespace RoomReservation.API.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController(IClientsService _clientsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientsService.GetAllClients();
        return Ok(clients);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var client = await _clientsService.GetById(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientDto createClientDto)
    {
        int id = await _clientsService.CreateClient(createClientDto);

        return CreatedAtAction(nameof(GetById), new {id}, null);
    }


}
