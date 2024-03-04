using Microsoft.EntityFrameworkCore;
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
    public class DboUserRepository : GenericRepository<User>, IDboUserRepository
    {
        private readonly ApplicationContext _context;
        public DboUserRepository(ApplicationContext context) : base(context)
        {

            _context = context; 

        }

        public async Task<User> GetByIdentityId(string Id)
        {
            var User =  await  _context.Set<User>().FirstOrDefaultAsync(u => u.UserIdIndentity == Id);

            return User;

        }
    }
}
