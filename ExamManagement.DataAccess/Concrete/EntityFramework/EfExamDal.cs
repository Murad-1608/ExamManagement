using Entity.Concrete;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfExamDal : EfRepositoryBase<Exam, AppDbContext>, IExamDal
    {
        public List<Exam> GetAllWithPagination(bool tracking, int pageSize, int pageNumber, Expression<Func<Exam, bool>> filter = null)
        {
            using var context = new AppDbContext();

            var values = filter == null
                        ? context.Exams.Include(x => x.Subject).Include(x => x.Subject.Teacher).Include(x => x.Subject.Teacher.AppUser).Skip(pageNumber * pageSize).Take(pageSize)
                        : context.Exams.Include(x => x.Subject).Include(x => x.Subject.Teacher).Include(x => x.Subject.Teacher.AppUser).Where(filter).Skip(pageNumber * pageSize).Take(pageSize);

            if (!tracking)
                values = values.AsNoTracking();


            var newValues = values.ToList();

            return values.ToList();
        }
    }
}
