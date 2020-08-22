using System.Threading.Tasks;

namespace APISorteio.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
