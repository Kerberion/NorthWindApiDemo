using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWindApiDemo.EFModels;

namespace NorthWindApiDemo.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private NorthWindContext _context;

        public CustomerRepository(NorthWindContext context)
        {
            _context = context;
        }

        public bool CustomerExists(string customerId)
        {
            return _context.Customers.Any(c => c.CustomerId == customerId);
        }

        public Customers GetCustomer(string customerId, bool includeOrders)
        {
            if (includeOrders)
            {
                return _context.Customers
                    .Include(c => c.Orders)
                    .Where(c => c.CustomerId == customerId)
                    .FirstOrDefault();
            }
            return _context.Customers
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefault();
        }

        public IEnumerable<Customers> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.CompanyName)
                 .ToList();
        }

        public Orders GetOrder(string customerId, int orderId)
        {
            return _context.Orders
                 .Where(c => c.CustomerId == customerId && c.OrderId == orderId)
                 .FirstOrDefault();
        }

        public IEnumerable<Orders> GetOrders(string customerId)
        {
            return _context.Orders.Where(c => c.CustomerId == customerId).ToList();
        }
    }
}
