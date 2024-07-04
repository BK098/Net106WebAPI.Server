using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Queries;
using Application.Services.Models.CategoryModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.CategoryQueries
{
    public class GetAllCategoriresQuery
    {
        public class GetAllCategoryQuery : CategoryForView, IRequest<CategoryForView> { }
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, CategoryForView>
        {
            private readonly ICategoryQueryService _categoryService;

            public GetAllCategoryQueryHandler(ICategoryQueryService categoryService)
            {
                _categoryService = categoryService;
            }

            public async Task<CategoryForView> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _categoryService.GetAllCategoriesAsync();
            }
        }
    }
}
