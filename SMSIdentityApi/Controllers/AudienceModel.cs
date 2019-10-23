using System.ComponentModel.DataAnnotations;

namespace SMSIdentityApi.Controllers
{
    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}