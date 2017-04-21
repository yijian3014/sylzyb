using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;


namespace sylzyb_employer_mgr
{
    public partial class Report : System.Web.UI.Page
    {
        protected void btn_rpt_khlc_Click(object sender, EventArgs e)
        {
            pnl_khlc.Visible = true;
            pnl_khzj.Visible = false;
        }

        protected void btn_rpt_khzj_Click(object sender, EventArgs e)
        {
            pnl_khlc.Visible = false;
            pnl_khzj.Visible = true;
        }

        protected void bgn_rpt_jiangjin_Click(object sender, EventArgs e)
        {
            
        }
    }
}