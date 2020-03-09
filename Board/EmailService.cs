﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MyBoard
{
  public class EmailService
  {
    public async Task SendEmailAsync(string email, string subject, string message)
    {
      var emailMessage = new MimeMessage();

      emailMessage.From.Add(new MailboxAddress("Администрация сайта", "malt22222222@gmail.com"));
      emailMessage.To.Add(new MailboxAddress("", email));
      emailMessage.Subject = subject;
      emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
      {
        Text = message
      };

      using var client = new SmtpClient();
      await client.ConnectAsync("smtp.gmail.com", 465,true);
      await client.AuthenticateAsync("malt22222222@gmail.com", "Ruslan280222@");
      await client.SendAsync(emailMessage);

      await client.DisconnectAsync(true);
    }
  }
}
