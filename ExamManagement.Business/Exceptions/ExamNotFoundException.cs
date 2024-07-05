namespace ExamManagement.Business.Exceptions
{
    public class ExamNotFoundException : Exception
    {
        public ExamNotFoundException(string? message) : base(message)
        {
        }
    }
}
