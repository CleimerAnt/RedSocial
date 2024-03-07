using RedSocial.Core.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string UserIdIndentity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? ImgUrl { get; set; }
        public string PhoneNumber { get; set; }

        //Navegation Properties
        public ICollection<Publication> publications { get; set; }
        public ICollection<Friends> friends { get; set; }
        public ICollection<Comment> comment { get; set; }    
    }
}
