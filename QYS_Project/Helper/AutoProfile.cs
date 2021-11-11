using System;
using AutoMapper;
using QYS.Entity;
using QYS.Service.Manager.MenuManager.Dto;
using QYS.Service.Manager.OperManager.Dto;
using QYS.Service.Manager.RefreshTokenManager.Dto;
using QYS.Service.Manager.RoleManager.Dto;
using QYS.Service.Manager.RoleMenuManager.Dto;
using QYS.Service.Manager.RoleOperManager.Dto;
using QYS.Service.Manager.UserManager.Dto;
using QYS.Service.Service.SystemSvr.Dto;
using QYS.Service.Tool;
using QYS_Project.Requests.System;

namespace QYS_Project.Helper
{
    public class AutoProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Oper, OperDto>().ReverseMap();
            CreateMap<RoleMenu, RoleMenuDto>().ReverseMap();
            CreateMap<RoleOper, RoleOperDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();

            CreateMap<MenuAction, OperDto>()
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.ActionUrl))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.ActionMethod))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ActionName))
				.ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.Now))
				.ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<InitSystemDto, MenuDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MenuName))
				.ForMember(dest => dest.Code, opt => opt.MapFrom(src => EncryptionHelper.Md5Encrypt32(src.Url)))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<InitSystemDto, MenuAndOper>().ReverseMap();
            CreateMap<MenuAction, MenuOper>().ReverseMap();
        }
    }
}
