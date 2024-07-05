using Entity.Abstract;
using Entity.Concrete.Identity;

namespace Entity.Concrete
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string Group { get; set; }
        public bool IsActive { get; set; }


        #region Relationship
        public AppUser AppUser { get; set; }
        public List<StudentExam> StudentExams { get; set; }
        #endregion
    }
}
