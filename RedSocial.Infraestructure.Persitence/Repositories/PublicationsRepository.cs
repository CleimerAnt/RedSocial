using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infraestructure.Persitence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persitence.Repositories
{
    public class PublicationsRepository : GenericRepository<Publication>, IPublicationsRepository
    {
        private readonly ApplicationContext _Context;
        public PublicationsRepository(ApplicationContext context) : base(context)
        {
            _Context = context; 
        }

        
    }
}
