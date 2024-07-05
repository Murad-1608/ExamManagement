using ExamManagement.Business.DTOs.StudentDTOs;
using FluentValidation;

namespace ExamManagement.Business.ValidationRules.FluentValidation
{
    public class StudentValidator : AbstractValidator<CreateStudentDTO>
    {
        public StudentValidator()
        {
            RuleFor(s => s.FirstName).NotEmpty().MinimumLength(4).WithMessage("Must be at least 4 characters");
            RuleFor(s => s.UserName).NotEmpty().MinimumLength(4).WithMessage("Must be at least 4 characters");
            RuleFor(s => s.LastName).NotEmpty().MinimumLength(4).WithMessage("Must be at least 4 characters");
            RuleFor(s => s.Email).NotEmpty().MinimumLength(4).WithMessage("Must be at least 4 characters");
            RuleFor(s => s.Password).NotEmpty().MinimumLength(6).WithMessage("Must be at least 6 characters");
            RuleFor(s => s.Group).NotEmpty().WithMessage("Group is required");
            RuleFor(s => s.PhoneNumber).Matches(@"^\d+$").WithMessage("Only numeric characters are allowed.");
        }
    }
}
