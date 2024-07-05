using Entity.Abstract;

namespace Entity.Concrete
{
    public class Subject : IEntity
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }


        #region Relationship
        public Teacher Teacher { get; set; }
        public List<Exam> Exams { get; set; }
        #endregion
    }
}
