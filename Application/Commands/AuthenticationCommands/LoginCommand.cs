using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.AuthenticationModels;
using Application.Services.Models.Base;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Commands.AuthenticationCommands
{
    public class LoginCommand : LoginModel, IRequest<UserMangeResponse> { }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserMangeResponse>
    {
        private readonly IValidator<LoginModel> _validatorLogin;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly ILocalizationMessage _localization;
        private readonly SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private IConfiguration _configuration;

        public LoginCommandHandler(IValidator<LoginModel> validatorLogin,
            ILogger<LoginCommandHandler> logger,
            ILocalizationMessage localization,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _validatorLogin = validatorLogin;
            _logger = logger;
            _localization = localization;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<UserMangeResponse> Handle(LoginCommand model, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorLogin.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "User");
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "Email");
                }
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.WrongPassword, "Password");
                }

                var claims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    //new Claim("Email", model.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)   
                };

                var roles = await _signInManager.UserManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var roleClaim = string.Join(",", roles).Replace("\n", "");
                    claims.Add(new Claim(ClaimTypes.Role, roleClaim));
                }
                var token = CreateToken(claims);
                var refreshToken = GenerateRefreshToken();

                return ResponseHelper.SuccessResponse(SuccessCode.LoginSuccess, "" ,new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
            var add30Date = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                 issuer: _configuration["AuthSettings:Issuer"],
                 audience: _configuration["AuthSettings:Audience"],
                 claims: claims,
                 expires: add30Date.ToUniversalTime(),
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                 );
            return token;
        }
    }
}
