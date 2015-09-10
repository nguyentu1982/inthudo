using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public enum DesignRequestStatusEnum
    {
        NotExist =0,
        DesignRequestCreated = 1,
        Designing = 2,
        DesignCopmleted = 3,
        DesignApprovedByCustomer = 4,
        DesignNotApproved=5
    }

    public class DesignRequestBO :BusinessObject
    {
        public int DesignRequestId { get; set; }
        public string Description { get; set; }
        public Nullable<int> DesignerId { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int OrderId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public int OrderItemId { get; set; }
        public Nullable<bool> ApprovedByCustomer { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }

        public virtual OrderItemlBO OrderItem { get; set; }
        public virtual MemberBO User { get; set; }
        public virtual MemberBO User1 { get; set; }
        public virtual MemberBO User2 { get; set; }

        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public string OrderDetailStatusString {
            get {
                return OrderItem.OrderItemStatusString;
            }
        }

        public DesignRequestStatusEnum DesignRequestStatus
        {
            get { 
                DesignRequestStatusEnum status = DesignRequestStatusEnum.NotExist;
                
                if(!BeginDate.HasValue && !EndDate.HasValue)
                {
                    status = DesignRequestStatusEnum.DesignRequestCreated;
                }

                if(BeginDate.HasValue && !EndDate.HasValue)
                {
                    status = DesignRequestStatusEnum.Designing;
                }

                if(BeginDate.HasValue && EndDate.HasValue && (ApprovedByCustomer == null) )
                {
                    status = DesignRequestStatusEnum.DesignCopmleted;
                }

                if(BeginDate.HasValue && EndDate.HasValue && ApprovedByCustomer == true )
                {
                    status = DesignRequestStatusEnum.DesignApprovedByCustomer;
                }

                if (BeginDate.HasValue && !EndDate.HasValue && (ApprovedByCustomer == false))
                {
                    status = DesignRequestStatusEnum.DesignNotApproved;
                }
                return status;
            }
        }

        public string DesignRequestStatusString
        {
            get
            { 
                string result = "Trạng thái không tồn tại";

                switch (DesignRequestStatus)
                { 
                    case DesignRequestStatusEnum.DesignRequestCreated:
                        return "Yêu cầu mới";
                    case DesignRequestStatusEnum.Designing:
                        return "Đang thiết kế";
                    case DesignRequestStatusEnum.DesignCopmleted:
                        return "Đã thiết kế xong";
                    case DesignRequestStatusEnum.DesignApprovedByCustomer:
                        return "Khách hàng đã duyệt mẫu TK";
                    case DesignRequestStatusEnum.DesignNotApproved:
                        return "Khách hàng KHÔNG duyệt mẫu TK";
                }

                return result;
            }
        }
    }
}
