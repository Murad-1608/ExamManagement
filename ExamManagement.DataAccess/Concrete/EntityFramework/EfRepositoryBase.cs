﻿using Entity.Abstract;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
       where TEntity : class, IEntity, new()
       where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;

            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            var deleteEntity = context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;

            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }
        public void Update(TEntity entity)
        {
            using var context = new TContext();
            var updateEntity = context.Entry(entity);
            updateEntity.State = EntityState.Modified;

            context.SaveChanges();
        }
    }
}
