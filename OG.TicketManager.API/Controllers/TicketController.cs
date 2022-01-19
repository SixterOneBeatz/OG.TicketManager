using MediatR;
using Microsoft.AspNetCore.Mvc;
using OG.TicketManager.Application.Features.Ticket.Commands;
using OG.TicketManager.Application.Features.Ticket.Queries;

namespace OG.TicketManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("GetTickets")]
        public async Task<IActionResult> GetTickets()
            => Ok(await _mediator.Send(new GetTicketsQuery()));

        [HttpGet("GetTickets/{id}")]
        public async Task<IActionResult> GetTickets(int id)
            => Ok(await _mediator.Send(new GetTicketQuery(id)));

        [HttpPost("CreateTicket")]
        public async Task<ActionResult<int>> CreateTicket(CreateTicketCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("ResolveTicket")]
        public async Task<ActionResult<int>> ResolveTicket(ResolveTicketCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("UpdateTicket")]
        public async Task<ActionResult<int>> UpdateTicket(UpdateTicketCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("DeleteTicket/{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
            => Ok(await _mediator.Send(new DeleteTicketCommand(id)));
    }
}
