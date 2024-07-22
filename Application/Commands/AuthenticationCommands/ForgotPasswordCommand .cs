using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.AuthenticationModels;
using Application.Services.Models.Base;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Application.Commands.AuthenticationCommands
{
    public class ForgotPasswordCommand : ForgotPasswordModel, IRequest<UserMangeResponse> { }
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, UserMangeResponse>
    {
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ForgotPasswordModel> _validator;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ForgotPasswordCommandHandler(
            ILogger<ForgotPasswordCommandHandler> logger,
            UserManager<AppUser> userManager,
            IValidator<ForgotPasswordModel> validator,
            IMapper mapper,
            ILocalizationMessage localization)
        {
            _logger = logger;
            _userManager = userManager;
            _validator = validator;
            _mapper = mapper;
            _localization = localization;
        }

        public async Task<UserMangeResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.ValidationError, validationResult.Errors, _localization, "quên mật khẩu");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "Email");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("146cdb896bf3c5", "683afa8c4a9d41"),
                EnableSsl = true
            };
            client.Send("buikhang122004@gmail.com", $"{user.Email}", "Password Reset", $"Đây là key:{encodedToken} để bạn có thể khởi lại mật khẩu");
            //var resetLink = $"https://yourdomain.com/reset-password?email={request.Email}&token={encodedToken}";

            // Send email logic here
            // Example: await _emailService.SendPasswordResetEmail(user.Email, resetLink);

            return new UserMangeResponse
            {
                IsSuccess = true,
                Message = "Link đặt lại mật khẩu đã được gửi đến email của bạn"
            };
        }
    }
}
