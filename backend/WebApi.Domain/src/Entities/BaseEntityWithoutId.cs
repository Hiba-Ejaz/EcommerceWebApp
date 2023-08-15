using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.src.Entities
{
    public class BaseEntityWithoutId
    {
                 public DateTime CreatedAt {get; set;}
                public DateTime UpdatedAt {get; set;}
    }
}