﻿using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.CommentsViewModel
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string UserImg { get; set; }
        public string UserName { get; set; }
        public int PublicationId { get; set; }
        public int? ReplyId { get; set; }
        public  List<ReplyViewModel.ReplyViewModel> replyText { get; set; }
        
        //Navegation Properties
        public ICollection<PublicationViewModel.PublicationViewModel> Publications { get; set; }
        public UserViewModel.dbUserViewModel User { get; set; }
        public ICollection<ReplyViewModel.ReplyViewModel> Replys { get; set; }
    }
}
