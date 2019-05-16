using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExchangeRate.Models;
using ExchangeRate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ApiClient _client;

        public StatsController(ApiClient client)
        {
            _client = client;
        }

        // GET api/stats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync([FromQuery]OutputRequestModel model)
        {
            var currency = model.Currency?.Split("->");
            if (currency != null && currency.Length == 2)
            {
                model.FirstCurrency = currency[0];
                model.SecondCurrency = currency[1];
            }

            model.DatesList = model.Dates?.Split(',').ToList();

            var valueList = new List<Double>();

            foreach (var date in model.DatesList)
            {
                var request = InputRequestBuilder(date, model.FirstCurrency);

                var response = await _client.GetResponse<InputResponseModel>(request);

                System.Reflection.PropertyInfo prop = typeof(Rates).GetProperty(model.SecondCurrency);

                var value = prop.GetValue(response.Value.rates);

                valueList.Add(Double.Parse(value.ToString()));
            }

            var result = new OutputResponseModel()
            {
                Min = valueList.Min().ToString(),
                Max = valueList.Max().ToString(),
                Avg = valueList.Average().ToString()
            };

            return Ok(result);
        }

        #region Helpers

        string InputRequestBuilder(string date, string currency)
        {
            return $"{date}?base={currency}";
        }
    }


    #endregion
}
