using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OG.TicketManager.Application.Repositories;

namespace OG.TicketManager.Application.Features.Ticket.Commands
{
    public class CreateTicketCommand : IRequest<int>
    {
        public string Description { get; set; } = string.Empty;
    }

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTicketCommandHandler> _logger;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateTicketCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            Domain.Ticket newTicket = _mapper.Map<Domain.Ticket>(request);
            _unitOfWork.Repository<Domain.Ticket>().AddEntity(newTicket);
            int result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                string errorMsg = "Ticket cannot be created";
                _logger.LogError(errorMsg);
                throw new Exception(errorMsg);
            }

            _unitOfWork.HistoryRepository.AddFirstHistory(newTicket.Id);
            int resultHistory = await _unitOfWork.Complete();
            if (resultHistory <= 0)
            {
                string errorMsg = "History cannot be created";
                _logger.LogError(errorMsg);
                throw new Exception(errorMsg);
            }

            return newTicket.Id;
        }
    }

    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Property {Description} cannot be empty")
                .NotNull().WithMessage("Property {Description} cannot be null");
        }
    }
}
