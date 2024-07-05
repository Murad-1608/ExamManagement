using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.ExamDTOs;
using FluentValidation;

namespace ExamManagement.Business.ValidationRules.FluentValidation
{
    public class ExamValidator : AbstractValidator<CreateExamDTO>
    {
        public ExamValidator()
        {
            RuleFor(s => s.SubjectId).NotEmpty();
            RuleFor(s => s.Date).NotEmpty();
        }
    }
}
