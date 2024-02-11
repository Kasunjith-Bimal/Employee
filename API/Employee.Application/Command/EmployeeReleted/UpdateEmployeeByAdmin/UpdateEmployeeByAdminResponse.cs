using Employee.Domain.Entities;
using Employee.Domain.Enum;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin
{
    public class UpdateEmployeeByAdminResponse
    {
        public UpdateEmployeeByAdminDetailResponse employee { get; set; }

    }

    public class UpdateEmployeeByAdminDetailResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public RoleType RoleType { get; set; }
    }


}

