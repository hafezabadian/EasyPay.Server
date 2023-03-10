using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Model
{
    public class User : BaseEntity<String>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 10)]
        public string UserName { get; set; }
        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Range(typeof(DateTime), "1/1/1960", "3/4/2010",
       ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActive{ get; set; }
        [StringLength(50,MinimumLength = 3)]
        public string City { get; set; }
        [Required]
        public bool IsAcive { get; set; }
        [Required]
        public bool Status { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<BankCard> BankCards { get; set; }

    }
}
