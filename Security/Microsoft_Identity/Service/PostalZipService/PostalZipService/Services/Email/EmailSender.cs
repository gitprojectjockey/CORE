using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PostalZipService.Services.Email
{
    public class EmailSender : IEMailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation($"{message}");
            return Task.CompletedTask;
        }
    }
}
