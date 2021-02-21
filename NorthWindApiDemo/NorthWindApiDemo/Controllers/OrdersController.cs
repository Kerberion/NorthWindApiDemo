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


            var finalOrder = Mapper.Map<Orders>(order);

            _customerRepoitory.Addorder(customerId, finalOrder);

            if (!_customerRepoitory.save())
            {
                return StatusCode(500, "Please verify your data");
            }

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

            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            var existingORder = _customerRepoitory.GetOrder(customerId, id);

            if (existingORder == null)
            {
                return NotFound();
            }

            Mapper.Map(order, existingORder);

            if (!_customerRepoitory.save())
            {
                return StatusCode(500, "Please verify your data");
            }

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

            return NoContent();
        }

        [HttpDelete("{customerId}/orders/{id}")]
        public IActionResult DeleteOrder(string customerId,int id)
        {

            if (!_customerRepoitory.CustomerExists(customerId))
            {
                return NotFound();
            }

            var existingOrder = _customerRepoitory.GetOrder(customerId, id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            //En esta línea el registro se esta borrando unicamente en memoría
            _customerRepoitory.DeleteOrder(existingOrder);

            //en el save se ejecuta la instruccion para hacer el guardado en la base de datos
            if (!_customerRepoitory.save())
            {
                return StatusCode(500, "Please verify your data");
            }

            return NoContent();
        }
    }
}
