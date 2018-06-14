using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebWSS.Common
{
    public partial class ControlMonthSelect : System.Web.UI.UserControl
    {
        private static DateTime _selectdateb = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));

        private static DateTime _selectdatee = _selectdateb.AddMonths(1).AddDays(-1);

        /// <summary>
        /// 选中的时间,开始
        /// </summary>
        public DateTime SelectDateB
        {
            get { return _selectdateb; }
            set { _selectdateb = value; }
        }

        /// <summary>
        /// 选中的时间,结束
        /// </summary>
        public DateTime SelectDateE
        {
            get { return _selectdatee; }
            set { _selectdatee = value; }
        }



        public event EventHandler SelectDateChanged;
        protected void Page_Load(object sender, EventArgs e)
        {
            //加入月份按钮控件
            for (int i = 0; i <= 13; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
            if (!IsPostBack)
            {
                _selectdateb = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                _selectdatee = _selectdateb.AddMonths(1) > DateTime.Now ? DateTime.Now : _selectdateb.AddMonths(1).AddDays(-1);
                SetSelectDate();
            }

        }


        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                _selectdateb = Convert.ToDateTime(btn.Text);
                _selectdatee = _selectdateb.AddMonths(1) > DateTime.Now ? DateTime.Now : _selectdateb.AddMonths(1).AddDays(-1);

                SetSelectDate();
                SelectDateChanged(this, new EventArgs());
            }
            catch (System.Exception ex)
            {
            }
        }

        //设置日期按钮状态
        protected void SetSelectDate()
        {
            string dates = _selectdateb.ToString("yyyy-01-01");
            DateTime dtb = Convert.ToDateTime(dates);

            for (int i = 0; i <= 13; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    if (dtb.AddMonths(i - 1) <= DateTime.Now)
                    {
                        btn.Text = dtb.AddMonths(i - 1).ToString("yyyy-MM");
                        btn.Visible = true;

                        DateTime dttb = Convert.ToDateTime(btn.Text);
                        DateTime dtte = dttb.AddMonths(1) > DateTime.Now ? DateTime.Now : dttb.AddMonths(1).AddDays(-1);

                        if (dttb.ToString("yyyy-MM-dd") == _selectdateb.ToString("yyyy-MM-dd") && dtte.ToString("yyyy-MM-dd") == _selectdatee.ToString("yyyy-MM-dd"))
                        {
                            btn.CssClass = "buttonblo";
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.CssClass = "buttonbl";
                            btn.Enabled = true;
                        }
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
            }
        }

        public void SetSelectDate(object valueb,object valuee)
        {
            try
            {
                _selectdateb = Convert.ToDateTime(valueb);
                _selectdatee = Convert.ToDateTime(valuee);
                SetSelectDate();
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}