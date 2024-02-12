using Employee.Domain.Entities;
using Employee.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin
{
    public class UpdateEmployeeByAdminCommand
    {
       public UpdateEmployeeByAdminDetails Employee { get; set; }
       public string Id { get; set; }
     
    }

    public class UpdateEmployeeByAdminDetails
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public RoleType RoleType { get; set; }

        public bool IsActive { get; set; }
    }

}
