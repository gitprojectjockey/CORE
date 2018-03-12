using System.Threading.Tasks;
    
namespace PostalZipService.Services.Email
{
    public interface IEMailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
