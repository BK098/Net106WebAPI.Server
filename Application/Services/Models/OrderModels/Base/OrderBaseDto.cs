using Domain.Entities;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Services.Models.OrderModels.Base
{
    public class OrderBaseDto
    {
        public DateTimeOffset OrderDate { get; set; } //= DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
    }
}
