using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QYS.Entity
{
    public class UserRole
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

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
    }
}
