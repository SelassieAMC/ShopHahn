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
        private readonly IHttpClient _httpClient;
        private readonly string _baseUri;

        public SearchCountry(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUri = "https://restcountries.eu/rest/v2/name/";
        }
        public async Task<(string, bool)> SearchAsync(string countryName)
        {
            using (HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Get, _baseUri + countryName + "?fullText=true"))
            {
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return (string.Empty, true);
                }
                else
                {
                    return (response.ReasonPhrase,false);
                }
            }
        }
    }
}
