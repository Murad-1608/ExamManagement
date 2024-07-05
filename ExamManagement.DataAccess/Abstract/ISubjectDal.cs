using Entity.Concrete;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Abstract
{
    public interface ISubjectDal : IRepositoryBase<Subject>
    {
        List<Subject> GetAllWithPagination(bool tracking, int pageSize, int pageNumber, Expression<Func<Subject, bool>> filter = null);
    }
}
