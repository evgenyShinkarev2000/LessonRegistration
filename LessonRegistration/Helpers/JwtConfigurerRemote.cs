using LessonRegistration.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LessonRegistration.Helpers
{
    public class JWTConfigurerRemote : IJwtConfigurer
    {
        private readonly string uri;

        public JWTConfigurerRemote(string baseUri)
        {
            this.uri = baseUri + "/protocol/openid-connect/certs";
        }

        public async Task<TokenValidationParameters> GetTokenValidationParameters()
        {
            HttpResponseMessage response = default!;
            try
            {
                response = await new HttpClient().GetAsync(uri);
            }
            catch(Exception ex)
            {
                
                throw new Exception($"Exception when fetching keycloak key by uri {uri}", ex);
            }
            
            var keys = await response.Content.ReadFromJsonAsync<KeycloakKeys>();
            var keyCloakCerts = keys?.Keys;
            if (keyCloakCerts == null)
            {
                throw new Exception("Couldn't get keycloak certificate");
            }
            var keycloakSignCerts = keyCloakCerts.Where(c => c.Use == "sig").FirstOrDefault();
            if (keycloakSignCerts == null)
            {
                throw new Exception("keycloak certificate hasn't sig certificate");
            }
            var signCert = keycloakSignCerts.Certificates.FirstOrDefault();
            if (signCert == null)
            {
                throw new Exception("keycloak sig certificate is empty");
            }
            X509Certificate2 certificate = default!;
            try
            {
                certificate = new X509Certificate2(Convert.FromBase64String(signCert));
            }
            catch(Exception ex)
            {
                throw new Exception("Couldn't convert keycloak certificate to X509", ex);
            }

            var securityKey = new X509SecurityKey(certificate);

            return new TokenValidationParameters()
            {
                IssuerSigningKey = securityKey,
            };
        }
    }
}
