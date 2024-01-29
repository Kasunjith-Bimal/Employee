using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.AuthenticationReleted.Login
{
    public class LoginEmployeeCommand
    {
        public string Email { get; set; }
       
        public string Password { get; set; }

    }
}
