using System;

namespace QYS.Service.Common
{
    public class JwtSettings
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public TimeSpan ExpiresIn { get; set; }
    }
}
