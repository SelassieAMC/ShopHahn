using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.WebApi
{
    public class SearchCountry : ISearchCountry
    {
        private readonly HttpClient _httpClient;

        public SearchCountry(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("country");
        }
        public async Task<bool> SearchAsync(string countryName)
        {
            using (HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Get, countryName + "?fullText=true"))
            {
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
