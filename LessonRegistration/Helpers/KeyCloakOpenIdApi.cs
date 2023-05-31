using System.Collections.Generic;
using System.Net.Cache;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LessonRegistration.Helpers
{
    public class KeyCloakOpenIdApi : IOpenIdApi
    {
        public string RealmUri { get; }
        private readonly string clientSecret;
        private readonly string clientId;
        public KeyCloakOpenIdApi(string realmUri, string clientSecret, string clientId)
        {
            RealmUri = realmUri;
            this.clientSecret = clientSecret;
            this.clientId = clientId;
        }

        public string GetToken(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public string GetToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public string GetLoginPath()
        {
            var bytes = new Infostracture.SecurityAlphaNumericGenerator().GetChars(16);

            return $"{RealmUri}/protocol/openid-connect/auth?response_type=code&client_id={clientId}&scope=openid%20profile&state={string.Join("", bytes)}";
        }

        public async Task<HttpContent> GetTokenByCode(string code, string redirectUri)
        {
            var data = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri)
            };
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(
                $"{RealmUri}/protocol/openid-connect/token",
                new FormUrlEncodedContent(data));

            return response.Content;
        }

        public async Task<HttpContent> GetTokenByRefresh(string refreshToken)
        {
            var data = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            };

            var response = new HttpClient()
                .PostAsync($"{RealmUri}/protocol/openid-connect/token", new FormUrlEncodedContent(data));

            return (await response).Content;
        }
    }
}
