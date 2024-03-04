using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Application.Viewmodel.ReplyViewModel;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infraestructure.Persitence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persitence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentsRepository
    {
        private readonly ApplicationContext _Context;
        public CommentRepository(ApplicationContext context) : base(context)
        {
            _Context = context; 
        }

        public async Task<List<Comment>> GetCommentPublicationId(int publicationId)
        {
            var comment = from c in _Context.comments
                          where c.PublicationId == publicationId
                          select new Comment
                          {
                              Id = c.Id,
                              Text = c.Text,
                              UserId = c.UserId,
                              PublicationId = c.PublicationId
                          };
            return comment.ToList();    
        }
        
    }
}
