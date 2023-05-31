using System.Text.Json.Serialization;

namespace LessonRegistration.DTO
{
    public class KeycloakCertificate
    {
        [JsonPropertyName("x5t")]
        public string Thumbprint { get; set; } = default!;
        [JsonPropertyName("x5t#S256")]
        public string Thumbprint256 { get; set; } = default!;
        [JsonPropertyName("kid")]
        public string KeyIdentifer { get; set; } = default!;
        [JsonPropertyName("kty")]
        public string KeyType { get; set; } = default!;
        [JsonPropertyName("use")]
        public string Use { get; set; } = default!;
        [JsonPropertyName("n")]
        public string Chain { get; set; } = default!;
        [JsonPropertyName("x5c")]
        public string[] Certificates { get; set; } = default!;
    }
}
