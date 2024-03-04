using AutoMapper;
using Newtonsoft.Json;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class PublicationServices : GenericService<PublicationViewModel, PublicationPostViewModel, Publication>, IPublicationServices
    {
        private readonly IMapper _mapper;
        private readonly IPublicationsRepository _publicationsRepository;
        private readonly IDboUserServices _dbUserRepository;
        private readonly ICommentsRepository _commentsRepository;   
        public PublicationServices(IPublicationsRepository publicationsRepository, IMapper mapper,
            IDboUserServices dbUserRepository, ICommentsRepository commentsRepository) : base(publicationsRepository, mapper)
        {
            _mapper = mapper;
            _publicationsRepository = publicationsRepository;
            _dbUserRepository = dbUserRepository;
            _commentsRepository = commentsRepository;

        } 

        public async Task<List<PublicationViewModel>> GetComments(int Id)
        {
            var comments = await _commentsRepository.GetAllAsync();
            var publication = await _publicationsRepository.GetByIdAsync(Id);

            return comments.Where(c => c.PublicationId == publication.Id).Select(c => new PublicationViewModel
            {
                commentText = c.Text
            }).ToList();
        }

        public async Task<List<PublicationViewModel>> GetAllDesc(int ID)
        {
            var publication = await _publicationsRepository.GetAllAsync();
            var user = await _dbUserRepository.GetById(ID);

            return publication.Where(p => p.UserId == user.Id).Select(p => new PublicationViewModel
            {
                Id = p.Id,
                Text = p.Text,
                MediaPublicationImg = p.MediaPublicationImg,
                MediaPublicationVideo = p.MediaPublicationVideo,
                PostShared = p.PostShared,
                UserId = p.UserId,
                CommentsId = p.CommentsId,
                UserName = user.UserName,
                UserImg = user.ImgUrl,
                Date = p.Date
            }).OrderByDescending(p => p.Id) .ToList();

        }
    }
}
