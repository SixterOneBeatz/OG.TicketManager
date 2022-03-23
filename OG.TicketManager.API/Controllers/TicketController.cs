using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OG.TicketManager.API.Hubs;
using OG.TicketManager.Application.Features.Ticket.Commands;
using OG.TicketManager.Application.Features.Ticket.Queries;

namespace OG.TicketManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<TicketHub> _hubContext;

        public TicketController(IMediator mediator, IHubContext<TicketHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpGet("GetTickets")]
        public async Task<IActionResult> GetTickets()
            => Ok(await _mediator.Send(new GetTicketsQuery()));

        [HttpGet("GetTickets/{id}")]
        public async Task<IActionResult> GetTickets(int id)
            => Ok(await _mediator.Send(new GetTicketQuery(id)));

        [HttpPost("CreateTicket")]
        public async Task<ActionResult<int>> CreateTicket(CreateTicketCommand command)
        {
            int result = await _mediator.Send(command);
            await SendTickets();
            return Ok(result);  
        }

        [HttpPut("ResolveTicket")]
        public async Task<ActionResult<int>> ResolveTicket(ResolveTicketCommand command)
        {
            int result = await _mediator.Send(command);
            await SendTickets();
            return Ok(result);
        }

        [HttpPut("UpdateTicket")]
        public async Task<ActionResult<int>> UpdateTicket(UpdateTicketCommand command)
        {
            int result = await _mediator.Send(command);
            await SendTickets();
            return Ok(result);
        }

        [HttpDelete("DeleteTicket/{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            int result = await _mediator.Send(new DeleteTicketCommand(id));
            await SendTickets();
            return Ok(result);
        }

        private async Task SendTickets()
            => await _hubContext.Clients.All.SendAsync("sendTickets", await _mediator.Send(new GetTicketsQuery()));
    }
}