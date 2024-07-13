﻿
using Application.Services.Models.OrderModels.Base;
using Domain.Entities;

namespace Application.Services.Models.OrderModels
{
    public class OrderForView
    {
        /*public double TotalAmount { get; set; }*/
        public IList<OrderForViewItems> Orders { get; set; } = new List<OrderForViewItems>();
    }
    public class OrderForViewItems : OrderBaseDto
    {
        public Guid Id { get;set; }
        public double TotalAmount { get; set; }
        public Status Status { get; set; }
        public IList<OrderItemForView> OrderItems { get; set; } = new List<OrderItemForView>();
    }
    public class OrderItemForView
    {
        //orderItem
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        //combo
        public Guid? ComboId { get; set; }
        public string? ComboName { get; set; }
        //product
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        

    }

}
