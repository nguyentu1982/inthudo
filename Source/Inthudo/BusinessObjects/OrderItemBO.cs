using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order Detail business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign key mapping
    public enum OrderItemStatusEnum
    {
        OrderNotExist = 0,
        DesignRequestNotCreated = 1,
        DesignRequestCreated = 2,
        Designing = 3,
        DesignCopmleted = 4,
        DesignApprovedByCustomer = 5,
        ManufactureRequestCreated = 6,
        Manufacturing = 7,
        ManufactureCompleted = 8,
        CustomerApproved=9,
        CustomerRefused=10,
        Overdue=11
    }

    public class OrderItemlBO : BusinessObject
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public int OrderId { get; set; }
        public bool IsCustomerHasDesign { get; set; }
        public int PrintingTypeId { get; set; }

        public virtual MemberBO User { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual ProductBO Product { get; set; }
        public virtual OrderBO Order { get; set; }

        public string ProductName
        {
            get;
            set;
        }

        public OrderItemStatusEnum OrderItemStatus
        {
            get;
            set;
        }

        public string OrderItemStatusString
        {
            get
            {
                switch (OrderItemStatus)
                {
                    case OrderItemStatusEnum.OrderNotExist:
                        return "Đơn hàng không tồn tại / Chưa hoàn thành";
                    case OrderItemStatusEnum.DesignRequestNotCreated:
                        return "Chưa tạo yêu cầu thiết kế";
                    case OrderItemStatusEnum.DesignRequestCreated:
                        return "Đã tạo yêu cầu thiết kế";
                    case OrderItemStatusEnum.Designing:
                        return "Đang thiết kế";
                    case OrderItemStatusEnum.DesignCopmleted:
                        return "Đã thiết kế xong";
                    case OrderItemStatusEnum.DesignApprovedByCustomer:
                        return "Khách hàng đã duyệt mẫu thiết kế";
                    case OrderItemStatusEnum.ManufactureRequestCreated:
                        return "Yêu cầu sản xuất đã tạo";
                    case OrderItemStatusEnum.Manufacturing:
                        return "Đang sản xuất";
                    case OrderItemStatusEnum.ManufactureCompleted:
                        return "Đã sản xuất xong";
                    case OrderItemStatusEnum.CustomerApproved:
                        return "Khách hàng đã duyệt sản phẩm";
                    case OrderItemStatusEnum.CustomerRefused:
                        return "Lỗi - Khách hàng từ chối";
                    case OrderItemStatusEnum.Overdue:
                        return "Quá hạn";
                    default: return "Đơn hàng không tồn tại / Chưa hoàn thành";
                }
            }
        }

    }
}
