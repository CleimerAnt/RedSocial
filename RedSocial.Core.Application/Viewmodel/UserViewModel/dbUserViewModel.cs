using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.UserViewModel
{
    public class dbUserViewModel
    {
        public  int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string? ImgUrl { get; set; }
        public string PhoneNumber { get; set; }

        //Navegation Properties
        public ICollection<PublicationViewModel.PublicationViewModel> publications { get; set; }
        public ICollection<Friends> friends { get; set; }
        public ICollection<CommentsViewModel.CommentsViewModel> comment { get; set; }
    }
}
