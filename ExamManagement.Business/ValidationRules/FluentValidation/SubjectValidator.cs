using ExamManagement.Business.DTOs.SubjectDTOs;
using FluentValidation;

namespace ExamManagement.Business.ValidationRules.FluentValidation
{
    public class SubjectValidator : AbstractValidator<CreateSubjectDTO>
    {
        public SubjectValidator()
        {
            RuleFor(s => s.TeacherId).NotEmpty();
            RuleFor(s => s.Name).NotEmpty().MinimumLength(4).WithMessage("Must be at least 4 characters");
        }
    }
}
