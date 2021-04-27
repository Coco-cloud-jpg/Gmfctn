using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data_.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public GmfctnContext Context { get; set; }
        public DbSet<TEntity> DbSet { get; set; }
        Task<IEnumerable<TEntity>> GetAll(CancellationToken Cancel);
        Task<TEntity> GetById(Guid Id, CancellationToken Cancel);
        Task Create(TEntity Element, CancellationToken Cancel);
        Task Delete(Guid Id, CancellationToken Cancel);
        void Update(TEntity Element);
    }
}
