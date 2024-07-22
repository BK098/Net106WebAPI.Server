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
    public record GetAllUsersQuery(SearchBaseModel SearchModel) : IRequest<PaginatedList<UserForViewItems>>
    {
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserForViewItems>>
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
        public async Task<PaginatedList<UserForViewItems>> Handle(GetAllUsersQuery userDto, CancellationToken cancellationToken)
        {   
            try
            {
                var user = _userManager.Users.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(userDto.SearchModel.SearchTerm))
                {
                    user = user.Where(p =>
                        p.FirstName.ToLower().Contains(userDto.SearchModel.SearchTerm.ToLower()) ||
                        p.LastName.ToLower().Contains(userDto.SearchModel.SearchTerm.ToLower()));
                }
                var paginatedUser = await PaginatedList<AppUser>.CreateAsync(
                    user,
                    userDto.SearchModel.PageIndex,
                    userDto.SearchModel.PageSize,
                    cancellationToken);
                var items = new PaginatedList<UserForViewItems>(
                    _mapper.Map<List<UserForViewItems>>(paginatedUser.Items),
                    userDto.SearchModel.PageIndex,
                    userDto.SearchModel.PageSize,
                    paginatedUser.TotalCount);
                
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
