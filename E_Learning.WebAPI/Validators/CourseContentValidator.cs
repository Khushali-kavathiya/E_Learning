using FluentValidation;
using E_Learning.WebAPI.Contracts;

namespace E_Learning.WebAPI.Validators
{
    /// <summary>
    /// Validator for <see cref="CourseContentContract"/> using FluentValidation.
    /// </summary>
    public class CourseContentValidator : AbstractValidator<CourseContentContract>
    {
        /// Initializes a new instance of the <see cref="CourseContentValidator"/> class.
        public CourseContentValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("Course ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.ContentUrl)
                .NotEmpty().WithMessage("Content URL is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Content URL must be a valid absolute URL.");

            RuleFor(x => x.ContentType)
                .IsInEnum().WithMessage("Content type must be one of the supported values.");

            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order is required.")
                .GreaterThan(0).WithMessage("Order must be a positive integer.");
        }
    }
}