﻿using Microsoft.Extensions.Options;
using MimeKit;
using RedSocial.Core.Application.Dtos.Email.Account;
using RedSocial.Core.Domain.Settings;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using RedSocial.Core.Application.Interfaces.Email;

namespace RedSocial.Infraestructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get;  }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value; 
        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                MimeMessage email = new();

                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;    
                BodyBuilder builder = new BodyBuilder();    
                builder.HtmlBody = request.Body;    
                email.Body = builder.ToMessageBody();

                using SmtpClient smtp = new();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

         
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);

                await smtp.SendAsync(email);

                
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
            
            }
        }
    }
}
