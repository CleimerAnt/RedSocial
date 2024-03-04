using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Infraestructure.Persitence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persitence.Repositories
{
    public class GenericRepository<Entity>  : IGenericRepository<Entity> where Entity : class   
    {

        private readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(int Id)
        {
            return await _context.Set<Entity>().FindAsync(Id);
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Remove(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(Entity entity, int Id)
        {
            Entity entry = await _context.Set<Entity>().FindAsync(Id);

            _context.Entry(entry).CurrentValues.SetValues(entity);

            _context.SaveChanges();
        }
    }
}
