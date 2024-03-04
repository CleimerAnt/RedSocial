using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.ReplyViewModel
{
    public class ReplyViewModel
    {
        public  int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int comentId { get; set; }
        public string UserImg {  get; set; }    
        public string UserName { get; set; }    
      

        //Nevegations Properties
  
        public CommentsViewModel.CommentsViewModel comment { get; set; }

    }
}
