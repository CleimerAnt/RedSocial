using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel;

public class dbUserPostViewModel
{
    [DataType(DataType.Text)]
    public string UserIdIndentity { get; set; }
    public  int Id { get; set; }
    [DataType(DataType.Text)]
    public string FirstName { get; set; }
    [DataType(DataType.Text)]
    public string LastName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Text)]
    public string UserName { get; set; }
    [DataType(DataType.Text)]
    public string PassWord { get; set; }
    [DataType(DataType.Text)]
    public string? ImgUrl { get; set; }
    [DataType(DataType.Text)]
    public string PhoneNumber { get; set; }

    //Navegation Properties
    public ICollection<PublicationViewModel.PublicationViewModel> publications { get; set; }
    public ICollection<FriendsViewModel.FrinendsViewModel> friends { get; set; }
    public ICollection<CommentsViewModel.CommentsViewModel> comment { get; set; }
}
