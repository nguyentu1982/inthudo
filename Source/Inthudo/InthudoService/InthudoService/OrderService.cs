using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace InthudoService
{
    public class OrderService:ServiceBase, IOrderService
    {
        static readonly IOrderDao orderDao = factory.OrderDao;


        public BusinessObjects.OrderDetailBO GetOrderDetailById(int p)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderDetail(BusinessObjects.OrderDetailBO orderDetail)
        {
            throw new NotImplementedException();
        }


        public void InsertOrderDetail(OrderDetailBO orderDetail)
        {
            throw new NotImplementedException();
        }


        public OrderBO GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
