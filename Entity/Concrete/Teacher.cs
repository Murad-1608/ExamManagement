using Entity.Abstract;
using Entity.Concrete.Identity;
using Microsoft.AspNetCore.Identity;

namespace Entity.Concrete
{
    public class Teacher : IEntity
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }


        #region Relationship
        public AppUser AppUser { get; set; }
        public List<Subject> Subjects { get; set; }
        #endregion
    }
}
