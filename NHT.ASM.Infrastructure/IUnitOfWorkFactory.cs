using System.Data.Entity;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Creates new instances of a unit of Work.
    /// </summary>
    public interface IUnitOfWorkFactory<T> where T : DbContext
    {
        /// <summary>
        /// Creates a new instance of a unit of work
        /// </summary>
        IUnitOfWork Create(T context);
    }
}
