using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class OutputRequestModel
    {
        public string Dates { get; set; }
        public string Currency { get; set; }
        public List<string> DatesList { get; set; } = new List<string>();
        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
    }
}
