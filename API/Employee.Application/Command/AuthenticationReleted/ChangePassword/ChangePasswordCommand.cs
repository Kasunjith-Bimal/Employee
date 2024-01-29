using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.AuthenticationReleted.ChangePassword
{
    public class ChangePasswordCommand
    {
        public string Email { get; set; }
       
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }


    }
}
