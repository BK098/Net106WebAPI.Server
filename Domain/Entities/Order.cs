﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public Status OrderStatus { get; set; } 

        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
       
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
    public enum Status
    {
        Processing,
        Success,
        Failed
    }
}
