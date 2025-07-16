using FluentValidation;
using E_Learning.WebAPI.Contracts;

namespace E_Learning.WebAPI.Validators;

// <summary>
// Validator for <see cref="UserContract"/> using FluentValidation.
/// Ensures required fields like Email, Password, and FullName meet validation rules.
// </summary>

public class UserContractValidator : AbstractValidator<UserContract>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserContractValidator"/> class.
    /// Sets up validation rules for Email, Password, and FullName.
    /// </summary>
    public UserContractValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email fomate.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MinimumLength(2).WithMessage("Full name must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
    }
}