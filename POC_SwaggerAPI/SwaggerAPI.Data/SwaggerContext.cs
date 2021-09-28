using Microsoft.EntityFrameworkCore;
using SwaggerAPI.Domain;
using System;

namespace SwaggerAPI.Data
{
    public class SwaggerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=(local);Initial Catalog=SwaggerAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
