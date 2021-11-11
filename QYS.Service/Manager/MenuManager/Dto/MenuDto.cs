using System;
using QYS.Entity;

namespace QYS.Service.Manager.MenuManager.Dto
{
    public class MenuDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单路由
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sequ { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public EnumMenuTypes Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
