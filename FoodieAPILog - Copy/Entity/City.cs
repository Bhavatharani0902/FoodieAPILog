using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HungryHUB.Entity
{
    public class City
    {
        [Key]
        public int? CityID { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string? CityName { get; set; }
    }

}
