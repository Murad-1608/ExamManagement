using ExamManagement.Business.DTOs.StudentDTOs;
using ExamManagement.Business.DTOs.StudentExamDTOs;
using FluentValidation;

namespace ExamManagement.Business.ValidationRules.FluentValidation
{
    public class StudentExamValidator : AbstractValidator<CreateStudentExamDTO>
    {
        public StudentExamValidator()
        {
            RuleFor(s => s.ExamId).NotEmpty();
            RuleFor(s => s.StudentId).NotEmpty();
            RuleFor(s => s.Score).NotEmpty();
        }
    }
}
