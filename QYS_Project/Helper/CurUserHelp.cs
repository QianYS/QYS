using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QYS.Service.Common;

namespace QYS_Project.Helper
{
    public class CurUserHelp
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurUserHelp(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        /// <summary>
        /// 当前用户会话
        /// </summary>
        public UserInfo UserInfo => GetUserInfo();

        /// <summary>
        /// 解析token 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        private UserInfo GetUserInfo()
        {
            var listClaims = _contextAccessor.HttpContext.User.Claims;
            var enumerable = listClaims as Claim[] ?? listClaims.ToArray();
            if (!enumerable.Any())
                return null;
            return new UserInfo
            {
                Id = long.Parse(enumerable.FirstOrDefault(x => x.Type == ConstValue.UserId)?.Value ?? ""),
                Name = enumerable.FirstOrDefault(x => x.Type == ConstValue.Name)?.Value,
                LogName = enumerable.FirstOrDefault(x => x.Type == ConstValue.LoginName)?.Value,
                //Roles = enumerable.FirstOrDefault(x => x.Type == ConstValue.Roles)?.Value,
            };
        }

    }

    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// 真实名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        //public string Roles { get; set; }
    }
}
