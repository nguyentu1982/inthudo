using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace DataObjects.EntityFramework
{
    public class OrderDao:IOrderDao
    {
        static OrderDao()
        {
            Mapper.CreateMap<Order, BusinessObjects.OrderBO>();
            Mapper.CreateMap<BusinessObjects.OrderBO, Order>();
        }


    }
}
