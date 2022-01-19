using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OG.TicketManager.Application.Repositories;
using OG.TicketManager.Application.Services;
using OG.TicketManager.Infrastructure.Contexts;
using OG.TicketManager.Infrastructure.Repositories;
using OG.TicketManager.Infrastructure.Services;

namespace OG.TicketManager.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TicketDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SystemConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.AddTransient<IEmailInterceptorService, EmailInterceptorService>();

            return services;
        }
    }
}
