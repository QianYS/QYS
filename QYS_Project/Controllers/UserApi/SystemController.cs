using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QYS.Service.Service.SystemSvr;
using QYS.Service.Service.SystemSvr.Dto;
using QYS.Service.Service.UserSvr;
using QYS_Project.Helper;
using QYS_Project.Requests;
using QYS_Project.Requests.System;
using QYS_Project.Responses;
using QYS_Project.Responses.Model;

namespace QYS_Project.Controllers.UserApi
{
    [Route("userApi/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly ISystemService _systemService;

        private readonly CurUserHelp _curUserHelp;

        public SystemController(IUserService userService
            , IMapper mapper
            , ISystemService systemService
            , CurUserHelp curUserHelp)
        {
            _userService = userService;
            _mapper = mapper;
            _systemService = systemService;
            _curUserHelp = curUserHelp;
        }

        /// <summary>
        /// 初始化系统管理（菜单 菜单操作）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("InitSystemManage")]
        public async Task<ResponseSimple> InitSystemManage(InitSystemManageRequest request)
        {
            var data = _mapper.Map<List<MenuAndOper>, List<InitSystemDto>>(request.MenuAndOpers);

            //数据解析
            var (menus, opers) = await _systemService.Structure(data);

            //菜单处理
            await _systemService.UpdateMenu(menus);

            //操作处理
            await _systemService.UpdateOper(opers);

            return ResultBuilder.SimpleSuccess("请求成功");
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetMenus")]
        public async Task GetMenus()
        {
            var user = _curUserHelp.UserInfo;

            // 根据用户id  获取角色

            // 根据角色获取所有菜单（过滤弹窗级别的菜单）

        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<Response<TokenResponse>> Register(RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request.LoginName, request.Password, request.Address);

            if (!result.Success)
                return ResultBuilder.FailResult((TokenResponse) null, result.Errors);

            return ResultBuilder.SuccessResult(new TokenResponse
            {
                AccessToken = result.AccessToken,
                TokenType = result.TokenType,
                ExpiresIn = result.ExpiresIn,
                RefreshToken = result.RefreshToken
            });
            
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<Response<TokenResponse>> Login(LoginRequest request)
        {
            var result = await _userService.LoginAsync(request.UserName, request.Password);
            if (!result.Success)
                return ResultBuilder.FailResult((TokenResponse)null, result.Errors);

            return ResultBuilder.SuccessResult(new TokenResponse
            {
                AccessToken = result.AccessToken,
                TokenType = result.TokenType,
                ExpiresIn = result.ExpiresIn,
                RefreshToken = result.RefreshToken
            });
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [ServiceFilter(typeof(QYSAuthorizeAttribute))]
        public async Task<Response<TokenResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var result = await _userService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
            if (!result.Success)
                return ResultBuilder.FailResult((TokenResponse)null, result.Errors);

            return ResultBuilder.SuccessResult(new TokenResponse
            {
                AccessToken = result.AccessToken,
                TokenType = result.TokenType,
                ExpiresIn = result.ExpiresIn,
                RefreshToken = result.RefreshToken
            });
        }

    }
}
