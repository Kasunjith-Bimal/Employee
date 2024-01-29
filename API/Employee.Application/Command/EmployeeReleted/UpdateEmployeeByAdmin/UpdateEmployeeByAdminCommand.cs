using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.EmployeeReleted.UpdateEmployeeByAdmin
{
    public class UpdateEmployeeByAdminCommand
    {
        public EmployeeDetail employee { get; set; }

        public string Id { get; set; }
     
    }
}
