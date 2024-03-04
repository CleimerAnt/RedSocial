using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.AccounsViewModel
{
    public class UserPostViewModel
    {
        public string? Id { get; set; }
        public  string? ImgUrl { get; set; }
        [RegularExpression(@"^(809|829|849)-\d{3}-\d{4}$", ErrorMessage = "The phone number must be in Dominican Republic format.")]
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool HasError { get; set; }    
        public string? Error {  get; set; }
        public IFormFile? file { get; set; }
        public string PassWord { get; set; }
        [Compare(nameof(PassWord), ErrorMessage = "The  Password must be the same")]
        public string ConfirnPasword { get; set; }
    }
}
