using System.Collections.Generic;

namespace QYS_Project.Responses.Model
{
    /// <summary>
    /// 登录 注册 失败时返回错误信息
    /// </summary>
    public class FailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
