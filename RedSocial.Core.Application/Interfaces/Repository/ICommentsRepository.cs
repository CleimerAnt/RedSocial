using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repository
{
    public interface ICommentsRepository: IGenericRepository<Comment>
    {
        
        Task<List<Comment>> GetCommentPublicationId(int publicationId);
    }
}
