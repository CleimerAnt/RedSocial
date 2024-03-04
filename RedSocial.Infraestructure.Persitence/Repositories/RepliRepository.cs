using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infraestructure.Persitence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persitence.Repositories
{
    public class RepliRepository : GenericRepository<Reply>, IReplyRepository
    {
        private readonly ApplicationContext _Context;
        public RepliRepository(ApplicationContext context) : base(context) 
        {
            _Context = context; 
        }
    }
}
