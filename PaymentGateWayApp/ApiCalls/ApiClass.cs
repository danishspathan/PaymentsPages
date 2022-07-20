using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PaymentGateWayApp.ApiCalls
{
    public static class ApiClass
    {
        // Get Refresh token for zoho operations
        public static async Task<string> RefreshToken(string refreshToken)
        {
            HttpClient httpclient = new HttpClient();

            string clientID = ConfigurationManager.AppSettings["clientID"];
            string clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            var url = "https://accounts.zoho.com/oauth/v2/token";
            var parameters = new Dictionary<string, string> { { "refresh_token", refreshToken }, { "client_id", clientID }, { "client_secret", clientSecret }, { "grant_type", "refresh_token" } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            var response = await httpclient.PostAsync(url, encodedContent).ConfigureAwait(continueOnCapturedContext: false);
            if (response.StatusCode == HttpStatusCode.OK)
            {

                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic responseJSon = JsonConvert.DeserializeObject(responseContent);

                string accessToken = responseJSon["access_token"];



                return accessToken;

            }
            httpclient.Dispose();
            return string.Empty;

        }
        // Get Url of hosted pages after posting the required customer Details and 
        public static async Task<List<string>> Geturl(string jsonData, string ZohoAccessToken)
        {
           
            List<string> result = new List<string>();
            HttpClient httpclient = new HttpClient();

            string zohoOrganizationID = ConfigurationManager.AppSettings["zohoOrganizationID"];
            httpclient.DefaultRequestHeaders.Add("X-com-zoho-subscriptions-organizationid", zohoOrganizationID);

            httpclient.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

            var url = "https://subscriptions.zoho.com/api/v1/hostedpages/newsubscription";
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpclient.PostAsync(url, data).ConfigureAwait(continueOnCapturedContext: false);
            if (response.StatusCode == HttpStatusCode.Created)
            {

                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic responseJSon = JsonConvert.DeserializeObject(responseContent);
                string strUrl = responseJSon["hostedpage"]["url"];
                string hostedpagesId = responseJSon["hostedpage"]["hostedpage_id"];
                result.Add(strUrl);
                result.Add(hostedpagesId);  
                httpclient.Dispose();
                

            }

            httpclient.Dispose();
            return result;
        }
        public static async Task<string> GetPlanPrice(string zohoAccessToken, string plan)
        {
            HttpClient httpclient = new HttpClient();
            string zohoOrganizationID = ConfigurationManager.AppSettings["zohoOrganizationID"];
            httpclient.DefaultRequestHeaders.Add("X-com-zoho-subscriptions-organizationid", zohoOrganizationID);

            httpclient.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + zohoAccessToken);
            var url = "https://subscriptions.zoho.com/api/v1/plans/" + plan;



            var response = await httpclient.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic responseJSon = JsonConvert.DeserializeObject(responseContent);
                string strPrice = responseJSon["plan"]["recurring_price"];
                return strPrice;

            }
            httpclient.Dispose();
            return string.Empty;
        }
        public static async Task<string> GetExchangeRate(string currentCurrencyCode)
        {
            HttpClient httpclient = new HttpClient();
            string exchangeRateToken = ConfigurationManager.AppSettings["exchangeRateToken"];
            var builder = new UriBuilder("https://api.fastforex.io/fetch-one");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["api_key"] = exchangeRateToken;
            query["to"] = currentCurrencyCode;
            query["from"] = "USD";
            builder.Query = query.ToString();
            string url = builder.ToString();
            var response = await httpclient.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic responseJSon = JsonConvert.DeserializeObject(responseContent);
                string strexchangeRate = responseJSon["result"][currentCurrencyCode];
                httpclient.Dispose();
                return strexchangeRate;
            }
            httpclient.Dispose();
            return string.Empty;
        }
        // Get current Currency code through api calls
        public static async Task<string> GetCurrentCountryCode()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient httpclient = new HttpClient();
            var response = await httpclient.GetAsync("https://ipapi.co/json/").ConfigureAwait(continueOnCapturedContext: false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic responseJSon = JsonConvert.DeserializeObject(responseContent);
                string strexchangeRate = responseJSon["country_name"];
                httpclient.Dispose();
                return strexchangeRate;

            }
            httpclient.Dispose();
            return string.Empty;
        }
        public static async Task<dynamic> GetCUstomerInfo(string zohoAccessToken, string hostedPagesId)
        {
            HttpClient httpclient = new HttpClient();
            string zohoOrganizationID = ConfigurationManager.AppSettings["zohoOrganizationID"];
            httpclient.DefaultRequestHeaders.Add("X-com-zoho-subscriptions-organizationid", zohoOrganizationID);

            httpclient.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + zohoAccessToken);
            var url = "https://subscriptions.zoho.com/api/v1/hostedpages/" + hostedPagesId;



            var response = await httpclient.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic responseJSon = JsonConvert.DeserializeObject(responseContent);

                return responseJSon;

            }
            httpclient.Dispose();
            return string.Empty;
        }
    }
}

