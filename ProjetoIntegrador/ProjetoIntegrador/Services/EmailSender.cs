using MailKit.Net.Smtp;
using MimeKit;

public class EmailSender
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress("SeuApp", "seuemail@gmail.com"));
        mimeMessage.To.Add(new MailboxAddress("", email));
        mimeMessage.Subject = subject;

        mimeMessage.Body = new TextPart("html") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("seuemail@gmail.com", "senha_app");
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
