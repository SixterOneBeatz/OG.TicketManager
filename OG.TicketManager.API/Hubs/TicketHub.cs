using Microsoft.AspNetCore.SignalR;

namespace OG.TicketManager.API.Hubs
{
    public class TicketHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
