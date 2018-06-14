using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Coolite.Ext.Web;

namespace WSS.Web.Admin.GameTool
{
    public partial class GameConfigTools : System.Web.UI.Page
    {
        protected WebServiceXLJ.WebServiceGame xlj = new WebServiceXLJ.WebServiceGame();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Ext.IsAjaxRequest)
            //{
            //    string jsons = xlj.GetBattleZones();

            //    StoreBattleZone.LoadData(jsons);
            //}

        }

    }
}
