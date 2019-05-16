using ExchangeRate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExchangeRate.Services
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://api.exchangeratesapi.io/");

            _client = client;
        }

        public async Task<Response<InputResponseModel>> GetResponse<InputResponseModel>(string request)
        {
            var response = new Response<InputResponseModel>();

            try
            {
                var httpResponse = await _client.GetAsync(request);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                        response.Value = await httpResponse.Content.ReadAsAsync<InputResponseModel>();

                        response.Succeeded = true;
                }
                else
                {
                    response.Message = httpResponse.StatusCode.ToString();
                    response.Succeeded = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
            }

            return response;
        }

    }
}
