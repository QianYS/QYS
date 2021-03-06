using System.Collections.Generic;
using QYS.Entity;

namespace QYS.Service.Service.SystemSvr.Dto
{
    public class InitSystemDto
    {
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public EnumMenuTypes Type { get; set; }

        /// <summary>
        /// 页面操作
        /// </summary>
        public List<MenuAction> MenuOpers { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<InitSystemDto> Children { get; set; }
    }

    public class MenuAction
    {
        /// <summary>
        /// 操作名
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 访问路径
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string ActionMethod { get; set; }
    }
}
