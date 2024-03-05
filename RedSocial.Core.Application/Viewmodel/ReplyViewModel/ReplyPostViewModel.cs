using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.ReplyViewModel
{
    public class ReplyPostViewModel
    {
        public  int Id { get; set; }
        [DataType(DataType.Text)]
        public string Text { get; set; }
        public int UserId { get; set; }
        public int comentId { get; set; }
   

        //Nevegations Properties
       
        public CommentsViewModel.CommentsViewModel comment { get; set; }
    }
}
