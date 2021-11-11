using System;

namespace QYS.Service.Manager.RefreshTokenManager.Dto
{
    public class RefreshTokenDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// JwtId
        /// </summary>
        public string JwtId { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 是否使用，一个RefreshToken只能使用一次
        /// </summary>
        public bool Used { get; set; }

        /// <summary>
        /// 是否失效。修改用户重要信息时可将此字段更新为true，使用户重新登录
        /// </summary>
        public bool Invalidated { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryTime { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
    }
}
