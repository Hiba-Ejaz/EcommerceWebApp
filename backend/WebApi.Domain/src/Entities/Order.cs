using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.src.Entities
{
   public enum OrderStatus
    {
        Processing,
        Shipped,
        Delivered,
       
    }

    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
    }
}