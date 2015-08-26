using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IProductDao
    {
        List<BusinessObjects.ProductBO> GetAllProducts();

        BusinessObjects.ProductBO GetProductByName(string productName);

        int InsertProduct(string productName);

        List<string> GetProductNames(string prefixText);
    }
}
