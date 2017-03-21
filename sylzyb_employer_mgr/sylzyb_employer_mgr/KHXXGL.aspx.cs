using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace sylzyb_employer_mgr
{
    public partial class KHGL : System.Web.UI.Page
    {
        public static string sel_string = "select * from [dzsw].[dbo].[Syl_AppraiseInfo] where  TC_DateTime between  dateadd(month,-2,getdate()) and getdate()  order by AppKind desc, AppGroup,TC_DateTime";
        db  ds = new db();
        public DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        SqlDataReader dr;
        static string login_usrid;
        public static string lb;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (rbl_cx.SelectedIndex == 0)
            {
                sel_string = "select * from [dzsw].[dbo].SJ2B_KH_KaoHe_info   where  AppraiseTime between  '"
                    + tbx_bg_time.Text + "' and  '" + tbx_ed_time.Text + "'  order by AppraiseClass desc, AppraiseGroup,AppraiseTime";

                BTN_BLLC.Visible = false;
            }
            if (rbl_cx.SelectedIndex == 1)
            {

                sel_string = "select * from [dzsw].[dbo].SJ2B_KH_KaoHe_info "
                  + " where   AppraiseTime between  '" + tbx_bg_time.Text + "' and  '" + tbx_ed_time.Text + "' and(  (flow_state='考核人'and KHFK_ZT is not null and  userid='" + Session["UserID"].ToString() + "')"
                      + " or (flow_state='主管领导'and   Leader_1_State is null  and AppraiseClass='" + lb + "'))"
                 + " order by AppraiseClass desc, AppraiseGroup,AppraiseTime";



                BTN_BLLC.Visible = true;
            }
            if (rbl_cx.SelectedIndex == 2)
            {
                sel_string = "select * from [dzsw].[dbo].SJ2B_KH_KaoHe_info "
                + " where  AppraiseTime between  '" + tbx_bg_time.Text + "' and  '" + tbx_ed_time.Text + "' and(  (flow_state<>'考核人'and KHFK_ZT is not null and userid='" + Session["UserID"].ToString() + "')"
                    + " or (flow_state<>'主管领导'and   Leader_1_State is not null  and AppraiseClass='" + lb + "'))"
               + "  order by AppraiseClass desc, AppraiseGroup,AppraiseTime";


                BTN_BLLC.Visible = false;
            }
            ds1 = ds.GetDataSet(sel_string, "SJ2B_KH_KaoHe_info");
            GridView1.DataSource = ds1;

            GridView1.DataBind();
        }
        protected void get_sing_rec(string sel_rec)
        {
            SqlDataReader dr = ds.datareader(sel_rec);
            try
            {
                while (dr.Read())
                {

                    string ID_ = dr["ID"].ToString();
                    string AppraiseID_ = dr["AppraiseID"].ToString();
                    string Flow_State_ = dr["Flow_State"].ToString();
                    string UserID_ = dr["UserID"].ToString();
                    string UserName_ = dr["UserName"].ToString();
                    string tc_DateTime_ = dr["tc_DateTime"].ToString();
                    string AppraiseClass_ = dr["AppraiseClass"].ToString();
                    string AppraiseTime_ = dr["AppraiseTime"].ToString();
                    string AppraiseGroup_ = dr["AppraiseGroup"].ToString();
                    string AppraiseGroupID_ = dr["AppraiseGroupID"].ToString();
                    string AppraiseContent_ = dr["AppraiseContent"].ToString();
                    string kh_jiner_ = dr["kh_jiner"].ToString();
                    string DJ_ReturnTime_ = dr["DJ_ReturnTime"].ToString();
                    string KHFK_YJ_ = dr["KHFK_YJ"].ToString();
                    string KHFK_ZT_ = dr["KHFK_ZT"].ToString();
                    string KHFK_SJ_ = dr["KHFK_SJ"].ToString();
                    string ClassState_ = dr["ClassState"].ToString();
                    string ClassObjection_ = dr["ClassObjection"].ToString();
                    string COTime_ = dr["COTime"].ToString();
                    string ChargehandOpinion_ = dr["ChargehandOpinion"].ToString();
                    string ChargehandState_ = dr["ChargehandState"].ToString();
                    string Leader_1_Opinion_ = dr["Leader_1_Opinion"].ToString();
                    string Leader_1_State_ = dr["Leader_1_State"].ToString();
                    string Leader_2_Opinion_ = dr["Leader_2_Opinion"].ToString();
                    string Leader_2_State_ = dr["Leader_2_State"].ToString();
                    string Leader_3_Opinion_ = dr["Leader_3_Opinion"].ToString();
                    string Leader_3_State_ = dr["Leader_3_State"].ToString();
                    //直接将数据DR值转STRING肤质给LABEL.TEXT会报类型错误，所以用带_的由名字符变量中转一下。
                  
                }
            }
            catch (Exception er)
            {

                Response.Write(er.Message.ToString());

            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.White;

            }
            if (GridView1.SelectedIndex >= 0)
            //表格表头索引是-1，要屏蔽
            {
                GridView1.Rows[GridView1.SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;

              
                string sel_rec = "";
                sel_rec = "select * from SJ2B_KH_KaoHe_info where AppraiseID=" + GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
                get_sing_rec(sel_rec);

            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            PostBackOptions myPostBackOptions = new PostBackOptions(this);
            myPostBackOptions.AutoPostBack = false;
            myPostBackOptions.RequiresJavaScriptProtocol = true;
            myPostBackOptions.PerformValidation = false;

            String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString());
            e.Row.Attributes.Add("onclick", evt);

        }

        protected void BTN_BLLC_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex > -1)
            {
               
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请先从表中选择待办项');</script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('办理考核,该功能尚未完善!');</script>");

        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.Cells.Count == 22)
            {
               
            }
        }
     
      

        protected void btn_exit_Click(object sender, EventArgs e)
        {
            Session["UserID"] = "";
            Session["UserName"] = "";
            Session["UserRName"] = "";
            Session["UserRule"] = "";

            Response.Redirect("login.aspx");
        }

        protected void btn_tckh_Click(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('写入考核信息,该功能尚未完善!');</script>");

        }

        protected void btn_khfk_ok_Click(object sender, EventArgs e)
        {
            //0、废除
            //1、考核人
            //2、被考核人
            //3、组长
            //4、主管领导
            //5、书记
            //6、主任
            //7、完成
            //说明：任何人都可以发起考核。
            //但所遵守的原则是审核得要它的上级来进行。同意则由更上级审核，不同意则打回考核提出人。
            string sqlstr_update = "";
            string next_step = "";
            if (ddl_khfk_zt.Text == "同意")

            {
                switch (Convert.ToInt16(lb_tcr_usrid.Text) / 1000)
                {
                    case 1:
                        next_step = "组长";
                        break;
                    case 3:
                        next_step = "主管领导";
                        break;
                    case 4:
                        next_step = "书记";
                        break;
                    case 5:
                        next_step = "主任";
                        break;
                    case 6:
                        next_step = "完成";
                        break;
                }
            }

            if (ddl_khfk_zt.Text == "不同意")
                next_step = "考核人";//选择不同意，转到考核人


            if (tbx_xd_khfk_yj.Text == "&nbsp;" || tbx_xd_khfk_yj.Text == "")
            //判断是否是第一次办理，只记录第一次办里时间。
            {
                tbx_khfk_yj.Text = tbx_khfk_yj.Text.Replace("'", "''");
                tbx_khfk_yj.Text += "'+ Char(13)+Char(10)+'该信息由:" + Session["UserRname"].ToString() + " 编辑于 " + DateTime.Now.ToString() + "'+Char(13)+Char(10)+'";
                sqlstr_update = "update [dzsw].[dbo].[SJ2B_KH_KaoHe_info] set [KHFK_YJ] ='" + tbx_khfk_yj.Text
                + "',[KHFK_SJ]=getdate(),KHFK_ZT='" + ddl_khfk_zt.Text + "'  ,flow_state ='" + next_step
                + "' where AppraiseGroupID='" + Session["UserID"].ToString() + "'"
                + " and AppraiseID=" + GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text.Trim();
            }
            else
            {
                tbx_khfk_yj.Text = tbx_khfk_yj.Text.Replace("'", "''");
                tbx_khfk_yj.Text += "'+ Char(13)+Char(10)+'该信息由:" + Session["UserRname"].ToString() + " 编辑于 " + DateTime.Now.ToString() + "'+Char(13)+Char(10)+'";
                sqlstr_update = "update[dzsw].[dbo].[SJ2B_KH_KaoHe_info] set [KHFK_YJ] ='" + tbx_khfk_yj.Text
                    + "',[KHFK_SJ]=getdate(),KHFK_ZT='" + ddl_khfk_zt.Text + "',flow_state ='" + next_step
                    + "' where AppraiseGroupID='" + Session["UserID"].ToString() + "'"
                    + " and AppraiseID=" + GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text.Trim();
            }
            ds.ExecSQL(sqlstr_update);
            GridView1.DataSource = ds.GetDataSet(sel_string, "SJ2B_KH_KaoHe_info");
            GridView1.DataBind();
        }

        protected void btn_khfk_calcel_Click(object sender, EventArgs e)
        {

            Session["UserID"] = "";
            Session["UserName"] = "";
            Session["UserRName"] = "";
            Session["UserRule"] = "";

            Response.Redirect("login.aspx");
        }
        protected void tbx_check_Click(object sender, EventArgs e)
        {
            TextBox TBX = (TextBox)sender;
            TBX.Text.Replace("<", "<'");
            TBX.Text.Replace(">", "'>");
        }

        protected void tbx_time_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            DateTime tem;
            bool isDateTime = DateTime.TryParse(text, out tem);
            if (isDateTime)
            {
                // ((TextBox)sender).Text = Convert.ToDateTime(text).ToString().Substring(0,10);        
                //其他代码
            }
            else
                ((TextBox)sender).Text = "正确格式:2013-04-02或2013/4/2";
        }
        protected void btn_reflash_Click(object sender, EventArgs e)
        {

            DateTime bg_t, ed_t;
            bool bg = DateTime.TryParse(tbx_bg_time.Text, out bg_t);
            bool ed = DateTime.TryParse(tbx_ed_time.Text, out ed_t);
            if (bg && ed)
            {
                TimeSpan midTime = DateTime.Parse(tbx_ed_time.Text) - DateTime.Parse(tbx_bg_time.Text);

                if (midTime.Days > 0)
                    RadioButtonList1_SelectedIndexChanged(sender, e);
            }
            else
            {
                if (!bg) tbx_bg_time.Text = "正确格式:2013-04-02或2013/4/2";
                if (!ed) tbx_ed_time.Text = "正确格式:2013-04-02或2013/4/2";
            }

        }

        protected void ddl_qckh_AppName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_qckh_ok_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('写入考核信息,该功能尚未完善!');</script>");

        }

        protected void btn_qckh_cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('操作取消数据未同步！该功能尚未完善!" + e.ToString() + "');</script>");

        }

        protected void btn_shenpi_ok_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('审批操作！该功能尚未完善!" + e.ToString() + "');</script>");

        }

        protected void btn_shenpi_cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('审批操作取消数据未同步！该功能尚未完善!" + e.ToString() + "');</script>");

        }

        protected void ddl_qckh_AppGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //这个方法同步填充被考核人员GRIDVIEW。编缉，但不同步到数据表，通过确定扫描GRIDVIEW 对应的单元格。

        }
    }
}