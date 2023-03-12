using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dto.Site.Admin.Users
{
    public class UserForLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمیباشد")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsRemember { get; set; }
    }
}
