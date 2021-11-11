namespace QYS.Service.Manager.RoleOperManager.Dto
{
    public class RoleOperDto
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
        /// 操作Id
        /// </summary>
        public string OperCode { get; set; }
    }
}
