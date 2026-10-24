using System.ComponentModel.DataAnnotations;

namespace ApiUFC.Models
{
    public class Division
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;
    }
}
