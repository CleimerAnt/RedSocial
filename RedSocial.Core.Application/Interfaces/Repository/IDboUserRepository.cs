using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repository
{
    public interface IDboUserRepository : IGenericRepository<User>
    {
        Task<User> GetByIdentityId(string Id);
        Task<User> GetByName(string name);
    }
}
