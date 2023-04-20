using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Domain.Entities
{
    public class WindowElement
    {
        public string SubElementsId { get; set; }
        public string WindowId { get; set; }
        public string WindowName { get; set; }
        public string Type { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}

