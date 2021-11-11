using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QYS.Entity
{
    public class RefreshToken
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// JwtId
        /// </summary>
        [Required]
        [StringLength(128)]
        public string JwtId { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Token { get; set; }

        /// <summary>
        /// 是否使用，一个RefreshToken只能使用一次
        /// </summary>
        [Required]
        public bool Used { get; set; }

        /// <summary>
        /// 是否失效。修改用户重要信息时可将此字段更新为true，使用户重新登录
        /// </summary>
        [Required]
        public bool Invalidated { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Required]
        public DateTime ExpiryTime { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// User
        /// </summary>
        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
