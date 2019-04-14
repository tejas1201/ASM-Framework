using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Defines various methods for working with data in the system for entities not inherting <see cref="DomainEntity"/>.
    /// </summary>
    public interface INonDomainEntityRepository<T> where T : class
    {
        /// <summary>
        /// Returns an IQueryable of all items of type T.
        /// </summary>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>An IQueryable of the requested type T.</returns>
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Returns an IQueryable of items of type T.
        /// </summary>
        /// <param name="predicate">A predicate to limit the items being returned.</param>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>An IEnumerable of the requested type T.</returns>
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Adds an entity to the underlying collection.
        /// </summary>
        /// <param name="entity">The entity that should be added.</param>
        void Add(T entity);

        /// <summary>
        /// Adds multiple entities to the underlying DbContext
        /// </summary>
        /// <param name="entities">The entities that should be added.</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Removes an entity from the underlying collection.
        /// </summary>
        /// <param name="entity">The entity that should be removed.</param>
        void Remove(T entity);

        /// <summary>
        /// Removes multiple entities from the underlying collection.
        /// </summary>
        /// <param name="entities">The entity that should be removed.</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
