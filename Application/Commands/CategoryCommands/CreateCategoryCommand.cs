using Application.Services.Contracts.Services.Commands;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using Application.Services.Models.ProductModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.CategoryCommands
{
    public class CreateCategoryCommand : CategoryForCreate, IRequest<UserMangeResponse>{ }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, UserMangeResponse>
    {
        private readonly ICategoryCommandService _categoryService;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        public CreateCategoryCommandHandler(ICategoryCommandService categoryService, ILogger<CreateCategoryCommandHandler> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<UserMangeResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.CreateCategoryAsync(request);
            return result;
        }
    }
}
