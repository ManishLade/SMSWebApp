using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public DateTime CreationTs { get; set; }

        [NotMapped]
        public string CreatedBy { get; set; }

        [NotMapped]
        public DateTime? ModifiedTs { get; set; }

        [NotMapped]
        public string ModifiedBy { get; set; }

        [NotMapped]
        public StatusType StatusType { get; set; }
    }

    public enum StatusType : byte
    {
        Disabled = 0,
        Enabled = 1
    }
}
