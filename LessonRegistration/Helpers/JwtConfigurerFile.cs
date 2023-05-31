using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LessonRegistration.Helpers
{
    public class JwtConfigurerFile : IJwtConfigurer
    {
        private readonly string certPath;

        public JwtConfigurerFile(string certPath)
        {
            this.certPath = certPath;
        }
        public async Task<TokenValidationParameters> GetTokenValidationParameters()
        {
            //var certificate = new X509Certificate2(certPath); //doesn't work at linux

            //var rsaKey = RSA.Create(); //just example
            //rsaKey.ImportSubjectPublicKeyInfo(Convert.FromBase64String(key), out var _);
            //var securityKey = new RsaSecurityKey(rsaKey);

            var certificate = new X509Certificate2(Convert.FromBase64String(await File.ReadAllTextAsync(certPath)));
            var securityKey = new X509SecurityKey(certificate);


            return new TokenValidationParameters()
            {
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                //ValidIssuer = realmUri, // created by
                ValidAudience = "lesson-registration-client"
            };
        }
    }
}
