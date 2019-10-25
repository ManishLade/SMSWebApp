namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRoleClaim
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? ClaimId { get; set; }

        public virtual Role Role { get; set; }
    }
}
