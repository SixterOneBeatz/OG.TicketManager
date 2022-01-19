using OG.TicketManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OG.TicketManager.Application.Services
{
    public interface IEmailInterceptorService
    {
        List<string> Intercept();
    }
}
