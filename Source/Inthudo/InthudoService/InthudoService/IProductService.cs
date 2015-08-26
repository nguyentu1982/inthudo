using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InthudoService
{
    public interface IProductService
    {
        List<BusinessObjects.ProductBO> GetAllProucts();

        BusinessObjects.ProductBO GetProductByName(string p);

        int InsertProduct(string p);

        List<string> GetProductNames(string prefixText);
    }
}
