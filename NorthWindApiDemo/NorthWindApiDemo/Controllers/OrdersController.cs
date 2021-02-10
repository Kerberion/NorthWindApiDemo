using Microsoft.AspNetCore.Mvc;
using NorthWindApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiDemo.Controllers
{
   
    [Route("api/customers")]
    public class OrdersController : Controller
    {
        [HttpGet("{customerId}/orders")]
        public IActionResult GetOrders(int customerId)
        {
            var customer = Repository.
                Instance.
                Customers.
                FirstOrDefault(c => c.Id == customerId);

            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer.Orders);
        }

        [HttpGet("{customerId}/orders/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int customerId, int id)
        {
            var customer = Repository.
               Instance.
               Customers.
               FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var order = customer.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
       
        [HttpPost("{customerId}/orders")]
        public IActionResult CreateOrder(int customerId, [FromBody] OrdersForCreationDTO  order)
        {
            if(order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = Repository.
               Instance.
               Customers.
               FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var maxOrderId = Repository.Instance.Customers
                .SelectMany(c => c.Orders)
                .Max(o => o.OrderId);

            var finalOrder = new OrdersDTO()
            {
                    OrderId = maxOrderId++,
                    CustomerId = order.CustomerId,
                    EmployeeId = order.EmployeeId,
                    OrderDate = order.OrderDate,
                    RequiredDate = order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    ShipVia = order.ShipVia,
                    Freight = order.Freight,
                    ShipName = order.ShipName,
                    ShipAddress = order.ShipAddress,
                    ShipCity = order.ShipCity,
                    ShipRegion = order.ShipRegion,
                    ShipPostalCode = order.ShipPostalCode,
                    ShipCountry = order.ShipCountry
            };
            customer.Orders.Add(finalOrder);

            return CreatedAtRoute("GetOrder",
                new {
                    customerId = customerId,id = finalOrder.OrderId
                }, finalOrder);
        }

        [HttpPut("{customerId}/orders/{id}")]
        public IActionResult UpdateOrder(int customerId, int id,[FromBody] OrdersForUpdateDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = Repository.
             Instance.
             Customers.
             FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var orderFromRepository = customer.Orders.FirstOrDefault(o => o.OrderId == id);

            if (orderFromRepository == null)
            {
                return NotFound();
            }

            orderFromRepository.CustomerId = order.CustomerId;
            orderFromRepository.EmployeeId = order.EmployeeId;
            orderFromRepository.OrderDate = order.OrderDate;
            orderFromRepository.RequiredDate = order.RequiredDate;
            orderFromRepository.ShippedDate = order.ShippedDate;
            orderFromRepository.ShipVia = order.ShipVia;
            orderFromRepository.Freight = order.Freight;
            orderFromRepository.ShipName = order.ShipName;
            orderFromRepository.ShipAddress = order.ShipAddress;
            orderFromRepository.ShipCity = order.ShipCity;
            orderFromRepository.ShipRegion = order.ShipRegion;
            orderFromRepository.ShipPostalCode = order.ShipPostalCode;
            orderFromRepository.ShipCountry = order.ShipCountry;

            return NoContent();
        }
    }
}
