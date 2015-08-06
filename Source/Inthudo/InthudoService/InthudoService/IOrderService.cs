using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace InthudoService
{
    public interface IOrderService
    {

        OrderDetailBO GetOrderDetailById(int p);

        void UpdateOrderDetail(OrderDetailBO orderDetail);

        void InsertOrderDetail(OrderDetailBO orderDetail);

        OrderBO GetOrderById(int orderId);
    }
}
