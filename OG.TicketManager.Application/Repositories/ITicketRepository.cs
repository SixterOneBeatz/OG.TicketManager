using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Domain;

namespace OG.TicketManager.Application.Repositories
{
    public interface ITicketRepository : IAsyncRepository<Ticket>
    {
        Task<List<TicketDTO>> GetTicketsAndLastHistory();
        Task<TicketDTO> GetTicketAndLastHistory(int id);
        void ResolveTicket(Ticket entity);
        void DeleteTicket(Ticket entity);
        void UpdateTicket(Ticket entity);
    }
}
