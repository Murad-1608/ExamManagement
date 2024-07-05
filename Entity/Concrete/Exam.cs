using Entity.Abstract;

namespace Entity.Concrete
{
    public class Exam : IEntity
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }


        #region Relationship
        public Subject Subject { get; set; }
        public List<StudentExam> StudentExams { get; set; }
        #endregion
    }
}
