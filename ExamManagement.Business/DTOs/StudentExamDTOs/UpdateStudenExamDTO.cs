namespace ExamManagement.Business.DTOs.StudentExamDTOs
{
    public class UpdateStudenExamDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; }
    }
}
