using RedSocial.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    {
        public string Text { get; set; }
        public int UserId { get; set; }  
        public int PublicationId { get; set; }   
        public int? ReplyId { get; set; }  

        //Navegation Properties
        public ICollection<Publication> Publications { get; set; }
        public User User { get; set; }  
        public ICollection<Reply> Replys { get; set; }
    }
}
