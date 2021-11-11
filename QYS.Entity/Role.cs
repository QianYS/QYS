using System;
using System.ComponentModel.DataAnnotations;

namespace QYS.Entity
{
    public class Role
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuper { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        public string Remark { get; set; }

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

    }
}
