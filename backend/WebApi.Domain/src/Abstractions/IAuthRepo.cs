using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IAuthRepo
    {
        public string CreateToken(User user);
    }
}