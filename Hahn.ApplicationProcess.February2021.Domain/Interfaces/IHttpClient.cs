using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
