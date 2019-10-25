namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserClaim
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? ClaimId { get; set; }

        public virtual Claim Claim { get; set; }

        public virtual User User { get; set; }
    }
}
