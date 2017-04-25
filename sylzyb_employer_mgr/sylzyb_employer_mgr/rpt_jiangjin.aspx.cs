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
using System.Threading;

namespace sylzyb_employer_mgr
{
    public partial class rpt_jiangjin : System.Web.UI.Page
    {

        public static string sel_string1 = "SELECT * FROM[dzsw].[dbo].[Syl_Bonus_Group]";
        public static string sel_string2 = "SELECT * FROM [dzsw].[dbo].[Syl_Bonus_Person]";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        public DataSet ds2 = new DataSet();
        DataTable dt2 = new DataTable();
      
        public static string lb;
        ReportDataSource res = new ReportDataSource();
        Check ck = new Check();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ck.item("奖金报表", 5))
            {
                if (!IsPostBack)
                {

                    tbx_bg_date.Text = DateTime.Now.Date.AddMonths(-1).ToLongDateString();
                    tbx_ed_date.Text = DateTime.Now.Date.AddDays(1).ToLongDateString();
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
            tbx_bg_date.Text = cld_bg_date.SelectedDate.Date.ToLongDateString();
            btn_cx_Click(sender, e);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //设置结束时间
            pnl_ed_date.Visible = false;
            tbx_ed_date.Text = cld_ed_date.SelectedDate.Date.ToLongDateString();
            btn_cx_Click(sender, e);
        }



        protected void btn_cx_Click(object sender, EventArgs e)
        {
            string lc_banbie_g = "";
            string lc_banbie_p = "";//班别 
            string bgmonth;
            string edmonth;
            if (Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month < 10)
                bgmonth = Convert.ToDateTime(tbx_bg_date.Text.Trim()).Year.ToString() + '0' + Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month.ToString();
            else
                bgmonth = Convert.ToDateTime(tbx_bg_date.Text.Trim()).Year.ToString() + Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month.ToString();
            if (Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month < 10)
                edmonth = Convert.ToDateTime(tbx_ed_date.Text.Trim()).Year.ToString() + '0' + Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month.ToString();
            else
                edmonth = Convert.ToDateTime(tbx_ed_date.Text.Trim()).Year.ToString() + Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month.ToString();

            if (ddl_banbie.Text != "全部")
            {
                lc_banbie_p = " and [P_GroupName]='" + ddl_banbie.Text + "'";
                lc_banbie_g = " and [G_GroupName]='" + ddl_banbie.Text + "'";
            }

            sel_string1 = "SELECT [ID],[G_BonusDate],[OrderOfShow],[G_GroupName] ,[G_Coefficient],[G_BaseBonus] ,[G_DueBonus] ,[G_PlantApp],[G_DepartmentApp],[G_Other1] ,[G_Other2] ,[NumOfPeople],[G_ActualBonus] ,[AverageBonus] FROM[dzsw].[dbo].[Syl_Bonus_Group] WHERE [G_BonusDate] BETWEEN '"
                + bgmonth + "' AND '" + edmonth + "'"
               + lc_banbie_g
                + " order by [G_BonusDate] ,[G_GroupName],[OrderOfShow] asc";
 SqlDataSource1.SelectCommand = sel_string1;


           

            rv_banzhu_jiangjin.LocalReport.DataSources.Clear();
            string ban_path = Path.Combine(Server.MapPath(@"\"), "rpt_banzhu_jiangjin.rdlc");
            rv_banzhu_jiangjin.ProcessingMode = ProcessingMode.Local;
            rv_banzhu_jiangjin.LocalReport.ReportPath = ban_path;
            ReportDataSource rpt_banzhu_DataSource = new ReportDataSource("DataSet1", SqlDataSource1);
            rv_banzhu_jiangjin.LocalReport.DataSources.Add(rpt_banzhu_DataSource);
            rv_banzhu_jiangjin.LocalReport.Refresh();

       sel_string2 = "SELECT [ID],[P_BonusDate],[P_GroupName],[WorkerName],[IDCard],[P_Coefficient],[P_BaseBonus],[P_DueBonus],[P_PlantApp],[P_DepartmentApp],[P_GroupApp],[P_Other1],[P_Other2],[P_Other3],[P_Other4],[P_Other5],[DutyBonus],[P_ActualBonus] FROM [dzsw].[dbo].[Syl_Bonus_Person] WHERE [P_BonusDate] BETWEEN '"
          + bgmonth + "' AND '" + edmonth + "'"
          + lc_banbie_p
          + " order by [P_BonusDate] ,[WorkerName]";
           SqlDataSource2.SelectCommand = sel_string2;

        

    rv_geren_jiangjin.LocalReport.DataSources.Clear();
            string geren_path = Path.Combine(Server.MapPath(@"\"), "rpt_geren_jiangjin.rdlc");
            rv_geren_jiangjin.ProcessingMode = ProcessingMode.Local;
            rv_geren_jiangjin.LocalReport.ReportPath = geren_path;
            ReportDataSource rpt_genren_DataSource = new ReportDataSource("DataSet1", SqlDataSource2);
            rv_geren_jiangjin.LocalReport.DataSources.Add(rpt_genren_DataSource);
            rv_geren_jiangjin.LocalReport.Refresh();



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

        protected void btn_banzhu_Click(object sender, EventArgs e)
        {
            string lc_banbie_g = "";
            string lc_banbie_p = "";//班别 
            string bgmonth;
            string edmonth;
            if (Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month < 10)
                bgmonth = Convert.ToDateTime(tbx_bg_date.Text.Trim()).Year.ToString() + '0' + Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month.ToString();
            else
                bgmonth = Convert.ToDateTime(tbx_bg_date.Text.Trim()).Year.ToString() + Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month.ToString();
            if (Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month < 10)
                edmonth = Convert.ToDateTime(tbx_ed_date.Text.Trim()).Year.ToString() + '0' + Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month.ToString();
            else
                edmonth = Convert.ToDateTime(tbx_ed_date.Text.Trim()).Year.ToString() + Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month.ToString();

            if (ddl_banbie.Text != "全部")
            {
                lc_banbie_p = " and [P_GroupName]='" + ddl_banbie.Text + "'";
                lc_banbie_g = " and [G_GroupName]='" + ddl_banbie.Text + "'";
            }

            sel_string1 = "SELECT [ID],[G_BonusDate],[OrderOfShow],[G_GroupName] ,[G_Coefficient],[G_BaseBonus] ,[G_DueBonus] ,[G_PlantApp],[G_DepartmentApp],[G_Other1] ,[G_Other2] ,[NumOfPeople],[G_ActualBonus] ,[AverageBonus] FROM[dzsw].[dbo].[Syl_Bonus_Group] WHERE [G_BonusDate] BETWEEN '"
                + bgmonth + "' AND '" + edmonth + "'"
               + lc_banbie_g
                + " order by [G_BonusDate] ,[G_GroupName],[OrderOfShow] asc";
            SqlDataSource1.SelectCommand = sel_string1;




            rv_banzhu_jiangjin.LocalReport.DataSources.Clear();
            string ban_path = Path.Combine(Server.MapPath(@"\"), "rpt_banzhu_jiangjin.rdlc");
            rv_banzhu_jiangjin.ProcessingMode = ProcessingMode.Local;
            rv_banzhu_jiangjin.LocalReport.ReportPath = ban_path;
            ReportDataSource rpt_banzhu_DataSource = new ReportDataSource("DataSet1", SqlDataSource1);
            rv_banzhu_jiangjin.LocalReport.DataSources.Add(rpt_banzhu_DataSource);
            rv_banzhu_jiangjin.LocalReport.Refresh();
        }

        protected void btn_geren_Click(object sender, EventArgs e)
        {
            string lc_banbie_g = "";
            string lc_banbie_p = "";//班别 
            string bgmonth;
            string edmonth;
            if (Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month < 10)
                bgmonth = Convert.ToDateTime(tbx_bg_date.Text.Trim()).Year.ToString() + '0' + Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month.ToString();
            else
                bgmonth = Convert.ToDateTime(tbx_bg_date.Text.Trim()).Year.ToString() + Convert.ToDateTime(tbx_bg_date.Text.Trim()).Month.ToString();
            if (Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month < 10)
                edmonth = Convert.ToDateTime(tbx_ed_date.Text.Trim()).Year.ToString() + '0' + Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month.ToString();
            else
                edmonth = Convert.ToDateTime(tbx_ed_date.Text.Trim()).Year.ToString() + Convert.ToDateTime(tbx_ed_date.Text.Trim()).Month.ToString();

            if (ddl_banbie.Text != "全部")
            {
                lc_banbie_p = " and [P_GroupName]='" + ddl_banbie.Text + "'";
                lc_banbie_g = " and [G_GroupName]='" + ddl_banbie.Text + "'";
            }


            sel_string2 = "SELECT [ID],[P_BonusDate],[P_GroupName],[WorkerName],[IDCard],[P_Coefficient],[P_BaseBonus],[P_DueBonus],[P_PlantApp],[P_DepartmentApp],[P_GroupApp],[P_Other1],[P_Other2],[P_Other3],[P_Other4],[P_Other5],[DutyBonus],[P_ActualBonus] FROM [dzsw].[dbo].[Syl_Bonus_Person] WHERE [P_BonusDate] BETWEEN '"
               + bgmonth + "' AND '" + edmonth + "'"
               + lc_banbie_p
               + " order by [P_BonusDate] ,[WorkerName]";
            SqlDataSource2.SelectCommand = sel_string2;



            rv_geren_jiangjin.LocalReport.DataSources.Clear();
            string geren_path = Path.Combine(Server.MapPath(@"\"), "rpt_geren_jiangjin.rdlc");
            rv_geren_jiangjin.ProcessingMode = ProcessingMode.Local;
            rv_geren_jiangjin.LocalReport.ReportPath = geren_path;
            ReportDataSource rpt_genren_DataSource = new ReportDataSource("DataSet1", SqlDataSource2);
            rv_geren_jiangjin.LocalReport.DataSources.Add(rpt_genren_DataSource);
            rv_geren_jiangjin.LocalReport.Refresh();


        }
    }
}