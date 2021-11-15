using System.ComponentModel;

namespace QYS.Service.Service.TaskSvr.Dto
{
    public enum EnumJobActions
    {
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Suspend = 1,

        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stop = 2,

        /// <summary>
        /// 开启
        /// </summary>
        [Description("开启")]
        Start = 3,

        /// <summary>
        /// 立即执行
        /// </summary>
        [Description("立即执行")]
        Execute = 4
    }
}
