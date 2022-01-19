using MediatR;
using Microsoft.AspNetCore.Mvc;
using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Application.Features.History.Queries;

namespace OG.TicketManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        IMediator _mediator;
        public HistoryController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("GetTicketHistory/{ticketId}")]
        public async Task<ActionResult<List<HistoryDTO>>> GetTicketHistory(int ticketId)
            => Ok(await _mediator.Send(new GetHistoryByTicketQuery(ticketId)));
    }
}
