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
        [Required (ErrorMessage = "The phone number field is required")]
        [DataType (DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The User name field is required")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "The Email field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Last Name field is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Fisrt Name field is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        public bool HasError { get; set; }    
        public string? Error {  get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? file { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Compare(nameof(PassWord), ErrorMessage = "The  Password must be the same")]
        [Required(ErrorMessage = "The Conrfirn Password field is required")]
        [DataType(DataType.Password)]
        public string ConfirnPasword { get; set; }
    }
}
