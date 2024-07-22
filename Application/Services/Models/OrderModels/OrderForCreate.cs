using Application.Services.Models.OrderModels.Base;
using FluentValidation;

namespace Application.Services.Models.OrderModels
{
    public class OrderForCreate : OrderBaseDto
    {
        public IList<OrderItemForCreate> OrderItems { get; set; } = new List<OrderItemForCreate>();
    }
    public class OrderForCreateValidator : AbstractValidator<OrderForCreate>
    {
        public OrderForCreateValidator()
        {
            RuleFor(x => x.OrderDate)
                .NotNull().WithMessage("Ngày tạo không được để trống");
            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemForCreateValidator());
        }
    }
    public class OrderItemForCreate
    {
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ComboId { get; set; }
    }
    public class OrderItemForCreateValidator : AbstractValidator<OrderItemForCreate>
    {
        public OrderItemForCreateValidator()
        {
            RuleFor(order => order)
                .Must(order => order.ProductId != null || order.ComboId != null)
                .WithName("ProductId Or Combo Id")
                .WithMessage(order => $"{order.ProductId} hoặc {order.ComboId}");  
        }
    }
}
