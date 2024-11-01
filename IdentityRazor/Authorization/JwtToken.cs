using Newtonsoft.Json;

namespace IdentityRazor.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonProperty("expire_at")]
        public DateTime ExpireAt { get; set; }
    }
}
