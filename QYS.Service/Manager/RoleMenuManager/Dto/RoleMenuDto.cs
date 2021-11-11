namespace QYS.Service.Manager.RoleMenuManager.Dto
{
    public class RoleMenuDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuCode { get; set; }
    }
}
