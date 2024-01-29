using Employee.Domain.Dto;
using Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Command.EmployeeReleted.UpdateEmployeeByEmployee
{
    public class UpdateEmployeeByEmployeeCommand
    {
        public EmployeeUpdate employee { get; set; }
        public string Id { get; set; }
     
    }
}
