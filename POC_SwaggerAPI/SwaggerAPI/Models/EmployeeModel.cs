using AutoMapper;
using SwaggerAPI.Domain;
using System;

namespace SwaggerAPI.Models
{
    [AutoMap(typeof(Employee), ReverseMap = true)]
    public class EmployeeModel
    {
        public Guid EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
