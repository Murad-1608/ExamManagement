namespace ExamManagement.Business.DTOs.ExamDTOs
{
    public record CreateExamDTO
    {
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
    }
}
