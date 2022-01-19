using Microsoft.EntityFrameworkCore;
using OG.TicketManager.Application.DTOs;
using OG.TicketManager.Application.Repositories;
using OG.TicketManager.Domain;
using OG.TicketManager.Infrastructure.Contexts;

namespace OG.TicketManager.Infrastructure.Repositories
{
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketDbContext context) : base(context) { }

        public async Task<TicketDTO> GetTicketAndLastHistory(int id)
            => await _context!.Tickets!
            .Where(x => x.Id == id)
            .Include(x => x.HistoryRecords)
            .Select(x => new TicketDTO
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                DateResolved = x.DateResolved,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                LastHistory = x.HistoryRecords!
                               .OrderByDescending(y => y.CreatedDate)
                               .FirstOrDefault()!
                               .Description
                               ?? string.Empty,
            })
            .FirstOrDefaultAsync()
            ?? new();


        public async Task<List<TicketDTO>> GetTicketsAndLastHistory()
            => await _context!.Tickets!
            .Include(x => x.HistoryRecords)
            .Select(x => new TicketDTO
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                DateResolved = x.DateResolved,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                LastHistory = x.HistoryRecords!
                               .OrderByDescending(y => y.CreatedDate)
                               .FirstOrDefault()!
                               .Description
                               ?? string.Empty,
            })
            .ToListAsync()
            ?? new();

        public void DeleteTicket(Ticket entity)
        {
            entity.IsDeleted = true;
            _context!.Tickets!.Attach(entity);
            _context!.Entry(entity).State = EntityState.Modified;
        }

        public void ResolveTicket(Ticket entity)
        {
            entity.DateResolved = DateTime.Now;
            _context!.Tickets!.Attach(entity);
            _context!.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateTicket(Ticket entity)
        {
            if (!entity.IsDeleted || entity.DateResolved is not null)
            {
                _context!.Tickets!.Attach(entity);
                _context!.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
