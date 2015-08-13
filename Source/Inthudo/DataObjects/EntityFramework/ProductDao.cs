using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    public class ProductDao:IProductDao
    {
        static ProductDao()
        {
            Mapper.CreateMap<Product, BusinessObjects.ProductBO>();
            Mapper.CreateMap<BusinessObjects.ProductBO, Product>();
        }

        public List<BusinessObjects.ProductBO> GetAllProducts()
        {
            using (context)
            {
                var query = context.Products.ToList();
                return Mapper.Map<List<Product>, List<BusinessObjects.ProductBO>>(query);
            }
        }

        public InThuDoEntities context
        {
            get
            {
                return new InThuDoEntities();
            }
        }
    }
}
