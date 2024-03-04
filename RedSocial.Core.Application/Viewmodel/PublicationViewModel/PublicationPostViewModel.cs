using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.PublicationViewModel;

public class PublicationPostViewModel
{
    public  int Id { get; set; }
    public int? UserId { get; set; }
    public string? MediaPublicationImg { get; set; }
    public string? MediaPublicationVideo { get; set; }
  //  [Required (ErrorMessage = "you must enter text")]
    public string? Text { get; set; }
    public string? PostShared { get; set; }
    public int? CommentsId { get; set; }
    public IFormFile? file { get; set; }    
    public string IdenityUserId { get; set; }
    public DateTime Date { get; set; }

    // Navegation Properties
    public ICollection<CommentsViewModel.CommentsViewModel>? Comments { get; set; }
    public UserViewModel.dbUserViewModel? User { get; set; }
}
