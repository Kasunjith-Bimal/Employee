using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Entities
{
    public class EmployeeDetail : IdentityUser
    {
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}
