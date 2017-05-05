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
    public partial class rpt_khlc : System.Web.UI.Page
    {
     
               public static string sel_string = "SELECT * FROM [dzsw].[dbo].[Syl_AppraiseInfo]";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        SqlDataReader dr;
        public static string lb;
        ReportDataSource res = new ReportDataSource();
        Check ck = new Check();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ck.item("考核流程报表", 5))
            {
                if (!IsPostBack)
                {

                    tbx_bg_date.Text = DateTime.Now.Date.AddMonths(-1).ToShortDateString();
                    tbx_ed_date.Text = DateTime.Now.Date.AddDays(1).ToShortDateString();
                    btn_cx_Click(sender, e);
                    this.Page.Visible = true;
                }
            }
            else
            {
                btn_exit_Click(sender, e);
                this.Page.Visible = false;

                throw new Exception("你没权限使用该模块功能");
            }
        }

        protected void btn_bg_date_Click(object sender, EventArgs e)
        {

            pnl_bg_date.Visible = true;

        }

        protected void btn_ed_time_Click(object sender, EventArgs e)
        {
            pnl_ed_date.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //设置开始时间
            pnl_bg_date.Visible = false;
            tbx_bg_date.Text = cld_bg_date.SelectedDate.Date.ToShortDateString();
            btn_cx_Click(sender, e);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //设置结束时间
            pnl_ed_date.Visible = false;
            tbx_ed_date.Text = cld_ed_date.SelectedDate.Date.ToShortDateString();
            btn_cx_Click(sender, e);
        }



        protected void btn_cx_Click(object sender, EventArgs e)
        {
            string lclb = "";//类别
            string lcjb = "";//类别
            string lczt = "";//状态
            string lc_banbie = "";//班别 
            if (ddl_lcjb.Text != "全部")
                lcjb = " and Applevel='" + ddl_lcjb.Text + "'";


            if (ddl_lclb.Text != "全部")
                lclb = " and AppKind='" + ddl_lclb.Text + "'";

            switch (ddl_lczt.Text)
            {
                case "生效":
                    {
                        lczt = "  and Flow_State ='" + ddl_lczt.Text + "'";
                        break;
                    }
                case "其它":
                    {
                        lczt = "  and Flow_State<> '生效'";
                        break;
                    }
            }




            if (ddl_banbie.Text != "全部")
                lc_banbie = " and AppGroup='" + ddl_banbie.Text + "'";


            sel_string = "SELECT [id],[AppID] ,[Flow_State],[ApplicantName],[ApplicantIDCard],[Applevel],[AppKind],[AppAmount],[TC_DateTime],[FS_DateTime],[AppGroup],[AppNames],[AppContent],[AppBy],[Step_1_Oponion],[Step_2_Oponion],[Step_3_Oponion],[Step_4_Oponion],[Step_5_Oponion] ,[Admin_Opt_Comment]  from [dzsw].[dbo].[Syl_AppraiseInfo] WHERE FS_DateTime BETWEEN '"
                + tbx_bg_date.Text.Trim() + "' and dateadd(day,1,convert(datetime, '" + tbx_ed_date.Text.Trim() + "')) "
                + lczt + lcjb + lclb + lc_banbie
                + " order by TC_DateTime ,AppGroup,Applevel,AppKind";

            SqlDataSource1.SelectCommand = sel_string;




            ReportViewer1.LocalReport.DataSources.Clear();
            string path = Path.Combine(Server.MapPath(@"\"), "rpt_kh_lc.rdlc");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = path;
            ReportDataSource rptDataSource = new ReportDataSource("DataSet1", SqlDataSource1);
            ReportViewer1.LocalReport.DataSources.Add(rptDataSource);

            ReportViewer1.LocalReport.Refresh();

        }



        protected void ddl_lczt_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_cx_Click(sender, e);
        }

        protected void ddl_lclb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_cx_Click(sender, e);
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void btn_exit_Click(object sender, EventArgs e)
        {

            Session["UserID"] = "";
            Session["UserName"] = "";
            Session["UserRName"] = "";
            Session["UserRule"] = "";

            Response.Redirect("login.aspx");
        }

        protected void ddl_lcjb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_cx_Click(sender, e);
        }
    }
}
    