using MediatR;
using Microsoft.Extensions.Logging;
using OG.TicketManager.Application.Exceptions;
using OG.TicketManager.Application.Repositories;
namespace OG.TicketManager.Application.Features.Ticket.Commands
{
    public class ResolveTicketCommand : IRequest<int>
    {
        public ResolveTicketCommand(int id)
            => Id = id;
        public int Id { get; set; }
    }

    public class ResolveTicketCommandHandler : IRequestHandler<ResolveTicketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResolveTicketCommandHandler> _logger;

        public ResolveTicketCommandHandler(IUnitOfWork unitOfWork, ILogger<ResolveTicketCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(ResolveTicketCommand request, CancellationToken cancellationToken)
        {
            Domain.Ticket ticketToUpdate = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);
            if (ticketToUpdate is null) throw new NotFoundException("Ticket", request.Id);
            _unitOfWork.TicketRepository.ResolveTicket(ticketToUpdate);
            _unitOfWork.HistoryRepository.AddLastHistory(request.Id);

            int result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                string errorMsg = "Ticket cannot be resolved";
                _logger.LogError(errorMsg);
                throw new Exception(errorMsg);
            }

            return ticketToUpdate.Id;
        }
    }
}
