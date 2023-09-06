using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.WebApi.src.Middleware
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set;}
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}