﻿using Employee.Domain.Entities;
using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.AuthenticationReleted.Register
{
    public class RegisterEmployeeResponse
    {
        public EmployeeDetail employee { get; set; }

    }
}
