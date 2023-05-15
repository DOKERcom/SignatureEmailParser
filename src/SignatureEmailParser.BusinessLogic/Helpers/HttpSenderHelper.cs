using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class HttpSenderHelper : IHttpSenderHelper
    {
        private readonly HttpClient _client;

        public HttpSenderHelper(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> SendPostRequest(string url, string serializedReport)
        {
            try
            {
                StringContent content = new StringContent(serializedReport, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
