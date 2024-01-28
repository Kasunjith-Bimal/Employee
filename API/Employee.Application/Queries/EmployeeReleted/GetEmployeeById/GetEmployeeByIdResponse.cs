using Employee.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.Application.Queries.EmployeeReleted.GetEmployeeById
{
    public class GetEmployeeByIdResponse
    {
        public EmployeeDetail employee { get; set; }

    }
}
