using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

namespace Web.Modules
{
    public partial class PrintingTypeSelect : BaseUserControl
    {
        public List<int> SelectedValues
        {
            get
            {
                List<int> result = new List<int>();
                foreach (ListItem i in cblPrintingType.Items)
                {
                    if (i.Selected)
                        result.Add(int.Parse(i.Value));
                }
                return result;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            cblPrintingType.Attributes.Add("onclick", "selectPrintingType(event)");

            List<PrintingTypeBO> printTypes = this.OrderService.GetAllPrintingType();
            cblPrintingType.Items.Clear();
            ListItem itemAll = new ListItem("Tất cả", "0");
            itemAll.Selected = true;
            cblPrintingType.Items.Add(itemAll);
            foreach (PrintingTypeBO p in printTypes)
            {
                ListItem i = new ListItem(p.Name, p.Id.ToString());
                i.Selected = true;
                cblPrintingType.Items.Add(i);
            }

        }
    }
}