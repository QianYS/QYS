using System;

namespace QYS.Service.Manager.OperManager.Dto
{
    public class OperDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 访问路径
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sequ { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuCode { get; set; }

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
