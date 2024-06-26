﻿using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repository
{
    public interface IRepositoryFriends : IGenericRepository<Friends>
    {
        Task<Friends> GetBYFriendId(int Id);

        Task<Friends> GetByName(string name);
    }
}
