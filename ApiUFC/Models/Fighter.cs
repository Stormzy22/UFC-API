using System.ComponentModel.DataAnnotations;

namespace ApiUFC.Models
{
    public class Fighter
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Nickname { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int DivisionId { get; set; }

        [Range(1, int.MaxValue)]
        public int NationId { get; set; }

        [Range(0, 200)]
        public int Wins { get; set; }

        [Range(0, 200)]
        public int Losses { get; set; }

        [Range(0, 200)]
        public int Draws { get; set; }
    }
}
