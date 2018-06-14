using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebWSS.Common
{
    public partial class ControlChartSelect : System.Web.UI.UserControl
    {
        private bool _useline = true;
        public bool UseLine
        {
            get { return _useline; }
            set { _useline = value; }
        }

        private bool _usecol = true;
        public bool UseCol
        {
            get { return _usecol; }
            set { _usecol = value; }
        }
        private bool _usepie = true;
        public bool UsePie
        {
            get { return _usepie; }
            set { _usepie = value; }
        }
        private bool _usearea = true;
        public bool UseArea
        {
            get { return _usearea; }
            set { _usearea = value; }
        }



        private static int state = 0;
        public int State
        {
            get { return state; }
            set { state = value; }
        }


        public event EventHandler SelectChanged;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitBtn();
            if (!IsPostBack)
            {
                state = 0;
            }

        }

        private void InitBtn()
        {
            btnchartselectLine.Visible = _useline;
            btnchartselectcol.Visible = _usecol;
            btnchartselectpie.Visible = _usepie;
            btnchartselectarea.Visible = _usearea;
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                btnchartselectData.Enabled = true;
                btnchartselectLine.Enabled = true;
                btnchartselectcol.Enabled = true;
                btnchartselectpie.Enabled = true;
                btnchartselectarea.Enabled = true;

                btnchartselectData.CssClass = "buttonbl";
                btnchartselectLine.CssClass = "buttonbl";
                btnchartselectcol.CssClass = "buttonbl";
                btnchartselectpie.CssClass = "buttonbl";
                btnchartselectarea.CssClass = "buttonbl";

                Button btn = (Button)sender;
                btn.Enabled = false;
                btn.CssClass = "buttonblo";

                switch (btn.ID)
                {
                    case "btnchartselectData":
                        state = 0;
                        break;
                    case "btnchartselectLine":
                        state = 1;
                        break;
                    case "btnchartselectcol":
                        state = 2;
                        break;
                    case "btnchartselectpie":
                        state = 3;
                        break;
                    case "btnchartselectarea":
                        state = 4;
                        break;
                    default:
                        state = 0;
                        break;
                }

                SelectChanged(this, new EventArgs());
            }
            catch (System.Exception ex)
            {
            }
        }




    }
}