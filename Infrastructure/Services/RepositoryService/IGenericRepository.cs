using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepositoryService
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetIdAsync(int id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();


        Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec);
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);

    }
}
