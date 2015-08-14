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


        public OrderDetailBO GetOrderDetailById(int id)
        {
            return orderDao.GetOrderDetailById(id);
        }

        public void UpdateOrderDetail(OrderDetailBO orderDetail)
        {
            orderDao.UpdateOrderDetail(orderDetail);
        }


        public void InsertOrderDetail(OrderDetailBO orderDetail)
        {
            orderDao.InsertOrderDetail(orderDetail);
        }


        public OrderBO GetOrderById(int orderId)
        {
            return orderDao.GetOrderById(orderId);
        }

        

        void IOrderService.UpdateOrderDetail(OrderDetailBO orderDetail)
        {
            orderDao.UpdateOrderDetail(orderDetail);
        }

        void IOrderService.InsertOrderDetail(OrderDetailBO orderDetail)
        {
            orderDao.InsertOrderDetail(orderDetail);
        }

        List<OrderDetailBO> IOrderService.GetOrderItemsByOrderId(int orderId)
        {
            return orderDao.GetOrderItemsByOrderId(orderId);
        }


        public List<OrderStatusBO> GetAllOrderStatus()
        {
            return orderDao.GetAllOrderStatus();
        }


        public List<DepositMethodBO> GetAllDepositMethod()
        {
            return orderDao.GetAllDepositMethod();
        }


        public List<ShippingMethodBO> GetAllShippingMehod()
        {
            return orderDao.GetAllShippingMethod();
        }


        public OrderBO InsertOrderInfo(OrderBO order)
        {
            return orderDao.InsertOrderInfo(order);
        }

        public OrderStatusBO GetContinueOrderStatusByOrderId(int orderId)
        {
            return orderDao.GetContinueOrderStatusByOrderId(orderId);
        }


        public void InsertOrderStatusMapping(OrderStatusMappingBO orderStatusMapping)
        {
            orderDao.InsertOrderStatusMapping(orderStatusMapping);
        }


        public void UpdateOrderInfo(OrderBO order)
        {
            orderDao.UpdateOrderInfo(order);
        }


        public List<OrderBO> GetOrders(OrderSearch orderSearchObj)
        {
            return orderDao.GetOrders(orderSearchObj);
        }


        public List<OrderDetailBO> GetOrderDetailsByOrderId(int orderId)
        {
            return orderDao.GetOrderDetailsByOrderId(orderId);
        }


        public void MarkOrderDetailAsDeleted(int orderDetailId)
        {
            orderDao.MarkOrderDetailAsDeleted(orderDetailId);
        }
    }
}
