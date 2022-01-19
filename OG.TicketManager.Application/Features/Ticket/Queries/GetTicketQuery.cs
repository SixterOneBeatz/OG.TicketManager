using MediatR;
using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Application.Exceptions;
using OG.TicketManager.Application.Repositories;

namespace OG.TicketManager.Application.Features.Ticket.Queries
{
    public class GetTicketQuery : IRequest<TicketDTO>
    {
        public GetTicketQuery(int id)
            => Id = id;
        public int Id { get; set; }
    }

    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, TicketDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTicketQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;


        public async Task<TicketDTO> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            TicketDTO ticket = await _unitOfWork.TicketRepository.GetTicketAndLastHistory(request.Id);
            if (ticket is null) throw new NotFoundException("Ticket", request.Id);
            return ticket;
        }
    }
}
