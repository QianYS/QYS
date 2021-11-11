using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QYS.Entity
{
    public class RoleOper
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
        /// 操作Id
        /// </summary>
        [Required]
        public string OperCode { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [Required]
        [ForeignKey(nameof(OperCode))]
        public Oper Oper { get; set; }
    }
}
