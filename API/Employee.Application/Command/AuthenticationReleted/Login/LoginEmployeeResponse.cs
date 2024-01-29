using Employee.Domain.Entities;
using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.AuthenticationReleted.Login
{
    public class LoginEmployeeResponse
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string AccessToken { get; set; }


        public DateTime Expire { get; set; }

        public bool IsFirstLogin {get;set;}

    }
}
