using Microsoft.EntityFrameworkCore;
using OG.TicketManager.Domain;
using OG.TicketManager.Domain.Common;

namespace OG.TicketManager.Infrastructure.Contexts
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                        .HasMany(x => x.HistoryRecords)
                        .WithOne(x => x.Ticket)
                        .HasForeignKey(x => x.TicketId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<History>? Histories { get; set; }
    }
}
