using Application.Services.Models.Base;
using Application.Services.Models.UserModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Queries.UserQueries
{
    public record GetAllUsersQuery(SearchBaseModel SearchModel) : IRequest<PaginatedList<UserForView>>
    {
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserForView>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AppUser> _logger;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(RoleManager<IdentityRole> roleManager,
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
        public async Task<PaginatedList<UserForView>> Handle(GetAllUsersQuery userDto, CancellationToken cancellationToken)
        {   
            try
            {
                var usersQuery = _userManager.Users.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(userDto.SearchModel.SearchTerm))
                {
                    usersQuery = usersQuery.Where(p =>
                        p.FirstName.ToLower().Contains(userDto.SearchModel.SearchTerm.ToLower()) ||
                        p.LastName.ToLower().Contains(userDto.SearchModel.SearchTerm.ToLower()));
                }

                var paginatedUsers = await PaginatedList<AppUser>.CreateAsync(
                    usersQuery,
                    userDto.SearchModel.PageIndex,
                    userDto.SearchModel.PageSize,
                    cancellationToken);

                var userForViewList = new List<UserForView>();

                foreach (var user in paginatedUsers.Items)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userForView = _mapper.Map<UserForView>(user);
                    userForView.Role = roles.FirstOrDefault();
                    userForViewList.Add(userForView);
                }

                var items = new PaginatedList<UserForView>(
                    userForViewList,
                    userDto.SearchModel.PageIndex,
                    userDto.SearchModel.PageSize,
                    paginatedUsers.TotalCount);

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
