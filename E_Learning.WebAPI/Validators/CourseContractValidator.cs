using E_Learning.WebAPI.Contracts;
using FluentValidation;

namespace E_Learning.WebAPI.Validators;

/// <summary>
/// Validator for <see cref="CourseContract"/> using FluentValidation.
/// Ensures required fields like Title, Description, Level, and Price are properly validated.
/// </summary>
public class CourseContractValidator : AbstractValidator<CourseContract>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CourseContractValidator"/> class.
    /// Sets up validation rules for creating or updating a course.
    /// </summary>
    public CourseContractValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Course title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.Level)
            .NotEmpty().WithMessage("Course level is required.")
            .Must(level => new[] { "Beginner", "Intermediate", "Advanced" }.Contains(level))
            .WithMessage("Level must be one of: Beginner, Intermediate, Advanced.");

        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is required.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be zero or greater.");

        RuleFor(x => x.IsFree)
            .NotNull().WithMessage("IsFree must be specified.");

        RuleFor(x => x)
            .Must(c => c.IsFree || c.Price > 0)
            .WithMessage("Price must be greater than zero if course is not free.");
    }
}