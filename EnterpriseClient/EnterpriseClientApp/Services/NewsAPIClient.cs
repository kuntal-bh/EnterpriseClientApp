using EnterpriseClientApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnterpriseClientApp.Services
{
    public class NewsAPIClient : INewsAPIClient
    {
        private readonly string resourceIdforNewsAPI;

        private readonly string clientId;
        private readonly string clientSecret;
        private bool tokenset;
        private readonly string authority;
        private readonly HttpClient httpClient;

        public NewsAPIClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            resourceIdforNewsAPI = configuration["NewsAPI:ResourceId"];

            clientId = configuration["AzureAd:ClientId"];
            clientSecret = configuration["AzureAd:ClientSecret"];
            authority = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}";
            this.httpClient.BaseAddress = new Uri(configuration["NewsAPI:baseUrl"]);
        }

        public async Task SetTokenforNews()
        {
            if (!tokenset)
            {
                var authContext = new AuthenticationContext(authority);
                var credentials = new ClientCredential(clientId, clientSecret);
                var authResult = await authContext.AcquireTokenAsync(resourceIdforNewsAPI, credentials);
                var accessToken = authResult.AccessToken;
                this.httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "5e826194a034461f93a655e49f83973b");
                this.httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                tokenset = true;
            }
        }

        public async Task<Articles[]> GetValuesforNews()
        {
            await SetTokenforNews();
            var res = await this.httpClient.GetAsync("/NewsAPI/api/Values");
            // var res = await this.httpClient.GetAsync("/api/values");
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"{res.StatusCode}");
            }

            var content = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Articles[]>(content);
        }
    }
}
