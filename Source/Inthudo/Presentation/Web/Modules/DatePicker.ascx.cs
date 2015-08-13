﻿using System;

namespace Web.Modules
{
    public partial class DatePicker : BaseUserControl
    {
        #region Properties
        public bool ShowTime
        {
            get
            {
                return Convert.ToBoolean(ViewState["ShowTime"]);
            }
            set
            {
                ViewState["ShowTime"] = value;
            }
        }

        public DateTime? SelectedDate
        {
            get
            {
                DateTime inputDate;
                if (!DateTime.TryParse(txtDateTime.Text, out inputDate))
                {
                    return null;
                }
                return inputDate;
            }
            set
            {
                ajaxCalendar.SelectedDate = value;
            }
        }

        public string Format
        {
            get
            {
                return ajaxCalendar.Format;
            }
            set
            {
                ajaxCalendar.Format = value;
            }
        }
        #endregion
    }
}