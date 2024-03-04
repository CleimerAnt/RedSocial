using RedSocial.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Publication : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public string? MediaPublicationImg { get; set;}
        public string? MediaPublicationVideo { get; set; }
        public string Text { get; set; }
        public string? PostShared { get; set; }
        public int? CommentsId { get; set; }  
        public DateTime Date { get; set; }

        // Navegation Properties
        public ICollection<Comment> Comments { get; set; } 
        public  User User { get; set; }   

    }
}
