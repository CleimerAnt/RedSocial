using AutoMapper;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Application.Viewmodel.ReplyViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RedSocial.Core.Application.Services
{
    public class CommentServices : GenericService<CommentsViewModel, CommentsPostViewModel, Comment>, ICommentServices
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IDboUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IReplyRepository _replyRepository;
        public CommentServices(ICommentsRepository commentsRepository, IMapper mapper, IDboUserRepository userRepository, IReplyRepository replyRepository) : base(commentsRepository, mapper)
        {
            _commentsRepository = commentsRepository;   
            _mapper = mapper;
            _userRepository = userRepository;   
            _replyRepository = replyRepository;
        }


        public async Task<List<CommentsViewModel>> GetCommentByPublication(int publicationId)
        {
            var comments = await _commentsRepository.GetCommentPublicationId(publicationId);
            var users = await _userRepository.GetAllAsync();

            var commentViewModels = new List<CommentsViewModel>();

            foreach (var user in users)
            {

                foreach (var comment in comments.Where(c => c.UserId == user.Id))
                {
                    var replyViewModels = await GetReplies(comment.Id);

                    commentViewModels.Add(new CommentsViewModel
                    {
                        Id = comment.Id,
                        Text = comment.Text,
                        UserName = user.UserName,
                        UserImg = user.ImgUrl,
                        replyText = replyViewModels
                    });
                }
            }

            return commentViewModels.ToList();
        }

        private async Task<List<ReplyViewModel>> GetReplies(int commentId)
        {
            var replies = await _replyRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var reply = new List<ReplyViewModel>();

            foreach (var user in users)
            {
                var userReplies = (from r in replies
                                   join u in users on r.UserId equals u.Id
                                   where r.comentId == commentId && r.UserId == user.Id
                                   select new ReplyViewModel
                                   {
                                       Text = r.Text,
                                       Id = r.Id,
                                       UserImg = user.ImgUrl,
                                       UserName = user.UserName
                                   }).ToList();

                reply.AddRange(userReplies);
            }

            return reply;
        }



    }
}
