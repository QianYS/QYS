using System;
using System.ComponentModel.DataAnnotations;

namespace QYS.Entity
{
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户名(登录名)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [StringLength(50)]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public EnumGenders Gender { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(128)]
        public string Address { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// 上一次登录时间
        /// </summary>
        [Required]
        public DateTime LastLoginTime { get; set; }
    }
}
