using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Models.AuthenticationModels
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        public string? Email { get; set; }
    }

    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");
        }
    }
}