using Entity.Concrete;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfSubjectDal : EfRepositoryBase<Subject, AppDbContext>, ISubjectDal
    {
        public List<Subject> GetAllWithPagination(bool tracking, int pageSize, int pageNumber, Expression<Func<Subject, bool>> filter = null)
        {
            using var context = new AppDbContext();

            var values = filter == null
                        ? context.Subjects.Include(x => x.Teacher).Include(x => x.Teacher.AppUser).Skip(pageNumber * pageSize).Take(pageSize)
                        : context.Subjects.Include(x => x.Teacher).Include(x => x.Teacher.AppUser).Where(filter).Skip(pageNumber * pageSize).Take(pageSize);

            if (!tracking)
                values = values.AsNoTracking();


            var newValues = values.ToList();

            return values.ToList();

        }
    }
}
