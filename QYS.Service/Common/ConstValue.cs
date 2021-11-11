using System;

namespace QYS.Service.Common
{
    public class ConstValue
    {
        /// <summary>
        /// JwtId
        /// </summary>
        public const string JwtId = "JwtId";

        /// <summary>
        /// 用户Id
        /// </summary>
        public const string UserId = "UserId";

        /// <summary>
        /// 用户角色（多个用,分割）
        /// </summary>
        public const string Roles = "Roles";

        /// <summary>
        /// 头像
        /// </summary>
        public const string Actor = "Actor";

        /// <summary>
        /// 用户名
        /// </summary>
        public const string Name = "Name";

        /// <summary>
        /// 登录名
        /// </summary>
        public const string LoginName = "LoginName";

        /// <summary>
        /// 最初时间
        /// </summary>
        public DateTime BeginTime = DateTime.MinValue;
    }
}
