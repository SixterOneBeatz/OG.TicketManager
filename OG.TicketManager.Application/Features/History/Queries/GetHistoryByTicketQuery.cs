using AutoMapper;
using MediatR;
using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Application.Exceptions;
using OG.TicketManager.Application.Repositories;

namespace OG.TicketManager.Application.Features.History.Queries
{
    public class GetHistoryByTicketQuery : IRequest<List<HistoryDTO>>
    {
        public GetHistoryByTicketQuery(int ticketId)
            => TicketId = ticketId;
        public int TicketId { get; set; }
    }

    public class GetHistoryByTicketQueryHandler : IRequestHandler<GetHistoryByTicketQuery, List<HistoryDTO>>
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public GetHistoryByTicketQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HistoryDTO>> Handle(GetHistoryByTicketQuery request, CancellationToken cancellationToken)
        {
            Domain.Ticket ticket = await _unitOfWork.TicketRepository.GetByIdAsync(request.TicketId);
            if (ticket is null) throw new NotFoundException("Ticket", request.TicketId);

            List<Domain.History> histories = await _unitOfWork.HistoryRepository.GetHistoryAsync(request.TicketId);
            return _mapper.Map<List<HistoryDTO>>(histories);
        }
    }
}
