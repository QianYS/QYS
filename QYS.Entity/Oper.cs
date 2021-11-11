using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QYS.Entity
{
    public class Oper
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Code { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 访问路径
        /// </summary>
        [Required]
        public string Action { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [Required]
        public string Method { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sequ { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [Required]
        [ForeignKey(nameof(MenuCode))]
        public Menu Menu { get; set; }

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
