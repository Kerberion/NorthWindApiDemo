using NorthWindApiDemo.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiDemo.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customers> GetCustomers();
        Customers GetCustomer(string customerID, bool includeOrders);
        IEnumerable<Orders> GetOrders(string customerId);
        Orders GetOrder(string customerId, int orderId);
        bool CustomerExists(string customerId);
    }
}
