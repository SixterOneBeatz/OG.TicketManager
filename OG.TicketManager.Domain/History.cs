using OG.TicketManager.Domain.Common;

namespace OG.TicketManager.Domain
{
    public class History : BaseEntity
    {
        public string? Description { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }
    }
}
