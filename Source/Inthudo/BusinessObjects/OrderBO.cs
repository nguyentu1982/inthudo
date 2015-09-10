using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{

    public enum OrderStatusEnum
    {
        NotCompleted=1,
        Completed=2,
        IsFailed =3,
        Overdue=4,
    }
    
    public class OrderBO : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int OrderId { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<int> DepositTypeId { get; set; }
        public Nullable<int> ShippingMethodId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int BusinessManId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> ExpectedCompleteDate { get; set; }
        public string Note { get; set; }
        public string DeliveryAddress { get; set; }
        public Nullable<bool> ApprovedByCustomer { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<decimal> VAT { get; set; }

        public virtual CustomerBO Customer { get; set; }
        public virtual ICollection<DesignRequestBO> DesignRequests { get; set; }
        public virtual MemberBO BusinessMan { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual MemberBO User2 { get; set; }
        public virtual DepositMethodBO DepositType { get; set; }
        public virtual ShippingMethodBO ShippingMethod { get; set; }
        public virtual IList<OrderItemlBO> OrderItems { get; set; }


        

        public decimal OrderTotal
        {
            get;
            set;
        }

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

        public OrderStatusEnum OrderStatus
        {
            get;
            set;
        }

        public string OrderStatusString
        {
            get {
                switch (OrderStatus)
                {
                    case OrderStatusEnum.Completed:
                        return "Hoàn thành";
                    case OrderStatusEnum.NotCompleted:
                        return "Chưa hoàn thành";
                    case OrderStatusEnum.IsFailed:
                        return "Đơn hàng có lỗi";
                    case OrderStatusEnum.Overdue:
                        return "Quá hạn";
                    default : return "Chưa hoàn thành";
                }
            }
        }
    }

        
}
