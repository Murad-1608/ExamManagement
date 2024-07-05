using Entity.Concrete;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Abstract
{
    public interface IStudentExamDal : IRepositoryBase<StudentExam>
    {
        List<StudentExam> GetAllWithPagination(bool tracking, int pageSize, int pageNumber, Expression<Func<StudentExam, bool>> filter = null);
    }
}
