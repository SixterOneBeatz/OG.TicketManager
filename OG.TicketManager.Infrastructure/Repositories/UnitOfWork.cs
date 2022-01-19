using OG.TicketManager.Application.Repositories;
using OG.TicketManager.Domain.Common;
using OG.TicketManager.Infrastructure.Contexts;
using System.Collections;

namespace OG.TicketManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly TicketDbContext _context;
        private protected ITicketRepository _ticketRepository;
        private protected IHistoryRepository _historyRepository;

        public ITicketRepository TicketRepository
            => _ticketRepository = new TicketRepository(_context);

        public IHistoryRepository HistoryRepository
            => _historyRepository = new HistoryRepository(_context);

        public UnitOfWork(TicketDbContext context)
            => _context = context;


        public async Task<int> Complete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();


        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
            {
                _repositories = new();
            }

            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
