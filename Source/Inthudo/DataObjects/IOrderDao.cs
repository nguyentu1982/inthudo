using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IOrderDao
    {
        OrderDetailBO GetOrderDetailById(int id);

        OrderBO GetOrderById(int orderId);

        List<OrderDetailBO> GetOrderItemsByOrderId(int orderId);

        List<OrderStatusBO> GetAllOrderStatus();

        List<DepositMethodBO> GetAllDepositMethod();

        List<ShippingMethodBO> GetAllShippingMethod();

        OrderBO InsertOrderInfo(OrderBO order);

        OrderStatusBO GetContinueOrderStatusByOrderId(int orderId);

        void InsertOrderStatusMapping(OrderStatusMappingBO orderStatusMapping);

        void UpdateOrderDetail(OrderDetailBO orderDetail);

        void InsertOrderDetail(OrderDetailBO orderDetail);

        void UpdateOrderInfo(OrderBO order);

        List<OrderBO> GetOrders(OrderSearch orderSearchObj);
    }
}
