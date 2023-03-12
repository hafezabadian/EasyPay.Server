using EasyPay.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dto.Site.Admin.Users
{
    public class UserForListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }


        public ICollection<PhotoForUserDto> Photos { get; set; }
        public ICollection<BankCardForUserDto> BankCards { get; set; }
    }
}
