using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public T Value { get; set; }

        public string Message { get; set; }
    }
}
