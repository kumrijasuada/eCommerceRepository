using System.Collections.Generic;
using PayPal.Api;

namespace ShisheVere.Models
{
    //marrim konfigurimet nga web.config

    public class PaypalCofiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

         static PaypalCofiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string,string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
        //kjo do ktheje apocontext object
        public static APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }

    }
}