using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAppApi.Models
{
    public class IndexViewModel
    {
        public List<Currency> Entries { get; set; }
        public Dictionary<CurrencyType, String> TypeToCode { get; set; }

        public IndexViewModel()
        {
            TypeToCode = new Dictionary<CurrencyType, string>()
            {
                {CurrencyType.Bitcoin, "BTC" },
                {CurrencyType.Ethereum, "ETH" },
                {CurrencyType.Monero, "MON" }
            };
        }
    }
}
