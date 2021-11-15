using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QYS_Project.Requests
{
    public class PageInfo
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面显示条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
