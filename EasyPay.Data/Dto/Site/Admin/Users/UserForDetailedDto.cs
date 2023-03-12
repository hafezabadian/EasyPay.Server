using EasyPay.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Data.Dto.Site.Admin.Users
{
    public class UserForDetailedDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
    }
}
