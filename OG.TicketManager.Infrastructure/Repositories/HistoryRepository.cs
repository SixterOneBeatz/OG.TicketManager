using Microsoft.EntityFrameworkCore;
using OG.TicketManager.Application.Repositories;
using OG.TicketManager.Domain;
using OG.TicketManager.Infrastructure.Contexts;

namespace OG.TicketManager.Infrastructure.Repositories
{
    public class HistoryRepository : RepositoryBase<History>, IHistoryRepository
    {
        public HistoryRepository(TicketDbContext context) : base(context) { }

        public void AddFirstHistory(int id)
        {
            History history = new()
            {
                TicketId = id,
                Description = "First History",
            };

            _context.Add(history);
        }

        public void AddHistory(History entity)
        {

            History? lastHistory = _context!.Histories!
                .AsQueryable()
                .Where(x => x.TicketId == entity.TicketId)
                .Select(x => x)
                .OrderByDescending(x => x.CreatedDate)
                .Take(1)
                .FirstOrDefault() ?? null;

            if (lastHistory?.Description != entity.Description)
                _context.Histories.Add(entity);
        }

        public void AddLastHistory(int id)
        {
            History history = new()
            {
                TicketId = id,
                Description = "Last History",
            };

            _context.Add(history);
        }

        public async Task<List<History>> GetHistoryAsync(int ticketId)
            => await _context!.Histories!.Where(x => x.TicketId == ticketId)
                                         .OrderByDescending(x => x.CreatedDate)
                                         .ToListAsync();
    }
}
