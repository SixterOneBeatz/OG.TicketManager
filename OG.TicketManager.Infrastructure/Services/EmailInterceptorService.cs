using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OG.TicketManager.Application.Services;

namespace OG.TicketManager.Infrastructure.Services
{
    public class EmailInterceptorService : IEmailInterceptorService
    {
        private readonly string _user, _pass, _domain, _ewsUrl, _blackList;
        private readonly int _topItems;

        public ILogger<EmailInterceptorService> Logger { get; }
        public EmailInterceptorService(IConfiguration configuration, ILogger<EmailInterceptorService> logger)
        {
            _user = configuration["EmailSettings:User"];
            _pass = configuration["EmailSettings:Pass"];
            _domain = configuration["EmailSettings:Domain"];
            _ewsUrl = configuration["EmailSettings:EwsUrl"];
            _blackList = configuration["EmailSettings:BlackList"];
            _topItems = int.TryParse(configuration["EmailSettings:TopItems"], out int n) ? n : 10;
            Logger = logger;
        }

        public List<string> Intercept()
        {
            List<string> ticketsToInsert = new();
            try
            {
                if (!string.IsNullOrEmpty(_user))
                {
                    ExchangeService service = new(ExchangeVersion.Exchange2013_SP1);
                    service.Url = new Uri(_ewsUrl);
                    service.Credentials = new WebCredentials(_user, _pass, _domain);

                    Mailbox mb = new(_user);
                    FolderId fid = new(WellKnownFolderName.Inbox, mb);
                    Folder inbox = Folder.Bind(service, fid);

                    if (inbox != null)
                    {
                        List<Item> items = inbox.FindItems(CreateQueryString(), new(_topItems))
                                                .OrderBy(x => x.DateTimeReceived)
                                                .ToList();


                        foreach (var item in items)
                        {
                            EmailMessage mail = (EmailMessage)item;
                            mail.Load(new(BasePropertySet.FirstClassProperties, EmailMessageSchema.TextBody, EmailMessageSchema.From));
                            mail.IsRead = true;
                            if (!string.IsNullOrEmpty(mail.TextBody.Text))
                            {
                                ticketsToInsert.Add(@$"{mail.Subject ?? string.Empty}");
                            }
                            mail.Update(ConflictResolutionMode.AlwaysOverwrite);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Interceptor error", ex);
            }

            return ticketsToInsert;
        }

        private string CreateQueryString()
           => $"isread:false {(string.IsNullOrEmpty(_blackList) ? string.Empty : $"AND NOT from:(\"{string.Join("\" OR \"", _blackList.Split(";"))}\")")}";
    }
}
