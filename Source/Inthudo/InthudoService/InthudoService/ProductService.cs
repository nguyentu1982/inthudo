using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using BusinessObjects;

namespace InthudoService
{
    public class ProductService:ServiceBase,IProductService
    {
        static readonly IProductDao productDao = factory.ProductDao;
        public List<BusinessObjects.ProductBO> GetAllProucts()
        {
            return productDao.GetAllProducts();
        }


        public ProductBO GetProductByName(string productName)
        {
            return productDao.GetProductByName(productName);
        }

        public int InsertProduct(string productName)
        {
            return productDao.InsertProduct(productName);
        }


        public List<string> GetProductNames(string prefixText)
        {
            return productDao.GetProductNames(prefixText);
        }
    }
}
