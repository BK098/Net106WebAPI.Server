using Application.Services.Models.UserModels.Base;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Models.UserModels
{
    public class UserProfileForUpdate: UserBaseDto
    {
    }
    public class UserProfileForUpdateValidator: AbstractValidator<UserProfileForUpdate>
    {
        public UserProfileForUpdateValidator()
        {
            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email không được để trống")
                    .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Tên không được để trống");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Họ không được để trống");

            /*RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b").WithMessage("Số điện thoại không hợp lệ");*/
        }
    }
}