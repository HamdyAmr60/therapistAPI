using System.Net.Mail;
using System.Net;

namespace therapist.API.Helpers
{
    public static class SendEmail
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.yourserver.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email", "your-password"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("Hamdyamr60@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);
            await client.SendMailAsync(mailMessage);
        }
    }
}
