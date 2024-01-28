using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.AuthenticationReleted.Register
{
    public class RegisterEmployeeCommand
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public RoleType RoleType { get; set; }

    }
}
