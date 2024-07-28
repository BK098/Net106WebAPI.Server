using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.Queries.CategoryQueries
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<OneOf<CategoryForView, UserMangeResponse>> { }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, OneOf<CategoryForView, UserMangeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;
        public GetCategoryByIdQueryHandler(IMapper mapper,
            ICategoryRepository repository,
            ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _categoryRepository = repository;
            _logger = logger;
        }
        public async Task<OneOf<CategoryForView, UserMangeResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(request.id);
                if (category == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "Loại hàng");

                }
                CategoryForView item = _mapper.Map<CategoryForView>(category);
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
