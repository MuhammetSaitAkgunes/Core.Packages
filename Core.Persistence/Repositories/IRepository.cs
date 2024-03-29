﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories
{
    public interface IRepository<TEntity, TEntityId> : IQuery<TEntity> where TEntity : Entity<TEntityId>
    {
        TEntity? Get(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true);

        Paginate<TEntity> GetList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true);

        Paginate<TEntity> GetListByDynamic(
            DynamicQuery dynamic,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true);

        bool Any(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool enableTracking = true,
            bool withDeleted = false);

        TEntity Add(TEntity entity);

        ICollection<TEntity> AddRange(ICollection<TEntity> entity);

        TEntity Update(TEntity entity);

        ICollection<TEntity> UpdateRange(ICollection<TEntity> entity);

        TEntity Delete(TEntity entity, bool permanent = false);

        ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false);
    }
}