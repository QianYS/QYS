using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QYS.Entity
{
    public class RoleMenu
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Required]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [Required]
        [ForeignKey(nameof(MenuCode))]
        public Menu Menu { get; set; }
    }
}
