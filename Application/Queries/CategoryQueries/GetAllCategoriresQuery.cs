using Application.Services.Contracts.Repositories;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.CategoryQueries
{
    public class GetAllCategoriresQuery : CategoryForView, IRequest<CategoryForView> { }
    public class GetAllCategoriresQueryHandler : IRequestHandler<GetAllCategoriresQuery, CategoryForView>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCategoriresQueryHandler> _logger;

        public GetAllCategoriresQueryHandler(ICategoryRepository categoryRepository, 
            IMapper mapper, 
            ILogger<GetAllCategoriresQueryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CategoryForView> Handle(GetAllCategoriresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Category> categories = await _categoryRepository.GetAllCategorys();
                IList<CategoryForViewItems> items = _mapper.Map<IEnumerable<CategoryForViewItems>>(categories).ToList();
                CategoryForView result = new CategoryForView();
                result.Categories = items;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}