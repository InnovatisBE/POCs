using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAPI.Domain
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
