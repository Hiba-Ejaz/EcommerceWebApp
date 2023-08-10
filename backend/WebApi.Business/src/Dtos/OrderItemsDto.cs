using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Business.src.Dtos
{
    public class OrderItemsDto
    {
        public OrderReadDto Order { get; set; }
        public ProductReadDto Product { get; set; }
        public int Quantity { get; set; }
        public Decimal SubTotal { get; set; }
    }
    
}