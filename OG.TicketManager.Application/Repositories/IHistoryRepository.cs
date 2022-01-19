using OG.TicketManager.Domain;

namespace OG.TicketManager.Application.Repositories
{
    public interface IHistoryRepository : IAsyncRepository<History>
    {
        Task<List<History>> GetHistoryAsync(int ticketId);
        void AddFirstHistory(int id);
        void AddLastHistory(int id);
        void AddHistory(History entity);
    }
}
