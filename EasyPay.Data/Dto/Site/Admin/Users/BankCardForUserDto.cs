using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dto.Site.Admin.Users
{
    public class BankCardForUserDto
    {
        public string Id { get; set; }
        public string BankName { get; set; }
        public string OwnerName { get; set; }
        public string Shaba { get; set; }
        public string CardNumber { get; set; }
        public string ExpireDateMonth { get; set; }
        public string ExpireDateYear { get; set; }
    }
}
