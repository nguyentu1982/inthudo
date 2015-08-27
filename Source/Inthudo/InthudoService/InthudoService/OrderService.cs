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


        public int InsertOrderDetail(OrderDetailBO orderDetail)
        {
            return orderDao.InsertOrderDetail(orderDetail);
        }


        public OrderBO GetOrderById(int orderId)
        {
            return orderDao.GetOrderById(orderId);
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


        public void MarkOrderAsDeleted(int orderId)
        {
            orderDao.MarkOrderAsDeleted(orderId);
        }


        public DesignRequestBO GetDesignRequestById(int requestId)
        {
            return orderDao.GetDesignRequestById(requestId);
        }


        public void UpdateDesignRequest(DesignRequestBO designReq)
        {
            orderDao.UpdateDesignRequest(designReq);
        }

        public int InsertDesignRequest(DesignRequestBO designReq)
        {
            return orderDao.InsertDesignRequest(designReq);
        }


        public void MaskDesignRequestAsDeleted(int designRequestId, int deletedBy)
        {
            orderDao.MaskDesignRequestAsDeleted(designRequestId, deletedBy);
        }


        public DesignRequestBO GetDesignRequestByOrderDetailId(int orderDetailId)
        {
            return orderDao.GetDesignRequestByOrderDetailId(orderDetailId);
        }


        public ManufactureRequestBO GetManufactureRequestById(int id)
        {
            return orderDao.GetManufactureRequestById(id);
        }


        public void UpdateManufactureRequest(ManufactureRequestBO manu)
        {
            orderDao.UpdateManufactureRequest(manu);
        }

        public int InsertManufactureRequest(ManufactureRequestBO manu)
        {
            return orderDao.InsertManufactureRequest(manu);
        }


        public ManufactureRequestBO GetManufactureRequestByDesignRequest(int designRequestId)
        {
            return orderDao.GetManufactureRequestByDesignRequest(designRequestId);
        }


        public void MarkManufactureRequestAsDeleted(int manuRequestId)
        {
            orderDao.MarkManufactureRequestAsDeleted(manuRequestId);
        }


        public List<DesignRequestBO> GetDesignRequests(DesignRequestSearch searchObj)
        {
            return orderDao.GetDesignRequests(searchObj);
        }


        public List<OrganizationBO> GetAllOrganizations()
        {
            return orderDao.GetAllOrganizations();
        }
    }
}
