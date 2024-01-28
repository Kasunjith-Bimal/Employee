using Employee.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Application.Queries.EmployeeReleted.GetAllAdmin
{
    public class GetAllAdminResponse
    {
        public List<EmployeeDetail> employees { get; set; }

    }
}
