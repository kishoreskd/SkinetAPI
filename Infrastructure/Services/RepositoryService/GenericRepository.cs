using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepositoryService
{
    public class Repository<T> where T : BaseEntity
    {
        #region Fields
        private readonly SkiNetDbContext _context;
        #endregion

        #region Propperties

        #endregion

        #region Constructors
        public Repository(SkiNetDbContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        #endregion
    }
}
