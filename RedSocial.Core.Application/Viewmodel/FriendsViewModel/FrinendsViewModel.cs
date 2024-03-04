using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.FriendsViewModel
{
    public class FrinendsViewModel
    {
        public  int Id { get; set; }
        public int FriendId { get; set; }
        public int UserId { get; set; }
        public string FriendFirstName { get; set; }
        public string FriendLastName { get; set; }
        public string FriendImgUrl { get; set; }
        public string FriendUserName { get; set; }
        public List<RedSocial.Core.Application.Viewmodel.PublicationViewModel.PublicationViewModel> PublicationList { get; set; }
        //Navegation Properties
        public ICollection<UserViewModel.dbUserViewModel> Users { get; set; }
    }
}
