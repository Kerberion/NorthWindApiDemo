﻿using Microsoft.AspNetCore.Mvc;
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

            //return new JsonResult(Repository.Instance.Customers);

            ////return new JsonResult(new List<object>()
            ////{
            ////    new { CustomerID =1, ContactName = "Anderson"},
            ////    new { CustomerID =2, ContactName = "Solaris"}
            ////});
            ///
            // lo ideal es devolver el conjunto de datos personalizados del DTO y no de la base de datos
            var customers = _customerRepostory.GetCustomers();
            var results = new List<CustomerWithoutOrders>();

            foreach(var customer in customers)
            {
                results.Add(new CustomerWithoutOrders()
                {
                    CustomerID = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTittle = customer.ContactTitle,
                    Address = customer.Address,
                    City = customer.City,
                    Region = customer.Region,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone,
                    Fax = customer.Fax
                });
            }

            return new JsonResult(results);
        }

        [HttpGet("{id}")]
        //Regresar tipo generico ejemplo de JsonResult  retornar IACtionResult
        public IActionResult GetCustomer(int id)
        {
            var result =
                Repository.Instance.Customers.
                FirstOrDefault(c => c.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
            //return new JsonResult(result);
        }
    }
}
