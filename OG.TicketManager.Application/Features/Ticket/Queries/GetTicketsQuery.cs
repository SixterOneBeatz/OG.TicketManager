using AutoMapper;
using MediatR;
using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Application.Repositories;

namespace OG.TicketManager.Application.Features.Ticket.Queries
{
    public class GetTicketsQuery : IRequest<List<TicketDTO>>
    {

    }

    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<TicketDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTicketsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            => _unitOfWork = unitOfWork;

        public async Task<List<TicketDTO>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.TicketRepository.GetTicketsAndLastHistory();
    }
}
