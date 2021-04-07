using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data_
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        public GmfctnContext context;
        public DbSet<TEntity> dbSet;

        public GenericRepository(GmfctnContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll() {
            return await dbSet.ToListAsync<TEntity>();
        }
        public async Task<TEntity> GetById(Guid id) {
            return await dbSet.FirstOrDefaultAsync(item => item.Id == id);
        }
        public async Task Create(TEntity element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            await dbSet.AddAsync(element);
        }

        public async Task Delete(Guid id)
        {
            var element = await dbSet.FirstOrDefaultAsync(item => item.Id == id);
            if (element == null)
            {
                throw new ArgumentException(nameof(id));
            }
            dbSet.Remove(element);
        }

        public async Task Update(Guid id,TEntity element)
        {
        }
        public async Task<bool> SaveChanges()
        {
            return ((await context.SaveChangesAsync()) >= 0);
        }
    }
}
