using OG.TicketManager.Domain.Common;

namespace OG.TicketManager.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        ITicketRepository TicketRepository { get; }
        IHistoryRepository HistoryRepository { get; }
        Task<int> Complete();
    }
}
