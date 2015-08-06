using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace InthudoService
{
    public class ProductService:ServiceBase,IProductService
    {
        static readonly IProductDao productDao = factory.ProductDao;
        public List<BusinessObjects.ProductBO> GetAllProucts()
        {
            return productDao.GetAllProducts();
        }
    }
}
