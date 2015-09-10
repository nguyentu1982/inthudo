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

        OrderItemlBO GetOrderDetailById(int p);

        void UpdateOrderDetail(OrderItemlBO orderDetail);

        int InsertOrderDetail(OrderItemlBO orderDetail);

        OrderBO GetOrderById(int orderId);

        List<OrderStatusBO> GetAllOrderStatus();

        List<DepositMethodBO> GetAllDepositMethod();

        List<ShippingMethodBO> GetAllShippingMehod();

        OrderBO InsertOrderInfo(OrderBO order);

        OrderStatusBO GetContinueOrderStatusByOrderId(int p);

        void InsertOrderStatusMapping(OrderStatusMappingBO orderStatusMapping);

        void UpdateOrderInfo(OrderBO order);

        List<OrderBO> GetOrders(OrderSearch orderSearchObj);

        List<OrderItemlBO> GetOrderDetailsByOrderId(int p);

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

        void MarkManufactureRequestAsDeleted(int p);

        List<DesignRequestBO> GetDesignRequests(DesignRequestSearch searchObj);

        List<OrganizationBO> GetAllOrganizations();

        List<ProductApprovedBO> GetApprovedProductByOrderId(int orderId);

        List<ProductApprovedBO> GetFailedProductByOrderId(int p);

        List<PrintingTypeBO> GetAllPrintingType();

        PrintingTypeBO GetPrintTypeByCode(string p);

        List<ManufactureRequestBO> GetManufactureRequests(ManufactureRequestSearch manuSearchObj);
    }
}
