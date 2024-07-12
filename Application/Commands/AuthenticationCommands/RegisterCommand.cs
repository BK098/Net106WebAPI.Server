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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Commands.AuthenticationCommands
{
    public class RegisterCommand : RegisterModel, IRequest<UserMangeResponse>
    {
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserMangeResponse>
    {
        private readonly IValidator<RegisterModel> _validatorRegister;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly ILocalizationMessage _localization;
        private readonly SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private IConfiguration _configuration;
        private IMapper _mapper;

        public RegisterCommandHandler(
            ILogger<RegisterCommandHandler> logger,
            ILocalizationMessage localization,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IValidator<RegisterModel> validatorRegister,
            IMapper mapper)
        {
            _logger = logger;
            _localization = localization;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _validatorRegister = validatorRegister;
            _mapper = mapper;
        }
        public async Task<UserMangeResponse> Handle(RegisterCommand model, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorRegister.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "Loại hàng");
            }
            try
            {
                // xử lí lại ở kia
                if (model.Password != model.ConfirmPassword)
                {
                    return new UserMangeResponse
                    {
                        Message = "Confirm pass doesn't match the pass",
                        IsSuccess = false
                    };
                }

                var isMailExisted = await _userManager.FindByEmailAsync(model.Email);
                if (isMailExisted != null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.Existed, validationResult.Errors, _localization, "Email");
                }
                var user = _mapper.Map<AppUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roles = await _signInManager.UserManager.GetRolesAsync(user);
                    if (!roles.Any())
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, "Customer");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new UserMangeResponse
                    {
                        Message = "User created successfully",
                        IsSuccess = true
                    };
                }
                return ResponseHelper.ErrorResponse(ErrorCode.ValidationError, validationResult.Errors, _localization, "Tài khoản");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
