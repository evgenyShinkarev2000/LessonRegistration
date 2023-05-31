using System.Net.Http;
using System.Threading.Tasks;

namespace LessonRegistration.Helpers
{
    public interface IOpenIdApi
    {
        public string GetToken(string login, string password);
        public string GetToken(string refreshToken);
        public string GetLoginPath();
        public Task<HttpContent> GetTokenByCode(string code, string redirect_uri);
        public Task<HttpContent> GetTokenByRefresh(string refreshToken);
        public string RealmUri { get; }
    }
}
