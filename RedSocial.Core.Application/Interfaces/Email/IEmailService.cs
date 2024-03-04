using RedSocial.Core.Application.Dtos.Email.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest emailRequest);
    }
}
