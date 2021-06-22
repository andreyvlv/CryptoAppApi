using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAppApi
{
    public class Entry
    {
        public string priceUsd { get; set; }
        public object time { get; set; }
        public DateTime date { get; set; }
    }

    public class Entries
    {
        public List<Entry> data { get; set; }
        public long timestamp { get; set; }
    }
}
