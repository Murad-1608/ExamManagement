using Entity.Concrete;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfStudentExamDal : EfRepositoryBase<StudentExam, AppDbContext>, IStudentExamDal
    {
        public List<StudentExam> GetAllWithPagination(bool tracking, int pageSize, int pageNumber, Expression<Func<StudentExam, bool>> filter = null)
        {
            using var context = new AppDbContext();

            var values = filter == null
                        ? context.StudentExams.Include(x => x.Student.AppUser).Include(x => x.Exam.Subject).Skip(pageNumber * pageSize).Take(pageSize)
                        : context.StudentExams.Include(x => x.Student.AppUser).Include(x => x.Exam.Subject).Where(filter).Skip(pageNumber * pageSize).Take(pageSize);

            if (!tracking)
                values = values.AsNoTracking();


            var newValues = values.ToList();

            return values.ToList();

        }
    }
}
