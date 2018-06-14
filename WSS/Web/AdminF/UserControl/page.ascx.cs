using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WSS.Web.AdminF.UserControl
{
    public partial class page : System.Web.UI.UserControl
    {
  private string mode = "normal";

    public event EventHandler PageChanged;

    public page()
    {
        this.ViewState["CurPage"] = 1;
    }

    protected void btnToPage_Click(object sender, EventArgs e)
    {
        int result = 0;
        int.TryParse(this.txtToPage.Text.Trim(), out result);
        if (result > 0)
        {
            if (result > this.PageTotal)
            {
                result = this.PageTotal;
            }
            if (result != this.CurPage)
            {
                this.ViewState["CurPage"] = result;
                this.CurPage = result;
                this.PagerBind();
                this.OnPageChanged(sender, e);
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.Load += new System.EventHandler(this.Page_Load);
        this.btn_pagefirst.Click += new System.EventHandler(this.PageButtonClick);
        this.btn_pageprev.Click += new System.EventHandler(this.PageButtonClick);
        this.btn_pagenext.Click += new System.EventHandler(this.PageButtonClick);
        this.btn_pagelast.Click += new System.EventHandler(this.PageButtonClick);
        this.txtToPage.Attributes.Add("onkeypress", "if(event.keyCode==13)btnToPage.onclick();");
        base.OnInit(e);
    }

    protected virtual void OnPageChanged(object sender, EventArgs e)
    {
        if (this.PageChanged != null)
        {
            this.PageChanged(sender, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.PagerBind();
        }
    }

    private void PageButtonClick(object sender, EventArgs e)
    {
        string commandName = ((LinkButton) sender).CommandName;
        if (commandName != null)
        {
            if (!(commandName == "next"))
            {
                if (commandName == "previous")
                {
                    this.ViewState["CurPage"] = ((int) this.ViewState["CurPage"]) - 1;
                    goto Label_00DE;
                }
                if (commandName == "last")
                {
                    this.ViewState["CurPage"] = (int) this.ViewState["PageTotal"];
                    goto Label_00DE;
                }
            }
            else
            {
                this.ViewState["CurPage"] = ((int) this.ViewState["CurPage"]) + 1;
                goto Label_00DE;
            }
        }
        this.ViewState["CurPage"] = 1;
    Label_00DE:
        this.PagerBind();
        this.OnPageChanged(sender, e);
    }

    private void PagerBind()
    {
        this.PageTotal = ((this.RecTotal % this.PageSize) > 0) ? ((this.RecTotal / this.PageSize) + 1) : (this.RecTotal / this.PageSize);
        if (this.mode.Equals("normal"))
        {
            this.lbl_PageInfo.Text = "共有记录&nbsp;" + this.RecTotal.ToString() + "&nbsp;条&nbsp;每页&nbsp;" + this.PageSize.ToString() + "&nbsp;条";
        }
        else
        {
            this.lbl_PageInfo.Text = "" + this.RecTotal.ToString() + "条";
        }
        if (this.CurPage > this.PageTotal)
        {
            this.CurPage--;
        }
        this.ViewState["CurrPage"] = (int) this.ViewState["CurPage"];
        this.txtToPage.Text = this.CurPage.ToString();
        this.btn_pagefirst.Enabled = true;
        this.btn_pageprev.Enabled = true;
        this.btn_pagenext.Enabled = true;
        this.btn_pagelast.Enabled = true;
        if (this.CurPage == 1)
        {
            this.btn_pagefirst.Enabled = false;
            this.btn_pageprev.Enabled = false;
            this.btn_pagenext.Enabled = true;
            this.btn_pagelast.Enabled = true;
        }
        if (this.CurPage == this.PageTotal)
        {
            this.btn_pagefirst.Enabled = true;
            this.btn_pageprev.Enabled = true;
            this.btn_pagenext.Enabled = false;
            this.btn_pagelast.Enabled = false;
        }
        if ((this.PageTotal - 1) <= 0)
        {
            this.btn_pagefirst.Enabled = false;
            this.btn_pageprev.Enabled = false;
            this.btn_pagenext.Enabled = false;
            this.btn_pagelast.Enabled = false;
        }
        if (this.mode.Equals("simple"))
        {
            this.btn_pagefirst.Text = "|<";
            this.btn_pageprev.Text = "<";
            this.btn_pagenext.Text = ">";
            this.btn_pagelast.Text = ">|";
        }
    }

    public int CurPage
    {
        get
        {
            int num = 0;
            if (this.ViewState["CurPage"] != null)
            {
                num = (int) this.ViewState["CurPage"];
            }
            if (num <= 0)
            {
                return 1;
            }
            return num;
        }
        set
        {
            this.ViewState["CurPage"] = value;
        }
    }

    public string Mode
    {
        get
        {
            return this.mode;
        }
        set
        {
            this.mode = value;
        }
    }

    public int PageSize
    {
        get
        {
            if (this.ViewState["PageSize"] == null)
            {
                return 1;
            }
            return (int) this.ViewState["PageSize"];
        }
        set
        {
            this.ViewState["PageSize"] = value;
        }
    }

    private int PageTotal
    {
        get
        {
            if (this.ViewState["PageTotal"] == null)
            {
                return 0;
            }
            return (int) this.ViewState["PageTotal"];
        }
        set
        {
            this.ViewState["PageTotal"] = value;
        }
    }

    public int RecTotal
    {
        get
        {
            if (this.ViewState["RecTotal"] == null)
            {
                return 0;
            }
            return (int) this.ViewState["RecTotal"];
        }
        set
        {
            this.ViewState["RecTotal"] = value;
            this.PagerBind();
        }
    }

    public delegate void EventHandler(object sender, EventArgs e);
    }
}