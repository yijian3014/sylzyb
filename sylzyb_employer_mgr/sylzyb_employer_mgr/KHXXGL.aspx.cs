using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
namespace sylzyb_employer_mgr
{
    public partial class KHGL : System.Web.UI.Page
    {
        public static string sel_string = "select * from [dzsw].[dbo].[Syl_AppraiseInfo] where  TC_DateTime between  dateadd(month,-2,getdate()) and getdate()  order by AppKind desc, AppGroup,TC_DateTime";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        SqlDataReader dr;
        static string login_usrid;
        public static string lb;

        Check option_ck = new Check();
        private int module_kind = 0;
        private int item_kind = 1;
        khgl khgl_qichao = new khgl();
        khgl khgl_select = new khgl();
        khgl khgl_shenpi = new khgl();
        DataSet ds_worker = new DataSet();
        DataSet ds_appWorker = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (option_ck.Module("考核信息管理", module_kind) == false || System.Web.HttpContext.Current.Session["UserName"].ToString() == "" || System.Web.HttpContext.Current.Session["IDCard"] == null)
                {
                    System.Web.HttpContext.Current.Session["RealName"] = "";
                    System.Web.HttpContext.Current.Session["IDCard"] = "";
                    System.Web.HttpContext.Current.Session["UserName"] = "";
                    System.Web.HttpContext.Current.Session["UserLevel"] = "";
                    System.Web.HttpContext.Current.Session["UserLevelName"] = "";
                    System.Web.HttpContext.Current.Session["UserPower"] = "";
                    System.Web.HttpContext.Current.Session["ModulePower"] = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('您尚未登陆或登陆超时');location.href='Login.aspx';</script>");

                }
                else
                {
                    tbx_ed_time.Text = DateTime.Now.ToString();
                    tbx_bg_time.Text = DateTime.Now.AddMonths(-1).ToString();


                    login_user.Text = System.Web.HttpContext.Current.Session["RealName"].ToString();
                    gv_App_gailan.DataSource = khgl_select.select_zhonglan(tbx_bg_time.Text, tbx_ed_time.Text);
                    gv_App_gailan.DataBind();



                    dv_qicaokaohe.Visible = false;
                    dv_gailan.Visible = true;
                    dv_khxd.Visible = false;
                    dv_shenpi.Visible = false;
                }

            }
            if (dv_qicaokaohe.Visible)
            {
                dv_gailan.Visible = false;
                dv_khxd.Visible = false;
                dv_shenpi.Visible = false;

            }
            if (dv_shenpi.Visible)
            {
                dv_qicaokaohe.Visible = false;
                dv_gailan.Visible = false;
                dv_khxd.Visible = false;

            }
            if (dv_gailan.Visible)
            {
                dv_qicaokaohe.Visible = false;
                dv_khxd.Visible = false;
                dv_shenpi.Visible = false;
            }


        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbl_cx.SelectedIndex == 0)
            {

                gv_App_gailan.DataSource = khgl_shenpi.select_zhonglan(tbx_bg_time.Text, tbx_ed_time.Text);
                BTN_BLLC.Visible = false;
            }
            if (rbl_cx.SelectedIndex == 1)
            {

                gv_App_gailan.DataSource = khgl_select.select_daiban(tbx_bg_time.Text, tbx_ed_time.Text, Session["IDCARD"].ToString(), Session["UserLevelName"].ToString());


                BTN_BLLC.Visible = true;
            }
            if (rbl_cx.SelectedIndex == 2)
            {
                gv_App_gailan.DataSource = khgl_select.select_yibanjie(tbx_bg_time.Text, tbx_ed_time.Text, Session["IDCARD"].ToString(), Session["UserLevelName"].ToString());
                BTN_BLLC.Visible = false;
            }
            gv_App_gailan.DataBind();
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


                }
            }
            catch (Exception er)
            {

                Response.Write(er.Message.ToString());

            }
        }
        protected void gv_App_gailan_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < gv_App_gailan.Rows.Count; i++)
            {
                gv_App_gailan.Rows[i].BackColor = System.Drawing.Color.White;

            }
            if (gv_App_gailan.SelectedIndex >= 0)
            //表格表头索引是-1，要屏蔽
            {
                gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;


                khgl_select.select_sigleflow(gv_App_gailan.SelectedIndex);

            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {


        }

        protected void gv_App_gailan_RowDataBound(object sender, GridViewRowEventArgs e)
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
            dv_shenpi.Visible = true;
            if (gv_App_gailan.SelectedIndex > -1)
            {

            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请先从表中选择待办项');</script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('办理考核,该功能尚未完善!');</script>");

        }
        protected void gv_App_gailan_RowCreated(object sender, GridViewRowEventArgs e)
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

            dv_qicaokaohe.Visible = true;
            dv_gailan.Visible = false;
            qicaokaohe_init();


        }

        protected void btn_khfk_ok_Click(object sender, EventArgs e)
        {

        }

        protected void btn_khfk_calcel_Click(object sender, EventArgs e)
        {

            Session["UserID"] = "";
            Session["UserName"] = "";
            Session["UserRName"] = "";
            Session["UserRule"] = "";

            Response.Redirect("login.aspx");
        }
        protected void DateCheck(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            DateTime tem;
            bool isDateTime = DateTime.TryParse(text, out tem);
            if (isDateTime)
            {
                tbx_qckh_FS_DateTime.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                ((TextBox)sender).Text = "正确格式:YYYY-MM-DD或YYYY/M/D";
                ((TextBox)sender).ForeColor = System.Drawing.Color.Red;
            }

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
                else { tbx_bg_time.Text = "开发始日期不能大于结束日期"; tbx_bg_time.ForeColor = System.Drawing.Color.Red; }
            }
            else
            {
                if (!bg) { tbx_bg_time.Text = "正确格式:YYYY-MM-DD或YYYY/M/D"; tbx_bg_time.ForeColor = System.Drawing.Color.Red; }
                if (!ed) { tbx_ed_time.Text = "正确格式:YYYY-MM-DD或YYYY/M/D"; tbx_ed_time.ForeColor = System.Drawing.Color.Red; }
            }

        }

        protected void ddl_qckh_AppName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_qckh_ok_Click(object sender, EventArgs e)

        {
            //这部分需要完成以下操作：1、选择下一步经办人，保存所有考核信息。
            dv_qicaokaohe.Visible = false;
            dv_gailan.Visible = true;
            btn_appworker_add.Visible = false;
            khgl_qichao.update_flow(Convert.ToInt32(lb_qckh_AppraiseID.Text));
            
           

           
        }

        protected void btn_qckh_cancel_Click(object sender, EventArgs e)
        {
            dv_qicaokaohe.Visible = false;
            dv_gailan.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('操作取消数据未同步！该功能尚未完善!" + e.ToString() + "');</script>");

        }

        protected void btn_shenpi_ok_Click(object sender, EventArgs e)
        {
            dv_shenpi.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('审批操作！该功能尚未完善!" + e.ToString() + "');</script>");

        }

        protected void btn_shenpi_cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('审批操作取消数据未同步！该功能尚未完善!" + e.ToString() + "');</script>");

        }

        protected void ddl_qckh_AppGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //这个方法同步填充被考核人员GRIDVIEW。编缉，但不同步到数据表，通过确定扫描GRIDVIEW 对应的单元格。

            dv_qicaokaohe.Visible = true;
            ds_worker = khgl_qichao.select_WorkerInfo(ddl_qckh_AppGroup.SelectedItem.Text);
            cbl_workers.Items.Clear();
            for (int i = 0; i < ds_worker.Tables[0].Rows.Count; i++)
            {
                //cbl_workers.Items.Add(ds_worker.Tables[0].Rows[i][1].ToString());
                cbl_workers.Items.Add("");
                cbl_workers.Items[i].Text = ds_worker.Tables[0].Rows[i][1].ToString();
                cbl_workers.Items[i].Value = ds_worker.Tables[0].Rows[i][2].ToString();
            }
          //对于旧的APPID,可能存在最早的未生效的被考核人员信息，如何清除？
            ds_appWorker = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), tbx_bg_time.Text, tbx_ed_time.Text);
            if (ds_appWorker != null)
                if (ds_appWorker.Tables.Count > 0)
                    if (ds_appWorker.Tables[0].Rows.Count > 0)
                    {
                        gv_AppWorker.DataSource = ds_appWorker;
                        gv_AppWorker.DataBind();

                    }
                    else
                    {
                        gv_AppWorker.DataSource = "";
                        gv_AppWorker.DataBind();
                    }
            btn_appworker_add.Visible = true;
        }


        TextBox tb = new TextBox();
        Button bt = new Button();

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ((GridView)sender).Rows.Count; i++)
            {
                ((GridView)sender).Rows[i].BackColor = System.Drawing.Color.White;
                ((GridView)sender).EditIndex = -1;
            }
            if (((GridView)sender).SelectedIndex >= 0)
            //表格表头索引是-1，要屏蔽

            {
                ////((GridView)sender).EditIndex = ((GridView)sender).SelectedIndex;


                //((GridView)sender).Rows[((GridView)sender).SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;
                //((GridView)sender).Rows[((GridView)sender).SelectedIndex].Cells[6].Enabled = true;




                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;



                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[8].Controls[1].Visible = true;
                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[8].Controls[3].Visible = true;
                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[8].Controls[1].Focus();

            }

        }
        //gridview 行的删除，编辑（不把数据同步至后台）只有在确定后表数据写入库。待解决问题：行数据的删除，单元格的编辑
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            PostBackOptions myPostBackOptions = new PostBackOptions(this);
            myPostBackOptions.AutoPostBack = false;
            myPostBackOptions.RequiresJavaScriptProtocol = true;
            myPostBackOptions.PerformValidation = false;
            String evt = Page.ClientScript.GetPostBackClientHyperlink(sender as GridView, "Select$" + e.Row.RowIndex.ToString());
            e.Row.Attributes.Add("onclick", evt);


        }
        protected void gv_AppWorker_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }



        protected void gv_detail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gv_detail_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gv_detail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        public void qicaokaohe_init()
        {
            lb_qckh_AppraiseID.Text = Convert.ToString(khgl_qichao.build_newid(Session["RealName"].ToString(), Session["IDCard"].ToString()));
            lb_qckh_Flow_State.Text = "点检";
            lb_qckh_ApplicantName.Text = Session["RealName"].ToString();
            lb_qckh_AppAmount.Text = "0";
            lb_qckh_TC_DateTime.Text = DateTime.Now.ToString();
            tbx_qckh_FS_DateTime.Text = "正确格式:YYYY-MM-DD或YYYY/M/D";
            tbx_qckh_AppContent.Text = "";
            tbx_qckh_AppBy.Text = "";

        }

        protected void tbx_qckh_FS_DateTime_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gv_AppWorker_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }



        public void qckh_update_AppMount(object sender, EventArgs e)
        {
            string tmp_value = ((TextBox)gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[8].Controls[1]).Text.Trim();
            if (Regex.IsMatch(tmp_value, @"\d{1,}.\d{1,}|[-]\d{1,}.\d{1,}|\d{1,}|[-]\d{1,}"))
            {
                if (khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[6].Text.Trim(), "[AppAmount]", tmp_value))
                    ((Button)sender).Visible = false;
                lb_qckh_AppAmount.Text =Convert.ToString(Convert.ToDecimal(lb_qckh_AppAmount.Text) + Convert.ToDecimal(((TextBox)gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[8].Controls[1]).Text));

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('请录准确备录入金额！" + e.ToString() + "');</script>");

            }
        }


        protected void cbl_workers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //按CBL_WORKER选择人员向表GV_APPWORKER添减人员




        }

        protected void btn_appworker_add_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_workers.Items.Count; i++)
            {
                if (cbl_workers.Items[i].Selected)
                {

                    khgl_qichao.insert_single_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[ApplicantName],[ApplicantIDCard],[AppName],[AppIDCard],[App_State]", Session["RealName"].ToString().Trim()
                        + "," + Session["IDCard"].ToString().Trim() + "," + cbl_workers.Items[i].Text.Trim() + "," + cbl_workers.Items[i].Value + ",未生效");
                }

                else

                    khgl_qichao.delete_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), cbl_workers.Items[i].Value);
            }
            gv_AppWorker.DataSource = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), tbx_bg_time.Text, tbx_ed_time.Text);

            gv_AppWorker.DataBind();

        }


        protected void rbl__qckh_nextORprevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] step;
            if (rbl_qckh_nextORprevious.SelectedItem.Text == "转交")
            {
                step = khgl_qichao.get_step_list(rbl_qckh_nextORprevious.SelectedItem.Text, lb_qckh_Flow_State.Text);
                if (step.Length > 1 && (step != null | step.Length < 1))
                    foreach (string tmp_str in step)
                    {
                        rbl_qckh_step.Items.Add(tmp_str);
                    }
                rbl_qckh_step.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('起草人删除考核功能未完善" + e.ToString() + "');</script>");
            }
        }

        protected void rbl_qckh_step_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_peoples;
            if (rbl_qckh_step.SelectedItem.Text != "")
            {
                ds_peoples = khgl_qichao.get_jingbanren(Convert.ToInt32(lb_qckh_AppraiseID.Text), rbl_qckh_nextORprevious.SelectedItem.Text, rbl_qckh_step.SelectedItem.Text);
                cbl_qckh_next_persion.Items.Clear();

                for (int i = 0; i < ds_peoples.Tables[0].Rows.Count; i++)
                {                   
                    cbl_qckh_next_persion.Items.Add("");
                    cbl_qckh_next_persion.Items[i].Text = ds_peoples.Tables[0].Rows[i][0].ToString();
                    cbl_qckh_next_persion.Items[i].Value = ds_peoples.Tables[0].Rows[i][1].ToString();
                }
                cbl_qckh_next_persion.DataBind();
            }
        }

        protected void cbl_qckh_next_persion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel_count = 0;

                for (int i = 0; i < cbl_qckh_next_persion.Items.Count; i++)
                    if (cbl_qckh_next_persion.Items[i].Selected)
                        sel_count++;
            if (cb_qckh_is_huiqian.Checked == false)
            {
                if (sel_count > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('只有在会签模式下才允许选择单人！');</script>");
                    for (int i = 0; i < cbl_qckh_next_persion.Items.Count; i++)

                        cbl_qckh_next_persion.Items[i].Selected = false;
                }
            }
        }
    }
}