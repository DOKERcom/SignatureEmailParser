using Newtonsoft.Json;
using SignatureEmailParser.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.Helpers
{
    public static class AzureHelper
    {
        public static async Task<List<RegionModel>> GetAllNewRegionsByLatestIdAsync(long latestId)
        {
            var url = $"{Constants.AZURE_FUNCTION_DOMAIN}{Constants.AZURE_GET_ALL_NEW_REGIONS_BY_LATEST_ID_URL}";

            var queryParam = $"latestId={latestId}";

            HttpResponseMessage response = await SendGetRequestToAzure(queryParam, url, Constants.TMP_LICENSE);

            if (response is null || !response.IsSuccessStatusCode)
            {
                //throw new Exception("Request failed.");  done after publishing on the server
                return null;
            }

            string content = await response.Content.ReadAsStringAsync();

            var regions = JsonConvert.DeserializeObject<List<RegionModel>>(content);

            return regions;
        }

        public static async Task<List<CityModel>> GetAllNewCitiesByLatestIdAsync(long latestId)
        {
            var url = $"{Constants.AZURE_FUNCTION_DOMAIN}{Constants.AZURE_GET_ALL_NEW_CITIES_BY_LATEST_ID_URL}";

            var queryParam = $"latestId={latestId}";

            HttpResponseMessage response = await SendGetRequestToAzure(queryParam, url, Constants.TMP_LICENSE);

            if (response is null || !response.IsSuccessStatusCode)
            {
                return null;
            }

            string content = await response.Content.ReadAsStringAsync();

            var cities = JsonConvert.DeserializeObject<List<CityModel>>(content);

            return cities;
        }

        public static async Task<HttpResponseMessage> SendGetRequestToAzure(string paramsQuery, string url, string licenseId = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder(url);
                builder.Query = paramsQuery;
                try
                {
                    httpClient.DefaultRequestHeaders.Add(Constants.AZURE_X_FUNCTION_KEY, Constants.AZURE_X_FUNCTION_KEY_VALUE);
                    httpClient.DefaultRequestHeaders.Add(Constants.LICENSE_HEADER_NAME, licenseId);

                    HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
                    return response;
                }
                catch (Exception ex)
                {
                    //await _zenDeskHelper.CreateNewTicket("SendGetRequestToAzure: failure!", "The SendGetRequestToAzure failed with " + ex.InnerException, ZendeskPriorityConstants.HIGH);
                    return null;
                }
            }
        }
    }
}
