using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAppApi.Models
{
    public class Currency
    {
        public CurrencyType Name { get; set; }        
        public string CurrentValue { get; set; }
        public string Change24H { get; set; }
        public List<HistoryData> History { get; set; }
    }
}
