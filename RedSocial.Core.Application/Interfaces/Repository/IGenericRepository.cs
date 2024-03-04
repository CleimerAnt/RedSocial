using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class 
    {
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int Id);
        Task<Entity> AddAsync(Entity entity);
        Task Remove(Entity entity);
        Task UpdateAsync(Entity entity, int Id);

    }
}
