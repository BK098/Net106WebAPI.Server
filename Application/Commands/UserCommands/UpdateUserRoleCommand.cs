using Application.Enums;
using Application.Helpers;
using Application.Services.Models.Base;
using Application.Services.Models.UserModels;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Application.Commands.UserCommands
{
    public class UpdateUserRoleCommand : UserRoleForUpdate, IRequest<UserMangeResponse>
    {
        [JsonIgnore]
        public AppUser? User { get; set; }
    }
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, UserMangeResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateUserRoleCommandHandler(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserMangeResponse> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await EnsureRolesExistAsync();
                var user = await _userManager.FindByIdAsync(request.User.Id);
                if (user == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "Người dùng");
                }

                var roles = await _userManager.GetRolesAsync(user);
                if (roles == null || !roles.Any())
                {
                    // Người dùng chưa có vai trò nào, chỉ thêm vai trò mới
                    var addResult = await _userManager.AddToRoleAsync(user, request.Role);
                    if (!addResult.Succeeded)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.OperationFailed, "Thêm vai trò mới");
                    }
                }
                else
                {
                    // Người dùng đã có vai trò, cần xóa vai trò cũ trước
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, roles);
                    if (!removeResult.Succeeded)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.OperationFailed, "Xóa vai trò cũ");
                    }

                    var addResult = await _userManager.AddToRoleAsync(user, request.Role);
                    if (!addResult.Succeeded)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.OperationFailed, "Thêm vai trò mới");
                    }
                }
                if (!await _roleManager.RoleExistsAsync(request.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(request.Role));
                }

                return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "vai trò");
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.Exception, ex.Message.ToString());
                throw;
            }
        }
        private async Task EnsureRolesExistAsync()
        {
            var roles = new[] { "Customer", "Admin" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };

                    await _roleManager.CreateAsync(identityRole);
                }
            }
        }
    }
}
