using AutoMapper;
using SwaggerAPI.Domain;
using System;
using System.Collections.Generic;

namespace SwaggerAPI.Models
{
    [AutoMap(typeof(Customer), ReverseMap = true)]
    public class CustomerModel
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public List<EmployeeModel> Employees { get; } = new List<EmployeeModel>();
    }
}
