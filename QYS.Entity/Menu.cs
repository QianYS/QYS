using System;
using System.ComponentModel.DataAnnotations;

namespace QYS.Entity
{
    public class Menu
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Code { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单路由
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sequ { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        [Required]
        public EnumMenuTypes Type { get; set; } 

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
