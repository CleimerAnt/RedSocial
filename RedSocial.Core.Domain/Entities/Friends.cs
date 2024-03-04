using RedSocial.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Friends : AuditableBaseEntity
    {
        public int FriendId { get; set; }    
        public int UserId { get; set; }
        public string FriendFirstName { get; set; }
        public string FriendLastName { get; set; }    
        public string FriendImgUrl { get; set; }    
        public string FriendUserName { get; set; }

        //Navegation Properties
        public ICollection<User> Users { get; set; }
    }
}
