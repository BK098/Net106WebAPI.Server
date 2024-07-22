using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Services.Models.OrderModels.Base
{
    public class OrderBaseDto
    {
        public DateTimeOffset OrderDate { get; set; } //= DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
