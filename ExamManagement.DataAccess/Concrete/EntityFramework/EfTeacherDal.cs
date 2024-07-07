using Entity.Concrete;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfTeacherDal : EfRepositoryBase<Teacher, AppDbContext>, ITeacherDal
    {
        public List<Teacher> GetAllWithUserDetails(bool tracking, int pageSize, int pageNumber, Expression<Func<Teacher, bool>> filter = null)
        {
            using var context = new AppDbContext();

            var table = context.Teachers.AsQueryable();

            if (!tracking)
                table = table.AsNoTracking();

            var values = filter == null
                        ? table.Include(x => x.AppUser).Skip(pageNumber * pageSize).Take(pageSize)
                        : table.Include(x => x.AppUser).Where(filter).Skip(pageNumber * pageSize).Take(pageSize);

            return values.ToList();
        }
        public Teacher GetWithUserDetails(bool tracking, Expression<Func<Teacher, bool>> filter)
        {
            using var context = new AppDbContext();

            var table = context.Teachers.AsQueryable();

            var value = tracking ? table.Include(x => x.AppUser).FirstOrDefault(filter)
                                 : table.AsNoTracking().Include(x => x.AppUser).FirstOrDefault(filter);

            return value;
        }
    }
}
