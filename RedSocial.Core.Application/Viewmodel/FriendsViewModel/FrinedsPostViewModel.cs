using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.FriendsViewModel
{
    public class FrinedsPostViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Debe seleccionar un Usuario")]
        public int FriendId { get; set; }
        public int? UserId { get; set; }
        [DataType (DataType.Text)]
        public string? FriendFirstName { get; set; }
        [DataType(DataType.Text)]
        public string? FriendLastName { get; set; }
        [DataType(DataType.Text)]
        public string? FriendImgUrl { get; set; }
        [DataType(DataType.Text)]
        public string? FriendUserName { get; set; }


        //Navegation Properties
        public ICollection<UserViewModel.dbUserViewModel>? Users { get; set; }
    }
}
