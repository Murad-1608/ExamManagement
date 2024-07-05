using Entity.Abstract;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Abstract
{
    public interface IRepositoryBase<TEntity>
         where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
    }
}
