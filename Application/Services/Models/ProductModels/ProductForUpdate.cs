﻿using Application.Services.Models.ProductModels.Base;
using FluentValidation;
using System.Text.Json.Serialization;

namespace Application.Services.Models.ProductModels
{
    public class ProductForUpdate : ProductBaseDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class ProductForUpdateValidator : AbstractValidator<ProductForUpdate>
    {
        public ProductForUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage("Không được để trống");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1).WithMessage("Không  được bé hơn 1");
            //.MustAsync(productRepo.IsUniqueProductName).WithMessage(x => $"Tên sản phẩm '{x.Name}' đã tồn tại");
            RuleFor(x => x.Discount)
                .LessThanOrEqualTo(100).WithMessage($"Bắt buộc phải nhỏ hơn hoặc bằng 100")
                .GreaterThanOrEqualTo(0).WithMessage($"Bắt buộc phải lớn hơn hoặc bằng 0");
            RuleFor(x => x.StockQuantity)
                .GreaterThan(0).WithMessage($"Phải lớn hơn 0");
        }
    }
}
