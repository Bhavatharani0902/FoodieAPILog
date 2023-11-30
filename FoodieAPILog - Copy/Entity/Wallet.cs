using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungryHUB.Entity
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        [Required]
        public double Balance { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}