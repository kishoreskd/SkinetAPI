using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    /// <summary>
    /// Specification will allows us to clearly say to repostory what we are requring in the manner of decriptive
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }


        /// <summary>
        /// Allows us to add expression like querying database
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }


        /// <summary>
        /// Allows to add the navigation entities
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();


        /// <summary>
        /// This is method is prodcuted because the deriving class only can allow as to use this method-basically a child class can accesss it
        /// </summary>
        /// <param name="includeExpression"></param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }
    }
}
