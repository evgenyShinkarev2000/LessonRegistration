using System.Text.Json.Serialization;

namespace LessonRegistration.DTO
{
    public class KeycloakKeys
    {
        [JsonPropertyName("keys")]
        public KeycloakCertificate[] Keys { get; set; } = default!;
    }
}
