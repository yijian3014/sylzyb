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
    //public static string sel_string = "select * from SJ2B_KH_KaoHe_info";
    //db ds = new db();
    //public DataSet ds1 = new DataSet();
    //DataTable dt1 = new DataTable();
    //SqlDataReader dr;
    //public static string lb;
    //ReportDataSource res = new ReportDataSource();

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {

    //        tbx_bg_date.Text = DateTime.Now.Date.AddMonths(-1).ToShortDateString();
    //        tbx_ed_date.Text = DateTime.Now.Date.ToShortDateString();
    //        btn_cx_Click(sender, e);

    //    }

    //}

    //protected void btn_bg_date_Click(object sender, EventArgs e)
    //{

    //    pnl_bg_date.Visible = true;

    //}

    //protected void btn_ed_time_Click(object sender, EventArgs e)
    //{
    //    pnl_ed_date.Visible = true;
    //}

    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    //设置开始时间
    //    pnl_bg_date.Visible = false;
    //    tbx_bg_date.Text = cld_bg_date.SelectedDate.Date.ToShortDateString();
    //    btn_cx_Click(sender, e);
    //}

    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    //设置结束时间
    //    pnl_ed_date.Visible = false;
    //    tbx_ed_date.Text = cld_ed_date.SelectedDate.Date.ToShortDateString();
    //    btn_cx_Click(sender, e);
    //}



    //protected void btn_cx_Click(object sender, EventArgs e)
    //{
    //    string lclb = "";//类别
    //    string lczt = "";//状态
    //    string lc_banbie = "";//班别 
    //    if (ddl_lclb.Text != "全部")
    //        lclb = " and AppraiseClass='" + ddl_lclb.Text + "'";
    //    if (ddl_lczt.Text != "全部")
    //        lczt = "  and Flow_State='" + ddl_lczt.Text + "'";
    //    if (ddl_lczt.Text == "其它")
    //        lczt = "  and Flow_State<>'完成' and Flow_State<>'废除'  ";
    //    if (ddl_banbie.Text != "全部")
    //        lc_banbie = " and AppraiseGroup='" + ddl_banbie.Text + "'";
    //    if (ddl_banbie.Text == "其它")
    //        lc_banbie = " and AppraiseGroup not in ( '甲班','乙班','丙班','丁班','白班') ";


    //    sel_string = "SELECT AppraiseID, Flow_State, UserID, UserName, tc_DateTime, AppraiseClass, AppraiseTime, AppraiseGroup, AppraiseContent,kh_jiner, DJ_ReturnTime,KHFK_ZT, ClassState, ClassObjection, COTime, ChargehandOpinion, ChargehandState, Leader_1_Opinion, Leader_1_State, Leader_2_Opinion, Leader_2_State, Leader_3_Opinion, Leader_3_State FROM [dzsw].[dbo].SJ2B_KH_KaoHe_info WHERE AppraiseTime BETWEEN '"
    //        + tbx_bg_date.Text.Trim() + "' AND '" + tbx_ed_date.Text.Trim() + "'"
    //        + lczt + lclb + lc_banbie
    //        + " order by AppraiseTime ,AppraiseGroup ,AppraiseClass";
    //    //ds1 = ds.GetDataSet(sel_string, "SJ2B_KH_KaoHe_info");
    //    SqlDataSource1.SelectCommand = sel_string;


    //    //res.DataSourceId = "ds1";

    //    ReportViewer1.LocalReport.DataSources.Clear();
    //    string path = Path.Combine(Server.MapPath(@"\"), "Report1.rdlc");
    //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
    //    ReportViewer1.LocalReport.ReportPath = path;
    //    ReportDataSource rptDataSource = new ReportDataSource("ds1", SqlDataSource1);
    //    ReportViewer1.LocalReport.DataSources.Add(rptDataSource);

    //    ReportViewer1.LocalReport.Refresh();

    //}



    //protected void ddl_lczt_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btn_cx_Click(sender, e);
    //}

    //protected void ddl_lclb_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btn_cx_Click(sender, e);
    //}

    //protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    //{

    //}

    //protected void btn_exit_Click(object sender, EventArgs e)
    //{

    //    Session["UserID"] = "";
    //    Session["UserName"] = "";
    //    Session["UserRName"] = "";
    //    Session["UserRule"] = "";

    //    Response.Redirect("login.aspx");
    //}
}