using BsCryptoTrading.Application.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BsCryptoTrading.Api.Controllers;

[ApiController]
[Route("api/clients")]
public sealed class ClientsController(ISender sender) : ControllerBase
{
    [HttpPost("onboard")]
    public async Task<ActionResult<Guid>> Onboard([FromBody] OnboardClientRequest request, CancellationToken ct)
    {
        var id = await sender.Send(new OnboardClientCommand(request.ExternalRef, request.LegalName), ct);
        return Ok(id);
    }
}

public sealed record OnboardClientRequest(string ExternalRef, string LegalName);
