using Azure.Core;
using Entity.Abstract;
using Entity.Concrete;
using ExamManagement.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamManagement.DataAccess.Concrete.EntityFramework
{
    public class EfStudentDal : EfRepositoryBase<Student, AppDbContext>, IStudentDal
    {
        public List<Student> GetAllWithUserDetails(bool tracking, int pageSize, int pageNumber, Expression<Func<Student, bool>> filter = null)
        {
            using var context = new AppDbContext();

            var table = context.Students.AsQueryable();

            if (!tracking)
                table = table.AsNoTracking();

            var values = filter == null
                        ? table.Include(x => x.AppUser).Skip(pageNumber * pageSize).Take(pageSize)
                        : table.Include(x => x.AppUser).Where(filter).Skip(pageNumber * pageSize).Take(pageSize);

            return values.ToList();
        }

        public Student GetWithUserDetails(bool tracking, Expression<Func<Student, bool>> filter)
        {
            using var context = new AppDbContext();
            var table = context.Students.AsQueryable();

            var value = tracking ? table.Include(x => x.AppUser).FirstOrDefault(filter)
                                 : table.AsNoTracking().Include(x => x.AppUser).FirstOrDefault(filter);

            return value;
        }
    }
}
