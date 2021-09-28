using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwaggerAPI.Domain
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; } = new List<Employee>();
    }
}
