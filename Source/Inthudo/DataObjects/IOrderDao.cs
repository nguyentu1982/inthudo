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

        List<OrderStatusBO> GetAllOrderStatus();

        List<DepositMethodBO> GetAllDepositMethod();

        List<ShippingMethodBO> GetAllShippingMethod();

        OrderBO InsertOrderInfo(OrderBO order);

        OrderStatusBO GetContinueOrderStatusByOrderId(int orderId);

        void InsertOrderStatusMapping(OrderStatusMappingBO orderStatusMapping);

        void UpdateOrderDetail(OrderDetailBO orderDetail);

        int InsertOrderDetail(OrderDetailBO orderDetail);

        void UpdateOrderInfo(OrderBO order);

        List<OrderBO> GetOrders(OrderSearch orderSearchObj);

        List<OrderDetailBO> GetOrderDetailsByOrderId(int orderId);

        void MarkOrderDetailAsDeleted(int orderDetailId);

        void MarkOrderAsDeleted(int orderId);

        DesignRequestBO GetDesignRequestById(int requestId);

        void UpdateDesignRequest(DesignRequestBO designReq);
        /// <summary>
        /// Insert Design return ID
        /// </summary>
        /// <param name="designReq"></param>
        /// <returns></returns>
        int InsertDesignRequest(DesignRequestBO designReq);

        void MaskDesignRequestAsDeleted(int designRequestId, int deletedBy);

        DesignRequestBO GetDesignRequestByOrderDetailId(int orderDetailId);

        ManufactureRequestBO GetManufactureRequestById(int id);

        void UpdateManufactureRequest(ManufactureRequestBO manu);

        int InsertManufactureRequest(ManufactureRequestBO manu);

        ManufactureRequestBO GetManufactureRequestByDesignRequest(int designRequestId);

        void MarkManufactureRequestAsDeleted(int manuRequestId);

        List<DesignRequestBO> GetDesignRequests(DesignRequestSearch searchObj);

        List<OrganizationBO> GetAllOrganizations();
    }
}
