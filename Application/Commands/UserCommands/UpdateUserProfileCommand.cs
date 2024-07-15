using Application.Commands.ProductCommands;
using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ComboModels;
using Application.Services.Models.UserModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Application.Commands.UserCommands
{
    public class UpdateUserProfileCommand : UserProfileForUpdate, IRequest<UserMangeResponse>
    {
        [JsonIgnore]
        public AppUser? User { get; set; }
    }
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UserMangeResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UpdateUserProfileCommandHandler> _logger;
        private readonly IValidator<UserProfileForUpdate> _validatorUpdate;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;

        public UpdateUserProfileCommandHandler(
            UserManager<AppUser> userManager,
            IMapper mapper,
            ILogger<UpdateUserProfileCommandHandler> logger,
            IValidator<UserProfileForUpdate> validatorUpdate,
            ILocalizationMessage localization)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _validatorUpdate = validatorUpdate;
            _localization = localization;
        }
        public async Task<UserMangeResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "người dùng");
            }
            try
            {
                AppUser user = await _userManager.FindByIdAsync(request.User.Id);
                if (user == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "người dùng");
                }
                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                if (request.User.PhoneNumber != phoneNumber)
                {
                    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
                    if (!setPhoneResult.Succeeded)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.ValidationError, "số điện thoại");
                    }
                }

                user.UserName = request.User.Email;
                _mapper.Map(request, user);

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.UpdateError, "người dùng");
                }
                return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "người dùng");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
