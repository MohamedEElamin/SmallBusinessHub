using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjectLayer;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class ProductManager : IProductManager
    {
        IProductAccessor _productAccessor;
        public ProductManager()
        {
            _productAccessor = new ProductAccessor();
        }
        public List<Product> GetProductListByActive(bool active = true)
        {
            try
            {
                return _productAccessor.SelectProductByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("List Not Available", ex);
            }
        }
    }


}
