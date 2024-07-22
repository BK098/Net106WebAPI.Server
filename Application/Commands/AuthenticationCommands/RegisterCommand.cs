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
    public class RegisterCommand : RegisterModel, IRequest<UserMangeResponse> { }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserMangeResponse>
    {
        private readonly IValidator<RegisterModel> _validatorRegister;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly ILocalizationMessage _localization;
        private readonly SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        private IMapper _mapper;

        public RegisterCommandHandler(
            ILogger<RegisterCommandHandler> logger,
            ILocalizationMessage localization,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IValidator<RegisterModel> validatorRegister,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _localization = localization;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _validatorRegister = validatorRegister;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<UserMangeResponse> Handle(RegisterCommand model, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorRegister.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "Đăng ký");
            }
            try
            {
                var isMailExisted = await _userManager.FindByEmailAsync(model.Email);
                if (isMailExisted != null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.Existed, "Email");
                }

                var user = _mapper.Map<AppUser>(model);
                user.UserName = model.Email;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roles = await _signInManager.UserManager.GetRolesAsync(user);
                    if (!roles.Any())
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, "User");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new UserMangeResponse
                    {
                        Message = "User created successfully",
                        IsSuccess = true
                    };
                }
                return ResponseHelper.ErrorResponse(ErrorCode.ValidationError, "Tài khoản");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
