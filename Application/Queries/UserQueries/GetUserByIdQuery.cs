using Application.Enums;
using Application.Helpers;
using Application.Services.Models.Base;
using Application.Services.Models.UserModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OneOf;
using System.Text.Json.Serialization;

namespace Application.Queries.UserQueries
{
    public record GetUserByIdQuery(Guid id) : IRequest<OneOf<UserForView, UserMangeResponse>> { }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OneOf<UserForView, UserMangeResponse>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AppUser> _logger;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AppUser> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<OneOf<UserForView, UserMangeResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AppUser user = await _userManager.FindByIdAsync(request.id.ToString());
                if (user == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "User");
                }
                UserForView item = _mapper.Map<UserForView>(user);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
