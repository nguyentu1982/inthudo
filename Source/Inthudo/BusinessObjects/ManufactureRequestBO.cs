using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public enum ManufactureRequestStatusEnum
    {
        NotExist = 0,
        ManufactureRequestCreated = 1,
        Manufacturing = 2,
        ManufactureCopmleted = 3,
        ApprovedByCustomer = 4,
        NotApproved = 5
    }

    public class ManufactureRequestBO
    {
        public int ManufactureRequestId { get; set; }
        public int DesignRequestId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public int Quantity { get; set; }
        public Nullable<bool> CustomerApproved { get; set; }
        public Nullable<System.DateTime> CustomerApprovedDate { get; set; }
        public string Note { get; set; }
        public Nullable<int> CustomerApprovedQuantity { get; set; }
        public Nullable<decimal> CustomerApprovedPrice { get; set; }
        public Nullable<bool> IsFailed { get; set; }
        public int ManufactureId { get; set; }

        public virtual DesignRequestBO DesignRequest { get; set; }
        public virtual MemberBO Member { get; set; }
        public virtual MemberBO Member1 { get; set; }
        public virtual CustomerBO Manufacture { get; set; }

        public ManufactureRequestStatusEnum ManufactureRequestStatus
        {
            get {
                ManufactureRequestStatusEnum status = ManufactureRequestStatusEnum.NotExist;

                if (!BeginDate.HasValue && !EndDate.HasValue)
                {
                    status = ManufactureRequestStatusEnum.ManufactureRequestCreated;
                }

                if (BeginDate.HasValue && !EndDate.HasValue)
                {
                    status = ManufactureRequestStatusEnum.Manufacturing;
                }

                if (BeginDate.HasValue && EndDate.HasValue && (CustomerApproved == null || CustomerApproved == false))
                {
                    status = ManufactureRequestStatusEnum.ManufactureCopmleted;
                }

                if (BeginDate.HasValue && EndDate.HasValue && CustomerApproved == true)
                {
                    status = ManufactureRequestStatusEnum.ApprovedByCustomer;
                }

                if (BeginDate.HasValue && EndDate.HasValue && IsFailed==true)
                {
                    status = ManufactureRequestStatusEnum.NotApproved;
                }
                return status;
            
            }
        }

        public string ManufactureRequestStatusString
        {
            get
            {
                string result = "Trạng thái không tồn tại";

                switch (ManufactureRequestStatus)
                {
                    case ManufactureRequestStatusEnum.ManufactureRequestCreated:
                        return "Yêu cầu mới";
                    case ManufactureRequestStatusEnum.Manufacturing:
                        return "Đang sản xuất";
                    case ManufactureRequestStatusEnum.ManufactureCopmleted:
                        return "Sản xuất xong";
                    case ManufactureRequestStatusEnum.ApprovedByCustomer:
                        return "Khách hàng đã duyệt sản phẩm";
                    case ManufactureRequestStatusEnum.NotApproved:
                        return "Khách hàng KHÔNG duyệt sản phẩm";
                }

                return result;
            }
        }

        public string ProductName { get; set; }
        public string PrintingTypeName { get; set; }
        
        public string BusinessMan { get; set; }
        public int OrderId { get; set; }
        public string CustomterName { get; set; }
        public string ManufactureName
        {
            get
            {
                return Manufacture.Name;
            }

        }
    }
}
