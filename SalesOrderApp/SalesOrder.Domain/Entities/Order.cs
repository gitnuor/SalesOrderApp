using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Domain.Entities
{
    public class Order
    {
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public string State { get; set; }
        public string WindowName { get; set; }
        public string WindowId { get; set; }
    }
}
