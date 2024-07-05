using Entity.Abstract;
using Entity.Concrete;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Abstract
{
    public interface IStudentDal : IRepositoryBase<Student>
    {
        List<Student> GetAllWithUserDetails(bool tracking, int pageSize, int pageNumber, Expression<Func<Student, bool>> filter = null);
        Student GetWithUserDetails(bool tracking, Expression<Func<Student, bool>> filter);
    }
}
