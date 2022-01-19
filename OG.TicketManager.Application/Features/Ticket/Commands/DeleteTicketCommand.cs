using MediatR;
using Microsoft.Extensions.Logging;
using OG.TicketManager.Application.Exceptions;
using OG.TicketManager.Application.Repositories;

namespace OG.TicketManager.Application.Features.Ticket.Commands
{
    public class DeleteTicketCommand : IRequest<int>
    {
        public DeleteTicketCommand(int id)
            => Id = id;
        public int Id { get; set; }
    }

    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTicketCommandHandler> _logger;

        public DeleteTicketCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteTicketCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            Domain.Ticket ticketToUpdate = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);
            if (ticketToUpdate is null) throw new NotFoundException("Ticket", request.Id);
            _unitOfWork.TicketRepository.DeleteTicket(ticketToUpdate);

            int result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                string errorMsg = "Ticket cannot be deleted";
                _logger.LogError(errorMsg);
                throw new Exception(errorMsg);
            }

            return ticketToUpdate.Id;
        }
    }
}
