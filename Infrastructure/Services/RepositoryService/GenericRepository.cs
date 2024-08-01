using Core.Entities;
using Core.Specifications;
using Infrastructure.Services.RepositoryService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.GenericRepositoryService
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly SkiNetDbContext _context;
        #endregion

        #region Propperties

        #endregion

        #region Constructors
        public GenericRepository(SkiNetDbContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public Task<T> GetIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        #endregion
    }
}
