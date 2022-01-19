namespace OG.TicketManager.Application.DTOs
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Description { get; set; } = string.Empty;
        public string? LastHistory { get; set; } = string.Empty;
        public DateTime? DateResolved { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }
}
