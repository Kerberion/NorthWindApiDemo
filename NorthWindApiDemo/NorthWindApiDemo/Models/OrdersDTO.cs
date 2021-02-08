using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiDemo.Models
{
    public class OrdersDTO
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public int Freight { get; set; }
        public int ShipName { get; set; }
        public int ShipAddress { get; set; }
        public int ShipCity { get; set; }
        public int ShipRegion { get; set; }
        public int ShipPostalCode { get; set; }
        public int ShipCountry { get; set; }

    }
}
