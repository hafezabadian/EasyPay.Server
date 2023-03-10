using System.ComponentModel.DataAnnotations;

namespace EasyPay.Data.Model
{
    public class Photo : BaseEntity<string>
    {
        public Photo()
        {
            Id = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
        [Required]
        [StringLength(1000)]
        public string Url { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(500)]
        public string Alt { get; set; }
        [Required]
        public bool IsMain { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}