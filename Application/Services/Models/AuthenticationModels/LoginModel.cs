using FluentValidation;

namespace Application.Services.Models.AuthenticationModels
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class LoginModelValidator: AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Không được để trống");
        }
    }
}
