using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253503_Kudosh.Domain.Entities
{
    public class CartItem
    {
        public TelescopeEntity Item { get; set; }
        public int Quantity { get; set; }
    }
}
