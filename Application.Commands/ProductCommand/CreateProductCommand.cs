using Application.Services.Contracts.Repositories;
using Application.Services.Models.Product;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.ProductCommand
{
    public class CreateProductCommand: IRequest<Product>
    {
        public ProductForCreate ProductForCreate { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Ánh xạ từ ProductForCreate sang Product
            var productEntity = _mapper.Map<Product>(request.ProductForCreate);

            // Tạo sản phẩm trong cơ sở dữ liệu
            await _productRepository.CreateAsync(productEntity);
            await _productRepository.SaveChangesAsync();

            return productEntity;
        }
    }

}
