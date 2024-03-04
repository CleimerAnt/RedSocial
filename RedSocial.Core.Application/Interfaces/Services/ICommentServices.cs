using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Application.Viewmodel.ReplyViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface ICommentServices : IGenericService<CommentsViewModel, CommentsPostViewModel, Comment>
    {
        Task<List<CommentsViewModel>> GetCommentByPublication(int publicationId);
        
    }
}
