using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.CommentsViewModel
{
    public class CommentsPostViewModel
    {
        public string Id { get; set; }
        [DataType (DataType.Text)]
        public string Text { get; set; }
        public int UserId { get; set; }
     
        public int PublicationId { get; set; }
        [DataType(DataType.Text)]
        public string? ReplyId { get; set; }

        //Navegation Properties
        public ICollection<PublicationViewModel.PublicationViewModel> Publications { get; set; }
        public UserViewModel.dbUserViewModel User { get; set; }
        public ICollection<ReplyViewModel.ReplyViewModel> Replys { get; set; }
    }
}
