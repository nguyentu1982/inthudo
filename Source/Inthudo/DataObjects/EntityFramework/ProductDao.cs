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
            using (var context = new InThuDoEntities())
            {
                var query = context.Products.ToList();
                return Mapper.Map<List<Product>, List<BusinessObjects.ProductBO>>(query);
            }
        }

        public ProductBO GetProductByName(string productName)
        {
            using (var context = new InThuDoEntities())
            {
                var query = (from p in context.Products
                            where p.Name == productName
                            select new ProductBO() { 
                                ProductId = p.ProductId,
                                Name = p.Name,
                                Description=p.Description
                            }).FirstOrDefault();
                return query;
            }
        }

        public int InsertProduct(string productName)
        {
            using (var context = new InThuDoEntities())
            {
                Product p = new Product()
                {
                    Name = productName
                };
                context.Entry(p).State = System.Data.EntityState.Added;
                context.Products.Add(p);
                context.SaveChanges();
                return p.ProductId;
            }
        }


        public List<string> GetProductNames(string prefixText)
        {
            using (var context = new InThuDoEntities())
            {
                List<string> result = new List<string>();
                var query = from p in context.Products
                             where p.Name.Contains(prefixText)
                             select p;
                foreach(Product p in query)
                {
                    result.Add(p.Name);
                }
                return result;
                
            }
        }
    }
}
