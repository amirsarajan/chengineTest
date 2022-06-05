using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSales.Domain;

namespace TopSales.Core
{
    public interface IOrdersService
    {
        IList<Order> GetOrders();
    }
}
