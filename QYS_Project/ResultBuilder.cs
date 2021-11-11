using QYS_Project.Responses;

namespace QYS_Project
{
    /// <summary>
    /// 响应结果生成工具
    /// </summary>
    public static class ResultBuilder
    {
        /// <summary>
        /// 失败结果
        /// </summary>
        /// <typeparam name="T">方法返回类型</typeparam>
        /// <param name="failReason">失败原因</param>
        /// <param name="data">业务参数</param>
        /// <param name="code">错误码</param>
        /// <returns></returns>
        public static Response<T> FailResult<T>(T data, string failReason = "请求失败", int code = 99)
        {
            return new Response<T>
            {
                Code = code,
                Message = failReason,
                Data = data
            };
        }

        /// <summary>
        /// 简单的失败消息
        /// </summary>
        /// <param name="failReason"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResponseSimple SimpleResult(string failReason, int code = 99)
        {
            return new ResponseSimple
            {
                Code = code,
                Message = failReason,
            };
        }

        /// <summary>
        /// 成功消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResponseSimple SimpleSuccess(string msg)
        {
            return new ResponseSimple
            {
                Code = 0,
                Message = msg,
            };
        }

        /// <summary>
        /// 失败消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResponseSimple SimpleFail(string msg)
        {
            return new ResponseSimple
            {
                Code = -1,
                Message = msg,
            };
        }



        /// <summary>
        /// 成功结果
        /// </summary>
        /// <typeparam name="T">方法返回类型</typeparam>
        /// <param name="data">业务参数</param>
        /// <param name="message">说明</param>
        /// <returns></returns>
        public static Response<T> SuccessResult<T>(T data, string message = "请求成功")
        {
            return new Response<T>
            {
                Code = 0,
                Message = message,
                Data = data
            };
        }
    }
}
