using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign Key Mapping.
    
    public class OrderBO : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int OrderId { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> DepositTypeId { get; set; }
        public Nullable<int> ShippingMethodId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
        public Nullable<bool> Deleted { get; set; }

        public virtual CustomerBO Customer { get; set; }
        public virtual ICollection<DesignRequestBO> DesignRequests { get; set; }
        public virtual MemberBO BusinessMan { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual MemberBO User2 { get; set; }
        public virtual DepositMethodBO DepositType { get; set; }
        public virtual ShippingMethodBO ShippingMethod { get; set; }
        public virtual IList<OrderDetailBO> OrderItems { get; set; }

        public string CustomerName
        {
            get;
            set;
        }
        
        public string BusinessManName
        {
            get;
            set;
        }

        public string DepositTypeName
        {
            get;
            set;
        }

        public string ShippingMethodName
        {
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }

        public OrderStatusEnum OrderStatus
        {
            get;
            set;
        }

        public string OrderStatusString
        {
            get
            {
                switch (OrderStatus)
                { 
                    case OrderStatusEnum.OrderNotExist:
                        return "Đơn hàng không tồn tại / Chưa hoàn thành";                        
                    case OrderStatusEnum.OrderCreated:
                        return "Đơn hàng đã tạo";
                    case OrderStatusEnum.DesignRequestCreated :
                        return "Yêu cầu thiết kế đã tạo";
                    case OrderStatusEnum.Designing:
                        return "Đang thiết kế";
                    case OrderStatusEnum.DesignCopmleted:
                        return "Đã thiết kế xong";
                    case OrderStatusEnum.ManufactureRequestCreated:
                        return "Yêu cầu sản xuất đã tạo";
                    case OrderStatusEnum.Manufacturing:
                        return "Đang sản xuất";
                    case OrderStatusEnum.ManufactureCompleted:
                        return "Đã sản xuất xong";
                    default: return "Đơn hàng không tồn tại / Chưa hoàn thành";
                }
            }
        }
    }

    public enum OrderStatusEnum
    {
        OrderNotExist=0,
        OrderCreated = 1,
        DesignRequestCreated = 2,
        Designing = 3,
        DesignCopmleted = 4,
        ManufactureRequestCreated =5,
        Manufacturing = 6,
        ManufactureCompleted = 7,
    }
}
