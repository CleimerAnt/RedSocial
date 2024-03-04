using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Viewmodel.PublicationViewModel
{
    public class PublicationViewModel
    {
        public  int Id { get; set; }
        public int UserId { get; set; }
        public string? MediaPublicationImg { get; set; }
        public string? MediaPublicationVideo { get; set; }
        public string Text { get; set; }
        public string PostShared { get; set; }
        public int? CommentsId { get; set; }
        public string UserName { get; set; }
        public  string UserImg { get; set; }
        public string commentText { get; set; }
        public DateTime Date { get; set; }
        public List<RedSocial.Core.Application.Viewmodel.CommentsViewModel.CommentsViewModel> comments { get; set; }    
        

        // Navegation Properties
        public ICollection<CommentsViewModel.CommentsViewModel> Comments { get; set; }
        public UserViewModel.dbUserViewModel User { get; set; }
    }
}
