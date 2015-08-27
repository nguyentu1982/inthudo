using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order Detail business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign key mapping
    public enum OrderDetailStatusEnum
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
    }

    public class OrderDetailBO : BusinessObject
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
        public int? DesignerId { get; set; }

        public virtual MemberBO User { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual ProductBO Product { get; set; }
        public virtual OrderBO Order { get; set; }

        public string ProductName
        {
            get;
            set;
        }

        public OrderDetailStatusEnum OrderDetailStatus
        {
            get;
            set;
        }

        public string OrderDetailStatusString
        {
            get
            {
                switch (OrderDetailStatus)
                {
                    case OrderDetailStatusEnum.OrderNotExist:
                        return "Đơn hàng không tồn tại / Chưa hoàn thành";
                    case OrderDetailStatusEnum.DesignRequestNotCreated:
                        return "Chưa tạo yêu cầu thiết kế";
                    case OrderDetailStatusEnum.DesignRequestCreated:
                        return "Đã tạo yêu cầu thiết kế";
                    case OrderDetailStatusEnum.Designing:
                        return "Đang thiết kế";
                    case OrderDetailStatusEnum.DesignCopmleted:
                        return "Đã thiết kế xong";
                    case OrderDetailStatusEnum.DesignApprovedByCustomer:
                        return "Khách hàng đã duyệt mẫu thiết kế";
                    case OrderDetailStatusEnum.ManufactureRequestCreated:
                        return "Yêu cầu sản xuất đã tạo";
                    case OrderDetailStatusEnum.Manufacturing:
                        return "Đang sản xuất";
                    case OrderDetailStatusEnum.ManufactureCompleted:
                        return "Đã sản xuất xong";
                    case OrderDetailStatusEnum.CustomerApproved:
                        return "Khách hàng đã duyệt sản phẩm";
                    default: return "Đơn hàng không tồn tại / Chưa hoàn thành";
                }
            }
        }
    }
}
