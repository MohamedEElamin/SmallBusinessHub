using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjectLayer;

namespace LogicLayerInterfaces
{
    public interface IProductManager
    {
        List<Product> GetProductListByActive(bool active = true);
    }
}
