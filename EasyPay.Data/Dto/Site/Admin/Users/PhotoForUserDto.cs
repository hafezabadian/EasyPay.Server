using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dto.Site.Admin.Users
{
    public class PhotoForUserDto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Alt { get; set; }
        public bool IsMain { get; set; }
    }
}
