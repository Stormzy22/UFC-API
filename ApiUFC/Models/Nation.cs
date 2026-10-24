using System.ComponentModel.DataAnnotations;

namespace ApiUFC.Models
{
    public class Nation
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string IsoCode { get; set; } = string.Empty;
    }
}
