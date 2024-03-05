using AutoMapper;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class FriendsServices : GenericService<FrinendsViewModel, FrinedsPostViewModel, Friends>, IFriendsServices
    {
        private readonly IRepositoryFriends _repository;
        private readonly IDboUserRepository _userRepository;
        private readonly IPublicationsRepository _publicationsRepository;
        private readonly IMapper _mapper;
        public FriendsServices(IRepositoryFriends repository, IMapper mapper, IDboUserRepository dboUserRepository, IPublicationsRepository publicationsRepository) : base(repository, mapper)
        {
            _repository = repository;  
            _userRepository = dboUserRepository;
            _mapper = mapper;   
            _publicationsRepository = publicationsRepository;
        }

        public async Task<Friends>  GetFriendByFriendId(int Id)
        {
            var friend = await _repository.GetBYFriendId(Id);

            return friend;
        }
        public async Task<List<FrinendsViewModel>> GetFriends(int Id)
        {
            var friendList = await _repository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var publications = await _publicationsRepository.GetAllAsync(); 

            var friends = from f in friendList
                          join u in users
                          on f.UserId equals u.Id
                          where u.Id == Id
                          select new FrinendsViewModel
                          {
                              FriendFirstName = f.FriendFirstName,
                              Id = f.Id,
                              FriendLastName = f.FriendLastName,
                              FriendImgUrl = f.FriendImgUrl,
                              FriendUserName = f.FriendUserName,
                              FriendId = f.FriendId,
                              PublicationList = (from p in publications
                                                 join f2 in friendList on p.UserId equals f.FriendId
                                                 where f2.FriendId == f.FriendId
                                                 select new PublicationViewModel
                                                 {
                                                     Text = p.Text,
                                                     MediaPublicationImg = p.MediaPublicationImg,
                                                     Date = p.Date,
                                                     UserImg = f.FriendImgUrl,
                                                     UserName = f.FriendUserName,
                                                     Id = p.Id
                                                 }).Distinct().ToList()
                          };


            return friends.ToList();
        }
    }
}
