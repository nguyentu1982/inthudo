using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constant
    {
        public const string PTK = "PTK";
        public const string PKD = "PKD";

        public const string USER_ROLE_NAME = "User";
        public const string ADMIN_ROLE_NAME = "Admin";
        public const string SUPPERVISOR_ROLE_NAME = "Suppervisor";

        public const string QUERY_STRING_CUST_ID = "CustId";

        public class Setting
        {
            public const string Not_Allow_Select_Business_Man_When_CreateOrder = "NotAllowSelectBusinessManWhenCreateOrder";
            public const string Not_Allow_Other_User_Edit_Order = "NotAllowOtherUserEditOrder";
            public const string Allow_Designer_Input_Approved_By_Customer_Info = "AllowDesignerInputApprovedByCustomerInfo";
        }

        public class Customer
        {
            public const string KH = "KH";
            public const string DVSX = "DVSX";
            public const string QUERY_STRING_CUSTOMER_TYPE = "CustType";
        }
    }
}
