using RedSocial.Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Reply : AuditableBaseEntity
    {
        public  string Text { get; set; }
        public int  UserId { get; set; }
        public int comentId { get; set; }     

        //Nevegations Properties
     
        public Comment comment { get; set; }

    }
}
