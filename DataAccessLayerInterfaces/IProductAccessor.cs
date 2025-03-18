﻿using DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface IProductAccessor
    {
        List<Product> SelectProductByActive(bool active = true);
    }
}
