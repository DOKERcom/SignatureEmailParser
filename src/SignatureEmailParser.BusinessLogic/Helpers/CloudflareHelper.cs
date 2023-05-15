using CloudFlare.Client;
using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.Models.CloudflareModels;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    /// <summary>
    /// https://gist.github.com/jjxtra/3b240b31a1ed3ad783a7dcdb6df12c36
    /// </summary>
    public class CloudflareHelper
    {
        public static async Task BlockIp(string ipAddress, string zoneId)
        {
            var xAuthEmail = CloudflareConstant.EMAIL;
            var xAuthKey = CloudflareConstant.APIKEY;

            var path = $"/client/v4/zones/{zoneId}/firewall/access_rules/rules";
            var body = new
            {
                mode = "js_challenge",
                configuration = new
                {
                    target = "ip",
                    value = ipAddress
                },
                notes = $"rate-limit-abuse-{DateTime.UtcNow.ToString("s")}"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://" + CloudflareConstant.CLOUDFLAREDOMAIN);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Auth-Email", xAuthEmail);
                client.DefaultRequestHeaders.Add("X-Auth-Key", xAuthKey);

                await client.PostAsJsonAsync(path, body);
            }
        }

        public static async Task RemoveIpBlocks(string zoneId)
        {
            var xAuthEmail = CloudflareConstant.EMAIL;
            var xAuthKey = CloudflareConstant.APIKEY;

            var path = $"/client/v4/zones/{zoneId}/firewall/access_rules/rules?per_page=100";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.cloudflare.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Auth-Email", xAuthEmail);
                client.DefaultRequestHeaders.Add("X-Auth-Key", xAuthKey);

                var response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var firewallRules = await response.Content.ReadAsAsync<Zones>();
                    foreach (var firewallRule in firewallRules.result)
                    {
                        var notes = firewallRule.notes.ToString();
                        if (notes.StartsWith("rate-limit-abuse-"))
                        {
                            var dateString = notes.Replace("rate-limit-abuse-", string.Empty);
                            var ruleCreated = DateTime.Parse(dateString);
                            var ruleDescription = $"IP {firewallRule.configuration.value} added on {ruleCreated.ToString("s")} -";

                            // Delete rules that were created more than a day ago
                            if (ruleCreated < DateTime.UtcNow.AddDays(-1))
                            {
                                path = $"/client/v4/zones/{zoneId}/firewall/access_rules/rules/{firewallRule.id}";
                                await client.DeleteAsync(path);
                            }
                            else
                            {
                                
                            }
                        }
                    }
                }
            }
        }
    }
}
