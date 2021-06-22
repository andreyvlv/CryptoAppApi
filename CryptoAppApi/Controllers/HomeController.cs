using CryptoAppApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAppApi.Controllers
{
    public class HomeController : Controller
    {       
        public HomeController()
        {         
        }

        public IActionResult Index()
        {   
            // create indexviewmodel
            var btc = GetCurrencyByType(CurrencyType.Bitcoin);
            var ethereum = GetCurrencyByType(CurrencyType.Ethereum);
            var monero = GetCurrencyByType(CurrencyType.Monero);

            var view = new IndexViewModel();
            view.Entries = new List<Currency>();
            view.Entries.Add(btc);
            view.Entries.Add(ethereum);
            view.Entries.Add(monero);

            return View(view);
        }       

        private Currency GetCurrencyByType(CurrencyType currencyType)
        {
            // history request
            var historyClient = new RestClient("https://api.coincap.io/v2/assets/" + currencyType.ToString().ToLower() + "/history?interval=d1");
            historyClient.Timeout = -1;
            var historyRequest = new RestRequest(Method.GET);
            IRestResponse historyResponse = historyClient.Execute(historyRequest);

            var historyEntries = JsonConvert.DeserializeObject<Entries>(historyResponse.Content);

            // current data request
            var currentDataClient = new RestClient("https://api.coincap.io/v2/assets/" + currencyType.ToString().ToLower());
            currentDataClient.Timeout = -1;
            var currentDataRequest = new RestRequest(Method.GET);
            IRestResponse currentDataResponse = currentDataClient.Execute(currentDataRequest);

            var currentDataEntry = JsonConvert.DeserializeObject<CurrentJson>(currentDataResponse.Content);

            // forming cuurency to indexviewmodel
            var history = new List<HistoryData>();

            foreach (var entry in historyEntries.data)
            {
                var price = $"{ double.Parse(entry.priceUsd, CultureInfo.InvariantCulture.NumberFormat):f2}";               
                var date = entry.date.ToString("dd.MM.yy", CultureInfo.InvariantCulture);
                history.Add(new HistoryData { Date = date, Price = price });
            }

            var currency = new Currency
            {
                Name = currencyType,                
                History = history,
                CurrentValue = $"{double.Parse(currentDataEntry.data.priceUsd, CultureInfo.InvariantCulture.NumberFormat):f2}",
                Change24H = $"{double.Parse(currentDataEntry.data.changePercent24Hr, CultureInfo.InvariantCulture.NumberFormat):f2}"
            };

            return currency;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
