namespace InfraStructure.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMTPDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SMTPDetail()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(256)]
        public string SMTPEmail { get; set; }

        public string SMTPPasswordHash { get; set; }

        [Required]
        [StringLength(256)]
        public string SMTPHostName { get; set; }

        [Required]
        [StringLength(256)]
        public string SMTPFromName { get; set; }

        [Required]
        [StringLength(256)]
        public string SMTPDisplayName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
