using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OG.TicketManager.Application.Exceptions;
using OG.TicketManager.Application.Repositories;

namespace OG.TicketManager.Application.Features.Ticket.Commands
{
    public class UpdateTicketCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string HistoryDescription { get; set; } = string.Empty;
    }

    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTicketCommandHandler> _logger;

        public UpdateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateTicketCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            Domain.Ticket ticketToUpdate = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);
            if (ticketToUpdate is null) throw new NotFoundException("Ticket", request.Id);

            _unitOfWork.TicketRepository.UpdateTicket(ticketToUpdate);
            _mapper.Map(request, ticketToUpdate);

            Domain.History newHistory = _mapper.Map<Domain.History>(request);
            _unitOfWork.HistoryRepository.AddHistory(newHistory);

            int result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                string errorMsg = "Ticket cannot be updated";
                _logger.LogError(errorMsg);
                throw new Exception(errorMsg);
            }

            return ticketToUpdate.Id;
        }
    }

    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(0).WithMessage("Property {Id} cannot be zero");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Property {Description} cannot be empty")
                .NotNull().WithMessage("Property {Description} cannot be null");
            RuleFor(x => x.HistoryDescription)
                .NotEmpty().WithMessage("Property {HistoryDescription} cannot be empty")
                .NotNull().WithMessage("Property {HistoryDescription} cannot be null");
        }
    }
}
