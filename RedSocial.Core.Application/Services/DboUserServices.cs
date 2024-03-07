using AutoMapper;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.UserViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class DboUserServices : GenericService<dbUserViewModel, dbUserPostViewModel, User>, IDboUserServices
    {
        private readonly IDboUserRepository _services;
        private readonly IMapper _mapper;
        public DboUserServices(IDboUserRepository services, IMapper mapper) : base(services, mapper)
        { 
            _services = services;
            _mapper = mapper;   
        }

        public async Task<dbUserViewModel> GetForIdentityId(string Id)
        {
          var User = await _services.GetByIdentityId(Id);

            dbUserViewModel userVm = _mapper.Map<dbUserViewModel>(User);

            return userVm;
        }

        public async Task<dbUserViewModel> GetByName(string name)
        {
            var user = await _services.GetByName(name);

            dbUserViewModel userViewModel = _mapper.Map<dbUserViewModel>(user);

            return userViewModel;
        }
    }
}
