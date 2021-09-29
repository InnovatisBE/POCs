using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwaggerAPI.Data;
using SwaggerAPI.Domain;
using SwaggerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly SwaggerContext _context;
        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger, SwaggerContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Customers
        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomers()
        {
            var result = await _context.Customers
                .Select(c => _mapper.Map<CustomerModel>(c))
                .ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerModel>> GetCustomer(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerModel>(customer));
        }

        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerModel">The customer model.</param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, CustomerModel customerModel)
        {
            if (customerId != customerModel.CustomerId)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = customerModel.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CustomerExists(customerId))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomerModel>> CreateCustomer(CustomerModel customerModel)
        {
            var customer = new Customer
            {
                Name = customerModel.Name
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.CustomerId },
                _mapper.Map<CustomerModel>(customer));
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid customerId) =>
             _context.Customers.Any(e => e.CustomerId == customerId);
    }
}
