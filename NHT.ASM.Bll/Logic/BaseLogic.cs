using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHT.ASM.Bll.ConfigurationHelpers;
using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Dal;
using NHT.ASM.Dal.Uow;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Bll.Logic
{
    /// <summary>
    /// Base class for logic
    /// </summary>
    /// <typeparam name="TSource">Entity from database</typeparam>
    /// <typeparam name="TDto">Data Transfer Object to transfer data to the client</typeparam>
    public abstract class BaseLogic<TSource,TDto> : ILogic<TSource,TDto> where TSource:DomainEntity where TDto : BaseDto
    {
        private readonly AsmContext _context;
        private readonly IDomainEntityRepository<TSource> _repository;

        //ToDo: Add error handling

        protected BaseLogic(AsmContext context, IDomainEntityRepository<TSource> repository)
        {
            _context = context;
            _repository = repository;
        }
        
        
        /// <inheritdoc />
        public IEnumerable<TDto> GetAll(Expression<Func<TSource, bool>> predicate=null, params Expression<Func<TSource, object>>[] includeProperties)
        {
            IQueryable<TSource> models = _repository.FindAll(predicate,includeProperties);

            var dto = models.MapIQueryableToListOf<TDto>();
            return dto;
        }
        
        /// <inheritdoc />
        public TDto GetById(int id, params Expression<Func<TSource, object>>[] includeProperties)
        {
            TSource model = _repository.FindById(id, includeProperties);

            return model.MapTo<TDto>();
        }

        /// <inheritdoc />
        public TSource GetEntityById(int id, params Expression<Func<TSource, object>>[] includeProperties)
        {
            return _repository.FindById(id, includeProperties);
        }

        /// <inheritdoc />
        public IQueryable<TSource> GetAllAsEntity(Expression<Func<TSource, bool>> predicate, params Expression<Func<TSource, object>>[] includeProperties)
        {
            return _repository.FindAll(predicate, includeProperties);
        }
        
        /// <inheritdoc />
        public virtual void Post(TDto dtoModel)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.Add(dtoModel.MapTo<TSource>());
            }
        }

        /// <inheritdoc />
        public void PostRange(List<TDto> dtoModels)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.AddRange(dtoModels.MapListToListOf<TSource>());
            }
        }

        /// <inheritdoc />
        public virtual void Put(int id, TDto updatedDto)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                TSource model = _repository.FindById(id);
                _repository.Update(model, updatedDto.MapTo<TSource>());
            }
        }
        
        /// <inheritdoc />
        public virtual void Delete(int id)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.Remove(id);
            }
        }

        /// <inheritdoc />
        public virtual void Delete(TSource entity)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.Remove(entity);
            }
        }

        /// <inheritdoc />
        public void DeleteRange(IEnumerable<TSource> entities)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.RemoveRange(entities);
            }
        }

        /// <inheritdoc />
        public IEnumerable<TResult> GetSelectList<TResult>(Expression<Func<TSource, TResult>> columns)
        {
           return _repository.GetSelectList(columns);
        }
    }
}