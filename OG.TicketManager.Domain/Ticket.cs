using OG.TicketManager.Domain.Common;

namespace OG.TicketManager.Domain
{
    public class Ticket : BaseEntity
    {
        public string? Description { get; set; }
        public DateTime? DateResolved { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public ICollection<History>? HistoryRecords { get; set; }
    }
}
