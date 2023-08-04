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

    public class Order : BaseEntityWithId
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}