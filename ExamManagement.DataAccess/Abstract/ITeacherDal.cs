using Entity.Concrete;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Abstract
{
    public interface ITeacherDal : IRepositoryBase<Teacher>
    {
        List<Teacher> GetAllWithUserDetails(bool tracking, int pageSize, int pageNumber, Expression<Func<Teacher, bool>> filter = null);
        Teacher GetWithUserDetails(bool tracking, Expression<Func<Teacher, bool>> filter);
    }
}
