using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace MyBoard
{
  public class EmailService
  {
    public async Task SendEmailAsync(string email, string subject, string message)
    {
      var emailMessage = new MimeMessage();

      emailMessage.From.Add(new MailboxAddress("Администрация сайта", "adminmalt@ukr.net"));
      emailMessage.To.Add(new MailboxAddress("", email));
      emailMessage.Subject = subject;
      emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
      {
        Text = message
      };

      using var client = new SmtpClient();
      await client.ConnectAsync("smtp.ukr.net", 465, true);
      await client.AuthenticateAsync("adminmalt@ukr.net", "Ruslan280222@");
      await client.SendAsync(emailMessage);

      await client.DisconnectAsync(true);
    }
  }
}
