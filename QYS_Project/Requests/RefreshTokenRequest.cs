using System.Text.Json.Serialization;

namespace QYS_Project.Requests
{
    /// <summary>
    /// RefreshToken 请求参数
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// access_token
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// refresh_token
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
