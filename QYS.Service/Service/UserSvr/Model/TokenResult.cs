namespace QYS.Service.Service.UserSvr.Model
{
    /// <summary>
    /// TokenResult 定义
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Success => string.IsNullOrEmpty(Errors);

        /// <summary>
        /// 错误
        /// </summary>
        public string Errors { get; set; }

        /// <summary>
        /// accessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// token类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// refreshToken
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
