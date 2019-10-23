namespace InfraStructure.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=CableIdentity")
        {
        }

        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SMTPDetail> SMTPDetails { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserRoleClaim> UserRoleClaims { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasOptional(e => e.UserRoleClaim)
                .WithRequired(e => e.Role);

            modelBuilder.Entity<SMTPDetail>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.SMTPDetail)
                .HasForeignKey(e => e.SMTPDetailsId);

            modelBuilder.Entity<UserRole>()
                .HasOptional(e => e.UserRoles1)
                .WithRequired(e => e.UserRole1);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Users1)
                .WithRequired(e => e.User1);
        }
    }
}
