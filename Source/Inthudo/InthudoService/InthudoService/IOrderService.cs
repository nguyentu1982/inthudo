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

        List<OrderDetailBO> GetOrderItemsByOrderId(int p);

        List<OrderStatusBO> GetAllOrderStatus();

        List<DepositMethodBO> GetAllDepositMethod();

        List<ShippingMethodBO> GetAllShippingMehod();

        OrderBO InsertOrderInfo(OrderBO order);

        OrderStatusBO GetContinueOrderStatusByOrderId(int p);

        void InsertOrderStatusMapping(OrderStatusMappingBO orderStatusMapping);

        void UpdateOrderInfo(OrderBO order);

        List<OrderBO> GetOrders(OrderSearch orderSearchObj);

        List<OrderDetailBO> GetOrderDetailsByOrderId(int p);

        void MarkOrderDetailAsDeleted(int orderDetailId);

        void MarkOrderAsDeleted(int orderId);

        DesignRequestBO GetDesignRequestById(int p);

        void UpdateDesignRequest(DesignRequestBO designReq);

        int InsertDesignRequest(DesignRequestBO designReq);

        void MaskDesignRequestAsDeleted(int designRequestId, int deletedBy);

        DesignRequestBO GetDesignRequestByOrderDetailId(int orderDetailId);

        ManufactureRequestBO GetManufactureRequestById(int p);

        void UpdateManufactureRequest(ManufactureRequestBO manu);

        int InsertManufactureRequest(ManufactureRequestBO manu);

        ManufactureRequestBO GetManufactureRequestByDesignRequest(int p);
    }
}
