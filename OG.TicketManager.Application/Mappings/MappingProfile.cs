using AutoMapper;
using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Application.Features.Ticket.Commands;
using OG.TicketManager.Domain;

namespace OG.TicketManager.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketDTO>();
            CreateMap<History, HistoryDTO>();
            CreateMap<CreateTicketCommand, Ticket>();
            CreateMap<UpdateTicketCommand, Ticket>();
            CreateMap<UpdateTicketCommand, History>()
                .ForMember(t => t.Description, src => src.MapFrom(s => s.HistoryDescription))
                .ForMember(t => t.TicketId, src => src.MapFrom(s => s.Id))
                .ForMember(t => t.Id, src => src.Ignore());
        }
    }
}
