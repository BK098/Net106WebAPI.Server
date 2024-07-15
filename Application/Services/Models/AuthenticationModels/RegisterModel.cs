using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Models.AuthenticationModels
{
    public class RegisterModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? ImagePath { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email không được để trống")
                    .EmailAddress().WithMessage("Email không hợp lệ");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự.")
                    .MaximumLength(255).WithMessage("Mật khẩu không được vượt quá 255 ký tự.")
                    .Matches(@"[A-Z]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết hoa.")
                    .Matches(@"[a-z]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết thường.")
                    .Matches(@"[0-9]+").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt như (!? *.).");

                RuleFor(x => x.ConfirmPassword)
                    .NotEmpty().WithMessage("Mật khẩu xác nhận không được để trống")
                    .Equal(x => x.Password).WithMessage("Mật khẩu xác nhận không khớp với mật khẩu");

                RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("Tên không được để trống");

                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("Họ không được để trống");

                RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("Số điện thoại không được để trống")
                    .Matches(@"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b").WithMessage("Số điện thoại không hợp lệ");
            }   
        }
    }
}