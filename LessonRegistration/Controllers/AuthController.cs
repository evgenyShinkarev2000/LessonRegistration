using LessonRegistration.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Threading.Tasks;

namespace LessonRegistration.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOpenIdApi openIdApi;

        public AuthController(IOpenIdApi openIdApi)
        {
            this.openIdApi = openIdApi;
        }

        [HttpGet("provider")]
        public IActionResult GetAuthProvider([FromQuery(Name = "redirect_uri")] string redirectUri)
        {
            dynamic t = new ExpandoObject();
            t.authProviderPath = openIdApi.GetLoginPath() + $"&redirect_uri={redirectUri}";
            return Ok(t);
        }

        [HttpGet("token-bycode")]
        public async Task<IActionResult> GetTokenByCode(
            [FromQuery(Name = "code")] string code, 
            [FromQuery(Name = "redirect_uri")] string redirectUri)
        {
            var httpContent = await openIdApi.GetTokenByCode(code, redirectUri);
            var content = httpContent.ReadAsStringAsync();

            return Ok(content.Result);
        }

        [HttpGet("update-token")]
        public async Task<IActionResult> GetTokenByRefresh([FromQuery(Name = "refresh_token")] string refreshToken)
        {
            var httpContent = await openIdApi.GetTokenByRefresh(refreshToken);
            var content = httpContent.ReadAsStringAsync();

            return Ok(content.Result);
        }
    }
}
