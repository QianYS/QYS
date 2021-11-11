using System;
using Microsoft.EntityFrameworkCore;
using QYS.Entity;

namespace QYS.Service
{
    public class QYSDbContext : DbContext
    {
        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public virtual DbSet<Oper> Opers { get; set; }

        /// <summary>
        /// 用户角色表
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 角色菜单表
        /// </summary>
        public virtual DbSet<RoleMenu> RoleMenus { get; set; }

        /// <summary>
        /// 角色操作表
        /// </summary>
        public virtual DbSet<RoleOper> RoleOpers { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public QYSDbContext(DbContextOptions<QYSDbContext> options)
            : base(options)
        {
        }

        [Obsolete]
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.CreateTime).HasColumnType("datetime");
                entity.Property(e => e.LastUpdate).HasColumnType("datetime");
                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.Property(e => e.Remark).HasColumnType("text").IsUnicode(false);
                entity.Property(e => e.IsSuper).HasDefaultValue(false);
                entity.Property(e => e.CreateTime).HasColumnType("datetime");
                entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            });

            builder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menus");
                entity.Property(e => e.Code).IsRequired().ValueGeneratedNever();
                entity.Property(e => e.CreateTime).HasColumnType("datetime");
                entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            });

            builder.Entity<Oper>(entity =>
            {
                entity.ToTable("Opers");
                entity.Property(e => e.Code).IsRequired().ValueGeneratedNever();
                entity.Property(e => e.CreateTime).HasColumnType("datetime");
                entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique().HasName("userid_roleid");
            });

            builder.Entity<RoleMenu>(entity =>
            {
                entity.ToTable("RoleMenus");
                entity.HasIndex(e => new { e.RoleId, e.MenuCode }).IsUnique().HasName("roleid_menucode");
            });

            builder.Entity<RoleOper>(entity =>
            {
                entity.ToTable("RoleOpers");
                entity.HasIndex(e => new { e.RoleId, e.OperCode }).IsUnique().HasName("roleid_opercode");
            });
        }
    }
}
