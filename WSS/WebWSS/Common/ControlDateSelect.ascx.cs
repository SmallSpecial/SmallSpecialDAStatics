using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebWSS.Common
{
    public partial class ControlDateSelect : System.Web.UI.UserControl
    {
        private static DateTime _selectdate = DateTime.Now;

        /// <summary>
        /// 选中的时间
        /// </summary>
        public DateTime SelectDate
        {
            get { return _selectdate; }
            set { _selectdate = value; }
        }

        public event EventHandler SelectDateChanged;
        protected void Page_Load(object sender, EventArgs e)
        {
            //加入当月日期按钮控件
            for (int i = 0; i <= 32; i++)
            {
                Button btn = new Button();
                btn.ID = "btndateselect" + i;
                btn.Text = i.ToString().PadLeft(2, '0').Replace("00", "<<").Replace("32", ">>");
                btn.Click += new EventHandler(btn_Click);
                DateSelect.Controls.Add(btn);
            }
            if (!IsPostBack)
            {
                _selectdate = DateTime.Now;
                SetSelectDate();
            }
            
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string date = Convert.ToDateTime(_selectdate).ToString("yyyy-MM-") + btn.Text;
                if (date.IndexOf("<<") >= 0)
                {
                    date = Convert.ToDateTime(date.Replace("<<", "01")).AddDays(-1).ToString("yyyy-MM-dd");
                }
                if (date.IndexOf(">>") >= 0)
                {
                    date = Convert.ToDateTime(date.Replace(">>", "01")).AddMonths(1).ToString("yyyy-MM-dd");
                }
                _selectdate = Convert.ToDateTime(date);
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
            string dates = _selectdate.ToString("yyyy-MM-01");
            DateTime dtb = Convert.ToDateTime(dates);
            DateTime dte = dtb.AddMonths(1).AddDays(-1);
            for (int i = 0; i <= 32; i++)
            {
                Control ctl = DateSelect.FindControl("btndateselect" + i);
                if (ctl != null && ctl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctl;
                    if (dtb.AddDays(i - 1) <= DateTime.Now && (i <= dte.Day || i == 32))
                    {
                        btn.Visible = true;
                        if (btn.Text == _selectdate.ToString("dd"))
                        {
                            btn.CssClass = "buttonblueo";
                            btn.Enabled = false;
                        }
                        else
                        {
                            btn.CssClass = "buttonblue";
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
        public void SetSelectDate(object value)
        {
            try
            {
                _selectdate = Convert.ToDateTime(value);
                SetSelectDate();
            }
            catch (System.Exception ex)
            {
            	
            }
        }
    }
}