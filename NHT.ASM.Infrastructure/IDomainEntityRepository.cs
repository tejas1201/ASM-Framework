using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Defines various methods for working with data in the system.
    /// </summary>

    public interface IDomainEntityRepository<T>
    {
        /// <summary>
        /// Finds an item by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the item in the database.</param>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>The requested item when found, or null otherwise.</returns>
        T FindById(int id, params Expression<Func<T, object>>[] includeProperties);

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
        /// Adds an entity to the underlying Db DataContextFactory.GetDataContext().
        /// </summary>
        /// <param name="entityInDb">The entity that should be updated</param>
        /// <param name="newValues">The values to update with</param>
        void Update(T entityInDb, T newValues);

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
        /// Removes an entity from the underlying collection.
        /// </summary>
        /// <param name="id">The ID of the entity that should be removed.</param>
        void Remove(int id);

        /// <summary>
        /// Removes multiple entities from the underlying collection.
        /// </summary>
        /// <param name="entities">The entity that should be removed.</param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// Used for creating Select list for UI of entity based on the columns specified for Text and Value fields.
        /// It will retrieve data only for columns which are specified
        /// </summary>
        /// <typeparam name="TResult">items of Type T</typeparam>
        /// <param name="columns">list of columns to retrieve data</param>
        /// <returns></returns>
        IQueryable<TResult> GetSelectList<TResult>(Expression<Func<T, TResult>> columns);
    }
}