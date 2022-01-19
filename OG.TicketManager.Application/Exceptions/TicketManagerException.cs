namespace OG.TicketManager.Application.Exceptions
{
    public class TicketManagerException
    {
        public int StatusCode { get; set; }
        public string? Details { get; set; }
        public string? Message { get; set; }

        public TicketManagerException(int statusCode, string? message = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
            Details = details;
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Request sended has errors",
                401 => "You are not authorized",
                404 => "Resource requested was not found",
                500 => "Internal server erros",
                _ => string.Empty
            };
        }
    }
}
