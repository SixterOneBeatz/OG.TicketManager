using MediatR;
using OG.TicketManager.Application.Features.Ticket.Commands;
using OG.TicketManager.Application.Services;

namespace OG.TicketManager.API.HostedServices
{
    public class EmailInterceptorHostedService : IHostedService
    {
        private readonly IEmailInterceptorService _emailInterceptorService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        private Timer _timer = null!;
        public EmailInterceptorHostedService(IEmailInterceptorService emailInterceptorService, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _emailInterceptorService = emailInterceptorService;
            _configuration = configuration;
            _mediator = serviceScopeFactory.CreateScope()
                                           .ServiceProvider
                                           .GetRequiredService<IMediator>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(TimerCallback!, null, TimeSpan.Zero, TimeSpan.FromSeconds(GetInterval()));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void TimerCallback(object state)
        {
            var tickets = _emailInterceptorService.Intercept();
            tickets?.ForEach(async x =>
            {
                CreateTicketCommand command = new() { Description = x };
                int result = await _mediator.Send(command);
            });
        }

        private int GetInterval()
            => int.TryParse(_configuration["EmailSettings:Interval"], out int n) ? n : 60;

    }
}
