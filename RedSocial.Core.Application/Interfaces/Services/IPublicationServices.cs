using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Application.Viewmodel.UserViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IPublicationServices : IGenericService<PublicationViewModel, PublicationPostViewModel, Publication>
    {
        Task<List<PublicationViewModel>> GetAllDesc(int Id);

        Task<List<PublicationViewModel>> GetComments(int Id);
    }
}
