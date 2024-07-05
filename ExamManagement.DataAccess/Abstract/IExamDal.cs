using Entity.Concrete;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Abstract
{
    public interface IExamDal : IRepositoryBase<Exam>
    {
        List<Exam> GetAllWithPagination(bool tracking, int pageSize, int pageNumber, Expression<Func<Exam, bool>> filter = null);
    }
}
