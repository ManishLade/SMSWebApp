namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRole
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual UserRole UserRoles1 { get; set; }

        public virtual UserRole UserRole1 { get; set; }
    }
}
