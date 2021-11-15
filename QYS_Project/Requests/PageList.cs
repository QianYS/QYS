using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QYS_Project.Requests
{
    public class PageList<T>
    {
        /// <summary>
        /// 列表
        /// </summary>
        public IEnumerable<T> Table { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public static PageList<T> Create(IEnumerable<T> data, int totalCount)
        {
            return new PageList<T>()
            {
                Table = data,
                TotalCount = totalCount,
            };
        }
    }
}
