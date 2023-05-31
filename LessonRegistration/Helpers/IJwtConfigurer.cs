using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace LessonRegistration.Helpers
{
    public interface IJwtConfigurer
    {
        Task<TokenValidationParameters> GetTokenValidationParameters();
    }
}
