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
        Check ck = new Check();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ck.Module("报表", 0))
                {
                    if (!IsPostBack)
                    {
                        pnl_khlc.Visible = true;
                        pnl_khzj.Visible = false;
                        pnl_jiangjin.Visible = false;
                    }
                    //此处应加入IFRAME内容的刷新动作。
                }
                else
                {
                    btn_exit_Click(sender, e);
                    throw new Exception("你没有权限进入该模块");
                }

            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + err.Message + "');</script>");

            }
        }
        protected void btn_rpt_khlc_Click(object sender, EventArgs e)
        {
            pnl_khlc.Visible = true;
            pnl_khzj.Visible = false;
            pnl_jiangjin.Visible = false;
        }

        protected void btn_rpt_khzj_Click(object sender, EventArgs e)
        {
            pnl_khlc.Visible = false;
            pnl_khzj.Visible = true;
            pnl_jiangjin.Visible = false;
        }

        protected void bgn_rpt_jiangjin_Click(object sender, EventArgs e)
        {
            pnl_khlc.Visible = false;
            pnl_khzj.Visible = false;
            pnl_jiangjin.Visible = true;
        }

        protected void btn_exit_Click(object sender, EventArgs e)
        {
            Session["UserID"] = "";
            Session["UserName"] = "";
            Session["UserRName"] = "";
            Session["UserRule"] = "";
            Response.Redirect("login.aspx");
        }

    }
}