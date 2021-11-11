using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QYS.Service.Manager.UserManager;
using QYS.Service.Manager.UserManager.Dto;
using QYS_Project.Responses;

namespace QYS_Project.Controllers.MngApi
{
    [Route("api/mngapi/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        private readonly IMapper _mapper;

        public UserController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Get/{id}")]
        public async Task<Response<UserDto>> Get(int id)
        {
            var result = await _userManager.FindByIdAsync(id);

            var dto = _mapper.Map<UserDto>(result);

            return ResultBuilder.SuccessResult(dto);
        }

    }
}
