using AutoMapper;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel.ReplyViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class ReplyServices : GenericService<ReplyViewModel, ReplyPostViewModel, Reply>, IReplyServices
    {
        private readonly IReplyRepository _replyRepository;
        private readonly IMapper _mapper;
        public ReplyServices(IReplyRepository replyRepository, IMapper mapper): base(replyRepository, mapper)
        {
            _replyRepository = replyRepository;
            _mapper = mapper;
        }
    }
}
