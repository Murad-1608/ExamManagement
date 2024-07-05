using Entity.Concrete;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfTeacherDal : EfRepositoryBase<Teacher, AppDbContext>, ITeacherDal
    {
        public List<Teacher> GetAllWithUserDetails(bool tracking, int pageSize, int pageNumber, Expression<Func<Teacher, bool>> filter = null)
        {
            using var context = new AppDbContext();

            var values = filter == null
                        ? context.Teachers.Include(x => x.AppUser).Skip(pageNumber * pageSize).Take(pageSize)
                        : context.Teachers.Include(x => x.AppUser).Where(filter).Skip(pageNumber * pageSize).Take(pageSize);

            if (tracking)
                values = values.AsNoTracking();

            return values.ToList();
        }
        public Teacher GetWithUserDetails(bool tracking, Expression<Func<Teacher, bool>> filter)
        {
            using var context = new AppDbContext();

            var value = tracking ? context.Teachers.Include(x => x.AppUser).FirstOrDefault(filter)
                                 : context.Teachers.AsNoTracking().Include(x => x.AppUser).FirstOrDefault(filter);

            return value;
        }
    }
}
