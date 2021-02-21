using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthWindApiDemo.Models;
using NorthWindApiDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiDemo.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        //Inyectar una dependencia de customerRepository
        private ICustomerRepository _customerRepostory;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepostory = customerRepository;
        }

        [HttpGet()]
        public JsonResult GetCustomers()
        {
            
            // lo ideal es devolver el conjunto de datos personalizados del DTO y no de la base de datos
            var customers = _customerRepostory.GetCustomers();
            var results = Mapper.Map<IEnumerable<CustomerWithoutOrders>>(customers);         

            return new JsonResult(results);
        }

        [HttpGet("{id}")]

        //Regresar tipo generico ejemplo de JsonResult  retornar IACtionResult
        public IActionResult GetCustomer(string id, bool includeOrders = false)
        {

            var customer = _customerRepostory.GetCustomer(id, includeOrders);

            if (customer == null)
            {
                return NotFound();
            }
            
            if (includeOrders)
            {
                var customerResult = Mapper.Map<CustomerDTO>(customer);
                return Ok(customerResult);
            }

            var customerResultOnly = Mapper.Map<CustomerWithoutOrders>(customer);
            return Ok(customerResultOnly);
        }
    }
}
