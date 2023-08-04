using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.src.Entities
{
    public class Product: BaseEntityWithId
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string[] Images { get; set; }
    }
}