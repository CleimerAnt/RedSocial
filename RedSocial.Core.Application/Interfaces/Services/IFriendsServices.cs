using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IFriendsServices : IGenericService<FrinendsViewModel, FrinedsPostViewModel, Friends>
    {
        Task<List<FrinendsViewModel>> GetFriends(int Id);
        Task<FrinendsViewModel> GetFriendByName(string name);
        Task<FrinendsViewModel> GetFriendByFriendId(int Id);
    }
}
