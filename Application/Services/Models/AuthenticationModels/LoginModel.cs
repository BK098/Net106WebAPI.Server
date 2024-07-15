using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Models.AuthenticationModels
{
    public class LoginModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email không hợp lệ")
                .NotEmpty().WithMessage("Email không được để trống");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự.")
                .MaximumLength(255).WithMessage("Mật khẩu không được vượt quá 255 ký tự.")
                .Matches(@"[A-Z]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết hoa.")
                .Matches(@"[a-z]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết thường.")
                .Matches(@"[0-9]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt như (!? *.).");
        }
    }
}