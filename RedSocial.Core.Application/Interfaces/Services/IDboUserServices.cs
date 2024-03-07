using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.UserViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IDboUserServices : IGenericService<dbUserViewModel, dbUserPostViewModel, User>
    {
        Task<dbUserViewModel> GetForIdentityId(string Id);
        Task<dbUserViewModel> GetByName(string name);
    }
}
