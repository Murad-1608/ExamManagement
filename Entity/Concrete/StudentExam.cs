using Entity.Abstract;

namespace Entity.Concrete
{
    public class StudentExam : IEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; }
        public bool IsActive { get; set; }


        #region Relationship
        public Student Student { get; set; }
        public Exam Exam { get; set; }
        #endregion
    }
}
