using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NorthWindApiDemo.EFModels;
using NorthWindApiDemo.Models;
using NorthWindApiDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiDemo.Controllers
{
   
    [Route("api/customers")]
    public class OrdersController : Controller
    {
        private ICustomerRepository _customerRepoitory;

        public OrdersController(ICustomerRepository customerRepository)
        {
            _customerRepoitory = customerRepository;
        }


        [HttpGet("{customerId}/orders")]
        public IActionResult GetOrders(string customerId)
        {
            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            var orders = _customerRepoitory.GetOrders(customerId);

            var ordersResult = Mapper.Map<IEnumerable<OrdersDTO>>(orders);

            //var customer = Repository.
            //    Instance.
            //    Customers.
            //    FirstOrDefault(c => c.Id == customerId);

            //if(customer == null)
            //{
            //    return NotFound();
            //}

            //return Ok(customer.Orders);
            return Ok(ordersResult);
        }

        [HttpGet("{customerId}/orders/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(string customerId, int id)
        {
            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            var order = _customerRepoitory.GetOrder(customerId, id);
            if (order == null)
            {
                return NotFound();
            }

            var orderResult = Mapper.Map<OrdersDTO>(order);
            //var customer = Repository.
            //   Instance.
            //   Customers.
            //   FirstOrDefault(c => c.Id == customerId);

            //if (customer == null)
            //{
            //    return NotFound();
            //}

            //var order = customer.Orders.FirstOrDefault(o => o.OrderId == id);

            //if (order == null)
            //{
            //    return NotFound();
            //}

            //return Ok(order);
            return Ok(orderResult);
        }
       
        [HttpPost("{customerId}/orders")]
        public IActionResult CreateOrder(string customerId, [FromBody] OrdersForCreationDTO  order)
        {
            if(order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            //var customer = Repository.
            //   Instance.
            //   Customers.
            //   FirstOrDefault(c => c.Id == customerId);

            //if (customer == null)
            //{
            //    return NotFound();
            //}

            var finalOrder = Mapper.Map<Orders>(order);

            _customerRepoitory.Addorder(customerId, finalOrder);

            if (!_customerRepoitory.save())
            {
                return StatusCode(500, "Please verify your data");
            }

            //var maxOrderId = Repository.Instance.Customers
            //    .SelectMany(c => c.Orders)
            //    .Max(o => o.OrderId);

            //var finalOrder = new OrdersDTO()
            //{
            //        OrderId = maxOrderId++,
            //        CustomerId = order.CustomerId,
            //        EmployeeId = order.EmployeeId,
            //        OrderDate = order.OrderDate,
            //        RequiredDate = order.RequiredDate,
            //        ShippedDate = order.ShippedDate,
            //        ShipVia = order.ShipVia,
            //        Freight = order.Freight,
            //        ShipName = order.ShipName,
            //        ShipAddress = order.ShipAddress,
            //        ShipCity = order.ShipCity,
            //        ShipRegion = order.ShipRegion,
            //        ShipPostalCode = order.ShipPostalCode,
            //        ShipCountry = order.ShipCountry
            //};
            //customer.Orders.Add(finalOrder);

            var customerCreated = Mapper.Map<OrdersDTO>(finalOrder);

            return CreatedAtRoute("GetOrder",
                new {
                    customerId = customerId,id = customerCreated.OrderId
                }, customerCreated);
        }

        [HttpPut("{customerId}/orders/{id}")]
        public IActionResult UpdateOrder(string customerId, int id,[FromBody] OrdersForUpdateDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var customer = Repository.
            // Instance.
            // Customers.
            // FirstOrDefault(c => c.Id == customerId);

            //if (customer == null)
            //{
            //    return NotFound();
            //}

            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            var existingORder = _customerRepoitory.GetOrder(customerId, id);

            if (existingORder == null)
            {
                return NotFound();
            }

            //var orderFromRepository = customer.Orders.FirstOrDefault(o => o.OrderId == id);

            //if (orderFromRepository == null)
            //{
            //    return NotFound();
            //}

            Mapper.Map(order, existingORder);

            if (!_customerRepoitory.save())
            {
                return StatusCode(500, "Please verify your data");
            }

            //orderFromRepository.CustomerId = order.CustomerId;
            //orderFromRepository.EmployeeId = order.EmployeeId;
            //orderFromRepository.OrderDate = order.OrderDate;
            //orderFromRepository.RequiredDate = order.RequiredDate;
            //orderFromRepository.ShippedDate = order.ShippedDate;
            //orderFromRepository.ShipVia = order.ShipVia;
            //orderFromRepository.Freight = order.Freight;
            //orderFromRepository.ShipName = order.ShipName;
            //orderFromRepository.ShipAddress = order.ShipAddress;
            //orderFromRepository.ShipCity = order.ShipCity;
            //orderFromRepository.ShipRegion = order.ShipRegion;
            //orderFromRepository.ShipPostalCode = order.ShipPostalCode;
            //orderFromRepository.ShipCountry = order.ShipCountry;

            return NoContent();
        }

        [HttpPatch("{customerId}/orders/{id}")]
        public IActionResult UpdateOrder(string customerId, int id, [FromBody] JsonPatchDocument<OrdersForUpdateDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            var existingOrder = _customerRepoitory.GetOrder(customerId, id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            var orderToUpdate = Mapper.Map<OrdersForUpdateDTO>(existingOrder);

            //var customer = Repository.
            // Instance.
            // Customers.
            // FirstOrDefault(c => c.Id == customerId);

            //if (customer == null)
            //{
            //    return NotFound();
            //}

            //var orderFromRepository = customer.Orders.FirstOrDefault(o => o.OrderId == id);

            //if (orderFromRepository == null)
            //{
            //    return NotFound();
            //}

            //var orderToUpdate = new OrdersForUpdateDTO()
            //{
            //    CustomerId = orderFromRepository.CustomerId,
            //    EmployeeId = orderFromRepository.EmployeeId,
            //    OrderDate = orderFromRepository.OrderDate,
            //    RequiredDate = orderFromRepository.RequiredDate,
            //    ShippedDate = orderFromRepository.ShippedDate,
            //    ShipVia = orderFromRepository.ShipVia,
            //    Freight = orderFromRepository.Freight,
            //    ShipName = orderFromRepository.ShipName,
            //    ShipAddress = orderFromRepository.ShipAddress,
            //    ShipCity = orderFromRepository.ShipCity,
            //    ShipRegion = orderFromRepository.ShipRegion,
            //    ShipPostalCode = orderFromRepository.ShipPostalCode,
            //    ShipCountry = orderFromRepository.ShipCountry
            //};

            patchDocument.ApplyTo(orderToUpdate,ModelState);

            TryValidateModel(orderToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(orderToUpdate, existingOrder);

            if (!_customerRepoitory.save())
            {
                return StatusCode(500, "Please verify your data");
            }
            //orderFromRepository.CustomerId = orderToUpdate.CustomerId;
            //orderFromRepository.EmployeeId = orderToUpdate.EmployeeId;
            //orderFromRepository.OrderDate = orderToUpdate.OrderDate;
            //orderFromRepository.RequiredDate = orderToUpdate.RequiredDate;
            //orderFromRepository.ShippedDate = orderToUpdate.ShippedDate;
            //orderFromRepository.ShipVia = orderToUpdate.ShipVia;
            //orderFromRepository.Freight = orderToUpdate.Freight;
            //orderFromRepository.ShipName = orderToUpdate.ShipName;
            //orderFromRepository.ShipAddress = orderToUpdate.ShipAddress;
            //orderFromRepository.ShipCity = orderToUpdate.ShipCity;
            //orderFromRepository.ShipRegion = orderToUpdate.ShipRegion;
            //orderFromRepository.ShipPostalCode = orderToUpdate.ShipPostalCode;
            //orderFromRepository.ShipCountry = orderToUpdate.ShipCountry;

            return NoContent();
        }

        [HttpDelete("{customerId}/orders/{id}")]
        public IActionResult DeleteOrder(int customerId,int id)
        {
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

            customer.Orders.Remove(orderFromRepository);
            return NoContent();
        }
    }
}
