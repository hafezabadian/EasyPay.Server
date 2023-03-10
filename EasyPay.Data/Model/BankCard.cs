using System.ComponentModel.DataAnnotations;

namespace EasyPay.Data.Model
{
    public class BankCard : BaseEntity<string>
    {
        public BankCard()
        {
            Id=Guid.NewGuid().ToString();
            DateCreated=DateTime.Now;
            DateModified=DateTime.Now;
        }
        [Required]
        [StringLength(50)]
        public string BankName { get; set; }
        [Required]
        [StringLength(100)]
        public string OwnerName { get; set; }
        [StringLength(50, MinimumLength = 20)]
        public string Shaba { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 16)]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireDateMonth { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireDateYear { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}