using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.AccounsViewModel
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string PassWord { get; set; }
        public string ConfirnPasword { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
