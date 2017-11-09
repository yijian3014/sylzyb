using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

namespace sylzyb_employer_mgr
{
    public partial class KHGL : System.Web.UI.Page
    {

        public static string sel_string = "select * from [dzsw].[dbo].[Syl_AppraiseInfo] order by TC_DateTime";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        public static string lb;

        Check ck_opt = new Check();
        private int module_kind = 0;

        khgl khgl_gl = new khgl();
        khgl khgl_qichao = new khgl();
        khgl khgl_select = new khgl();
        khgl khgl_shenpi = new khgl();
        DataSet ds_worker = new DataSet();
        DataSet ds_appWorker = new DataSet();
        DataSet ds_SylAppRun = new DataSet();
        static DataSet ds_AppraiseInfo = new DataSet();

        public static int UI_disp_code = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            if (!IsPostBack)
            {

                if (ck_opt.Module("考核信息管理", module_kind) == false || System.Web.HttpContext.Current.Session["UserName"].ToString() == "" || System.Web.HttpContext.Current.Session["IDCard"] == null)
                {
                    System.Web.HttpContext.Current.Session["RealName"] = "";
                    System.Web.HttpContext.Current.Session["IDCard"] = "";
                    System.Web.HttpContext.Current.Session["UserName"] = "";
                    System.Web.HttpContext.Current.Session["UserLevel"] = "";
                    System.Web.HttpContext.Current.Session["UserLevelName"] = "";
                    System.Web.HttpContext.Current.Session["UserPower"] = "";
                    System.Web.HttpContext.Current.Session["ModulePower"] = "";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('您尚未登陆或登陆超时');location.href='Login.aspx';</script>");
                    Response.Redirect("login.aspx");
                }
                else
                {
                  
                   
                    ddl_year.SelectedValue= DateTime.Now.Year.ToString();
                    ddl_month.SelectedValue= DateTime.Now.Month.ToString().PadLeft(2,'0');

                    login_user.Text = System.Web.HttpContext.Current.Session["RealName"].ToString();
                    ds_AppraiseInfo = khgl_select.select_zhonglan( "AND convert(NVARCHAR(7),[TC_DateTime], 120) = '" + ddl_year.SelectedItem.Text + "-" + ddl_month.SelectedItem.Text + "'");

                    gv_App_gailan.DataSource = ds_AppraiseInfo;
                    gv_App_gailan.DataBind();
                    btn_qzsc.Enabled = ck_opt.item("强制删除", 1);
                    btn_qzxg.Enabled = ck_opt.item("强制修改", 1);
                    btn_qzzj.Enabled = ck_opt.item("强制转交", 1);
                    btn_qzsx.Enabled = ck_opt.item("强制生效", 1);
                    btn_qckh.Enabled = ck_opt.item("考核-提出考核", 1);
                    btn_khql.Enabled = ck_opt.item("考核清理", 1);
                   
                    btn_sckh.Visible = false;
                    if (rbl_gailan_cx.SelectedIndex == 1)
                    { btn_khgd.Visible = ck_opt.item("归档", 1); }

                    btn_xgkh.Visible = false;

                    btn_qckh.Visible = true;
                    btn_qzsx.Visible = true;
                    btn_qzzj.Visible = true;
                    btn_qzxg.Visible = true;
                    btn_qzsc.Visible = true;
                    UI_disp_code = 0;
                }

            }
            switch (UI_disp_code)
            {
                //登陆 查询 删除考核 归档考核、强制生效
                case 0:
                    {
                        rbl_gailan_cx.Focus();
                        dv_qicaokaohe.Visible = false;
                        dv_gailan.Visible = true;

                        dv_khxd.Visible = false;
                        dv_shenpi.Visible = false;
                        btn_qckh.Visible = true;
                        btn_qzsx.Visible = true;
                        btn_qzzj.Visible = true;
                        btn_qzxg.Visible = true;
                        btn_qzsc.Visible = true;

                        break;
                    }
                //提出考核、修改考核
                case 1:
                    {
                        tbx_qckh_AppContent.Focus();
                        dv_gailan.Visible = false;
                        dv_khxd.Visible = false;
                        dv_shenpi.Visible = false;
                        dv_qicaokaohe.Visible = true;

                        btn_qckh.Visible = true;
                        btn_qzsx.Visible = true;
                        btn_qzzj.Visible = true;
                        btn_qzxg.Visible = true;
                        btn_qzsc.Visible = true;

                        break;
                    }
                //审批考核
                case 2:
                    {
                        tbx_shenpi_yj.Focus();
                        dv_gailan.Visible = false;
                        dv_shenpi.Visible = true;
                        dv_qicaokaohe.Visible = false;
                        dv_khxd.Visible = true;

                        btn_qckh.Visible = true;
                        btn_qzsx.Visible = true;
                        btn_qzzj.Visible = true;
                        btn_qzxg.Visible = true;
                        btn_qzsc.Visible = true;

                        break;
                    }
            }
            if ((Control)sender != null)
                Page.SetFocus((Control)sender);
       


        }


        protected void khxd_init()
        {
            /*
           ,[AppID]
      ,[Flow_State]
      ,[ApplicantName]
      ,[ApplicantIDCard]
      ,[AppKind]
      ,[AppAmount]
      ,[TC_DateTime]
      ,[FS_DateTime]
      ,[AppGroup]
      ,[AppNames]
      ,[AppContent]
      ,[AppBy]
      ,[step_1_Oponion]
      ,[step_1_Comment]
      ,[step_2_Oponion]
      ,[step_2_Comment]
      ,[step_3_Oponion]
      ,[step_3_Comment]
      ,[step_4_Oponion]
      ,[step_4_Comment]
      ,[step_5_Oponion]
      ,[step_5_Comment]

        编号: lb_khxd_AppraiseID
      流转状态: lb_khxd_Flow_State
      提出人姓名: lb_khxd_ApplicantName
      提出人身份证号: lb_khxd_ApplicantIDCard
      类型: lb_khxd_AppKind
      金额: lb_khxd_AppAmount
      提出时间: lb_khxd_TC_DateTime
      事件发生时间: lb_khxd_FS_DateTime
      被考核人所在班组: lb_khxd_AppGroup
      被考核对象: lb_khxd_AppNames
      考核内容: lb_khxd_AppContent
      考核依据: tbx_khxd_AppBy
      意见汇总（组长）: lb_khxd_step_1_Oponion
      评论汇总（组长）: tbx_khxd_step_1_Comment
      意见汇总（工程师）: lb_khxd_step_2_Oponion
      批评论汇总（工程师）: tbx_khxd_step_2_Comment
      意见汇总（区域主管）: lb_khxd_step_3_Oponion
      评论汇总（区域主管）: tbx_khxd_step_3_Comment"
意见汇总（书记）: lb_khxd_step_4_Oponion
评论汇总（书记）: tbx_khxd_step_4_Comment
意见汇总（部长）:  lb_khxd_step_5_Oponion
评论汇总（部长）: tbx_khxd_step_5_Comment
*/

            if (gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text != "")
            {
                ////说明：前台VISIBLE=FALSE时，必须用DS数据源，不能用GV数据。
                lb_khxd_AppraiseID.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text;
                lb_khxd_Flow_State.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text;
                lb_khxd_ApplicantName.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[3].Text;
                //lb_khxd_ApplicantIDCard.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].Text;
                lb_khxd_ApplicantIDCard.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][4].ToString();


                //lb_khxd_Applevel.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[5].Text;
                lb_khxd_Applevel.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][5].ToString();


                //lb_khxd_AppKind.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[6].Text;
                lb_khxd_AppKind.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][6].ToString();


                lb_khxd_AppAmount.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[7].Text;
                //lb_khxd_TC_DateTime.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[8].Text;
                lb_khxd_TC_DateTime.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][8].ToString();


                lb_khxd_FS_DateTime.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[9].Text;
                lb_khxd_AppGroup.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[10].Text;
                //lb_khxd_AppNames.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[11].Text; 
                lb_khxd_AppNames.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][11].ToString();


                lb_khxd_AppContent.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[12].Text;
                //tbx_khxd_AppBy.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[13].Text;
                tbx_khxd_AppBy.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][13].ToString();

                lb_khxd_step_1_Oponion.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[14].Text;
                //  tbx_khxd_Step_1_Comment.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[15].Text;
                tbx_khxd_Step_1_Comment.Text= ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][15].ToString();
                lb_khxd_step_2_Oponion.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[16].Text;
                //tbx_khxd_step_2_Comment.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[17].Text;
                tbx_khxd_step_2_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][17].ToString();

                lb_khxd_step_3_Oponion.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[18].Text;
               // tbx_khxd_step_3_Comment.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[19].Text;
                tbx_khxd_step_3_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][19].ToString();

                lb_khxd_step_4_Oponion.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[20].Text;
                //tbx_khxd_step_4_Comment.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[21].Text;
                tbx_khxd_step_4_Comment.Text = tbx_khgl_info.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][20].ToString();


                lb_khxd_step_5_Oponion.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[22].Text;
                //tbx_khxd_step_5_Comment.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[23].Text;
                tbx_khxd_step_5_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][23].ToString();



                //
                tbx_khgl_info.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex+10 * gv_App_gailan.PageIndex][25].ToString();
               // tbx_khgl_info.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[25].Text;


            }
            ds_appWorker = khgl_select.select_appworkerinfo(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text));
            gv_detail_appworker.DataSource = ds_appWorker;
            gv_detail_appworker.DataBind();
            
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

                dv_khxd.Visible = true;
                khxd_init();

            }
            UI_disp_code = 0;
            
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
        /// <summary>
        /// 进入审批考核单元，管理员，办事员不可以审批考核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_shenpikaohe_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserLevelName"].ToString() == "办事员" && lb_khxd_Flow_State.Text=="办事员")
                {
                    throw new Exception("提示：办事员不可以参与审批，直接点“升效”按钮使流程生效");
                }
            

            if (gv_App_gailan.SelectedIndex != -1)
                {
                    // 此处加入对是否为起起草人的判定，按程序设定起草人不允许审批，只允许修改考核、删除考核、
                    if (Session["IDCard"].ToString() != gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].Text)
                    {
                        UI_disp_code = 2;
                        shenpikaohe_init(Convert.ToInt32(lb_khxd_AppraiseID.Text));

                        Page_Load(sender, e);
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('你是该项考核的起草人，不允许审批，只允许修改或删除！');</script>");
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请先从表中选择待办项');</script>");
                }
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");

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

        protected void btn_qckh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserLevelName"].ToString() == "管理员")
                {
                    throw new Exception("管理员主要用于管理信息平台数据，不允许发起流程！");
                }
                UI_disp_code = 1;
                cb_qckh_ksfz.Enabled = false;
                tbx_qckh_ksfz.Enabled = false;
                lb_qckh_yuan.Visible = false;
                tbx_qckh_ksfz.Text = "";
                dv_qicaokaohe.Visible = true;
                dv_gailan.Visible = false;

                btn_xgkh_ok.Enabled = false;
                btn_qckh_ok.Enabled = true;
                rbl_qckh_nextORprevious.Enabled = true;

                cb_qckh_is_huiqian.Enabled = true;


                qicaokaohe_init();
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");

            }

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
       
        protected void btn_qckh_ok_Click(object sender, EventArgs e)

        {
            //这部分需要完成以下操作：1、选择下一步经办人，保存所有考核信息。
            //在强制修改时，流程无需流转，应怎么样保存操作数据，操作记录应是怎么样？——虽然修改不流转，但有第三方强权角色参与调整，操作应有所记录。AppRun应有记录。
            //在强制流转时，流程无需修改任何考核内容，只是实现节点跳转，如何如何操作？
            string AppName_str = "";

            try
            {
                for (int i = 0; i < cbl_workers.Items.Count; i++)
                {

                    if (cbl_workers.Items[i].Selected)
                    {
                        AppName_str += cbl_workers.Items[i].Text.Trim() + " ";
                    }

                }
                if (tbx_qckh_FS_DateTime.Text == "" || tbx_qckh_FS_DateTime.Text == "正确格式:YYYY - MM - DD或YYYY / M / D")
                    throw new Exception("发生时间不允许为空");
                if (tbx_qckh_AppContent.Text == "")
                    throw new Exception("考核主题不允许为空");
                if (tbx_qckh_AppBy.Text == "")
                    throw new Exception("考核依据不允许为空");

                if (rbl_qckh_nextORprevious.SelectedIndex == -1)
                    throw new Exception("你还没选择流转方向");


                if (rbl_qckh_step.SelectedIndex == -1)

                    throw new Exception("没有选择下一步节点");

                if (cbl_qckh_next_persion.SelectedIndex == -1)
                    throw new Exception("没有选择下一步经办人");
                if (lb_qckh_AppAmount.Text == "0" || lb_qckh_AppAmount.Text == "0.00")
                    throw new Exception("本次考核金额不允许为0");
                //用于修正添加员工完后，再修改金额，造成总金额不对
                ds_appWorker = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text));
                decimal tmp_sum = 0;
                for (int i = 0; i < ds_appWorker.Tables[0].Rows.Count; i++)
                    if (ds_appWorker.Tables[0].Rows[i][10].ToString() != "")
                    {

                       gv_AppWorker.Rows[i].Cells[10].Controls[1].Visible = true;
                        ((TextBox)gv_AppWorker.Rows[i].Cells[10].Controls[1]).Text = ds_appWorker.Tables[0].Rows[i][10].ToString();
                        tmp_sum += Convert.ToDecimal(ds_appWorker.Tables[0].Rows[i][10].ToString());

                    }

                lb_qckh_AppAmount.Text = tmp_sum.ToString();
                //---------------------------------------------------------------------

                dv_qicaokaohe.Visible = false;
                dv_gailan.Visible = true;
                btn_appworker_add.Enabled = false;

                khgl_qichao.Update_AppRun(Convert.ToInt32(lb_qckh_AppraiseID.Text), "起草", Session["IDCard"].ToString(), "[ApproveOponion],[App_Comment],[Oponion_State],[Oponion_DateTime]",
                     khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 0)
                + "," + rbl_qckh_nextORprevious.SelectedItem.Text + ",getdate()", false);

                khgl_qichao.Update_AppraiseInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[Flow_State],[Applevel],[AppKind] ,[AppAmount] ,[TC_DateTime] ,[FS_DateTime],[AppGroup],[AppNames] ,[AppContent] ,[AppBy]", rbl_qckh_step.SelectedItem.Text
                + "," + ddl_qckh_Applevel.SelectedItem.Text.Trim() + "," + ddl_qckh_AppKind.SelectedItem.Text.Trim() + "," + lb_qckh_AppAmount.Text + ",getdate(),"
                + tbx_qckh_FS_DateTime.Text.Trim() + "," + ddl_qckh_AppGroup.SelectedItem.Text.Trim() + "," + AppName_str.Trim()
                + "," + khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 0));

                khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[FS_DateTime],[AppLevel],[AppKind],[AppContent],[AppBy]", tbx_qckh_FS_DateTime.Text
                + "," + ddl_qckh_Applevel.SelectedItem.Text.Trim() + "," + ddl_qckh_AppKind.SelectedItem.Text.Trim()
                 + "," + khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                 + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 0));

                //      khgl_qichao.update_flow(Convert.ToInt32(lb_qckh_AppraiseID.Text), lb_qckh_Flow_State.Text, Session["IDCard"].ToString(),
                //          khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                //    , khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 0)
                //, rbl_qckh_nextORprevious.SelectedItem.Text, ddl_qckh_Applevel.SelectedItem.Text.Trim(), ddl_qckh_AppKind.SelectedItem.Text.Trim(), lb_qckh_AppAmount.Text,
                //          tbx_qckh_FS_DateTime.Text.Trim(), ddl_qckh_AppGroup.SelectedItem.Text.Trim(), AppName_str.Trim());


                for (int i = 0; i < cbl_qckh_next_persion.Items.Count; i++)
                {
                    if (cbl_qckh_next_persion.Items[i].Selected)
                        khgl_qichao.insert_AppRun(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[Flow_State],[ApproveName],[ApproveIDCard],[Oponion_State]", rbl_qckh_step.SelectedItem.Text
                        + "," + cbl_qckh_next_persion.Items[i].Text.Trim() + "," + cbl_qckh_next_persion.Items[i].Value.Trim() + ",待办理");
                }
                
                UI_disp_code = 0;
                btn_search_Click(sender, e);
                Page_Load(sender, e);
                rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());

                //清除审批状态中的强制项
                if (ddl_shenpi_zt.Items.Count == 3)
                    ddl_shenpi_zt.Items.RemoveAt(2);

            }
            catch (Exception err)
            {
                dv_qicaokaohe.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");
              
            }

        }

        protected void btn_xgkh_ok_Click(object sender, EventArgs e)
        {
            string AppName_str = "";
            try
            {
                for (int i = 0; i < cbl_workers.Items.Count; i++)
                {

                    if (cbl_workers.Items[i].Selected)

                        AppName_str += cbl_workers.Items[i].Text.Trim() + " ";

                }
             
                string old_AppBy = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex*10*gv_App_gailan.PageIndex][13].ToString();

                if (tbx_qckh_FS_DateTime.Text == "" || tbx_qckh_FS_DateTime.Text == "正确格式:YYYY - MM - DD或YYYY / M / D")
                    throw new Exception("发生时间不允许为空");
                if (tbx_qckh_AppContent.Text == "")
                    throw new Exception("考核主题不允许为空");
                if (tbx_qckh_AppBy.Text == "")
                    throw new Exception("考核依据不允许为空");


                if (lb_qckh_AppAmount.Text == "0" || lb_qckh_AppAmount.Text == "0.00")
                    throw new Exception("本次考核金额不允许为0");
                //用于修正添加员工完后，再修改金额，造成总金额不对
                ds_appWorker = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text));
                decimal tmp_sum = 0;
                for (int i = 0; i < ds_appWorker.Tables[0].Rows.Count; i++)
                    if (ds_appWorker.Tables[0].Rows[i][10].ToString() != "")
                    {

                        gv_AppWorker.Rows[i].Cells[10].Controls[1].Visible = true;
                        ((TextBox)gv_AppWorker.Rows[i].Cells[10].Controls[1]).Text = ds_appWorker.Tables[0].Rows[i][10].ToString();
                        tmp_sum += Convert.ToDecimal(ds_appWorker.Tables[0].Rows[i][10].ToString());
                    }

                lb_qckh_AppAmount.Text = tmp_sum.ToString();
               //---------------------------------------------------------------------
                dv_qicaokaohe.Visible = false;
                dv_gailan.Visible = true;
                btn_appworker_add.Enabled = false;

                //三个表的原子操作如何保证？
                if( khgl_qichao.Update_AppRun(Convert.ToInt32(lb_qckh_AppraiseID.Text), lb_qckh_Flow_State.Text,
                    //gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].Text, 这个值由于界面优化被关闭
                    lb_khxd_ApplicantIDCard.Text,
                    "[ApproveOponion],[App_Comment],[Oponion_DateTime]",
                     khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 1)
                + ",getdate()", false)&&

                khgl_qichao.Update_AppraiseInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text),
                    "[Applevel],[AppKind] ,[AppAmount] ,[TC_DateTime] ,[FS_DateTime],[AppGroup],[AppNames] ,[AppContent] ,[AppBy]",
                    ddl_qckh_Applevel.SelectedItem.Text.Trim() + "," + ddl_qckh_AppKind.SelectedItem.Text.Trim() + "," + lb_qckh_AppAmount.Text + ",getdate(),"
                + tbx_qckh_FS_DateTime.Text.Trim() + "," + ddl_qckh_AppGroup.SelectedItem.Text.Trim() + "," + AppName_str.Trim()
                + "," + khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 1)) &&

                khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[FS_DateTime],[AppLevel],[AppKind],[AppContent],[AppBy]", tbx_qckh_FS_DateTime.Text
                + "," + ddl_qckh_Applevel.SelectedItem.Text.Trim() + "," + ddl_qckh_AppKind.SelectedItem.Text.Trim()
                 + "," + khgl_shenpi.convert_str(tbx_qckh_AppContent.Text, Session["RealName"].ToString(), 0)
                 + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text, Session["RealName"].ToString(), 1)))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('考核修改成功！');</script>");
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('脏数据清除失败请联系管理员反应情况，并关闭浏览器重新登陆，或者重新发起考核，并联系办事员处理无效考核流程。');</script>");
                    //if (khgl_shenpi.clear_error_data(Convert.ToInt32(lb_qckh_AppraiseID.Text)))

                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('修改失败，脏数据将被清除！');</script>");
                    //else
                    //    throw new Exception("脏数据清除失败请联系管理员反应情况，并关闭浏览器重新登陆，或者重新发起考核，并联系办事员处理无效考核流程。");
                }


                UI_disp_code = 0;
                rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());
                Page_Load(sender, e);
            }
            catch (Exception err)
            {
                dv_qicaokaohe.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "！');</script>");

            }

        }

        protected void btn_qckh_cancel_Click(object sender, EventArgs e)
        {
            dv_qicaokaohe.Visible = false;
            dv_gailan.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('操作取消数据未同步！');</script>");
            UI_disp_code = 0;

            btn_search_Click(sender, e);
            Page_Load(sender, e);
        }
        /// <summary>
        /// 用于提交审批结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_shenpi_ok_Click(object sender, EventArgs e)
        {
            string opt_fields = "";
            string old_shenpi_msg = "";

            try
            {
                switch (Session["UserLevelName"].ToString())
                {
                    //0管理员，1部长，2书记，3分管领导、4工程师、5分管组长、6点检、7、办事员 
                    //1部长
                    case "部长":
                        {
                            opt_fields = "[step_5_Oponion] ,[step_5_Comment]";
                            old_shenpi_msg = tbx_khxd_step_5_Comment.Text;
                            break;
                        }
                    //2书记，
                    case "书记":
                        {
                            opt_fields = "[step_4_Oponion],[step_4_Comment] ";
                            old_shenpi_msg = tbx_khxd_step_4_Comment.Text;
                            break;
                        }
                    //3主管领导
                    case "主管领导":
                        {
                            opt_fields = "[step_3_Oponion],[step_3_Comment]";
                            old_shenpi_msg = tbx_khxd_step_3_Comment.Text;
                            break;
                        }
                    //4工程师
                    case "工程师":
                        {
                            opt_fields = "[step_2_Oponion] ,[step_2_Comment]";
                            old_shenpi_msg = tbx_khxd_step_2_Comment.Text;
                            break;
                        }
                    //5白班段长
                    case "白班段长":
                        {
                            opt_fields = "[step_2_Oponion] ,[step_2_Comment]";
                            old_shenpi_msg = tbx_khxd_step_2_Comment.Text;
                            break;
                        }
                    //6点检
                    case "点检":
                        {

                            opt_fields = "[step_1_Oponion],[step_1_Comment] ";
                            old_shenpi_msg = tbx_khxd_Step_1_Comment.Text;
                            break;
                        }

                    ////7安全员 没有审批权
                    //case "安全员":
                    //    {
                    //        opt_fields = "[step_1_Oponion],[step_1_Comment] ";
                    //        old_shenpi_msg = tbx_khxd_Step_1_Comment.Text;
                    //        break;

                    //    }
                    //8办事员，有管理权限操作管理字段
                    case "办事员":
                        {
                            opt_fields = "[Admin_Opt],[Admin_Opt_Comment] ";
                            //old_shenpi_msg是字段Admin_Opt_Comment的信息
                            old_shenpi_msg = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[25].Text; ;
                            break;
                        }
                    //0管理员
                    case "管理员":
                        {
                            opt_fields = "[Admin_Opt],[Admin_Opt_Comment] ";
                            //old_shenpi_msg是字段Admin_Opt_Comment的信息
                            old_shenpi_msg = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[25].Text; ;
                            break;
                        }
                        //9其它 没有审批权
                        //case "其它":
                        //    {
                        //        opt_fields = "[step_1_Oponion],[step_1_Comment] ";
                        //        old_shenpi_msg = tbx_khxd_Step_1_Comment.Text;
                        //        break;
                        //    }
                        //10班组长 没有审批权
                        //case "班组长":
                        //    {
                        //        opt_fields = "[step_1_Oponion],[step_1_Comment] ";
                        //        old_shenpi_msg = tbx_khxd_Step_1_Comment.Text;
                        //        break;

                        //    }

                }

         

                //使用使用管理员强制特权时，设置未办理用户的状态，包括会签相关
                if (ddl_shenpi_zt.SelectedIndex.ToString() == "2")
                {
                    //---获取转交对象的第一个人名， cbl_shenpi_next_persion.Items[0].Text.Trim()，为保证准确，下面是暂用代码-----------
                    int j = 0;
                    for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)
                        if (cbl_shenpi_next_persion.Items[i].Selected)
                            j = i;
                    //------------------------------------------------------------------------------------------
                    khgl_shenpi.Update_AppraiseInfo(Convert.ToInt32(lb_khxd_AppraiseID.Text), "[Flow_State]," +opt_fields,
                    rbl_shenpi_step.SelectedItem.Text + "," + ddl_shenpi_zt.SelectedItem.Text + "," 
                    + old_shenpi_msg + khgl_shenpi.convert_str(tbx_shenpi_yj.Text+"至"+ rbl_shenpi_step.SelectedItem.Text
                    + cbl_shenpi_next_persion.Items[j].Text.Trim()+"办理 但 "+lb_shenpi_wei_huiqianren.Text + "未参与审批", Session["RealName"].ToString(), 3));

                    khgl_shenpi.Update_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text),
                        lb_khxd_Flow_State.Text,
                         Session["IDCard"].ToString(), "[App_Comment],[Oponion_State],[Oponion_DateTime]",
                         khgl_shenpi.convert_str("该人员未参与评审 考核被强制流转", Session["RealName"].ToString(), 3)
                         + ",(强制)" + rbl_shenpi_nextORprevious.SelectedItem.Text + ",getdate()", true);

                    //下面向库中插入下一步经办人
                    for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)
                    {
                        if (cbl_shenpi_next_persion.Items[i].Selected == true)
                            khgl_shenpi.insert_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), "[Flow_State],[ApproveName],[ApproveIDCard],[Oponion_State]"
                                , rbl_shenpi_step.SelectedItem.Text + "," + cbl_shenpi_next_persion.Items[i].Text.Trim() + "," + cbl_shenpi_next_persion.Items[i].Value.Trim() + ",待办理");
                    }
                    UI_disp_code = 0;

                }
                else
                { 
                   //非管理员强制进的处理
                if (lb_shenpi_shenpimoshi.Text.Trim() == "独立" 
                    || lb_shenpi_shenpimoshi.Text.Trim() == "会签" 
                    && (lb_shenpi_wei_huiqianren.Text == "空"
                    || (lb_shenpi_wei_huiqianren.Text != "空" && cb_shenpi_qzzj.Checked == true)
                    )
                    )
                {


                    if (tbx_shenpi_yj.Text == "")
                        throw new Exception("审批意见不允许为空");

                    if (rbl_shenpi_nextORprevious.SelectedIndex == -1)
                        throw new Exception("你还没选择流转方向");


                    if (rbl_shenpi_step.SelectedIndex == -1)

                        throw new Exception("没有选择下一步节点");

                    if (cbl_shenpi_next_persion.SelectedIndex == -1)
                        throw new Exception("没有选择下一步经办人");

                    //更新当前经办人数据
                    khgl_shenpi.Update_AppraiseInfo(Convert.ToInt32(lb_khxd_AppraiseID.Text), "[Flow_State]," + opt_fields, rbl_shenpi_step.SelectedItem.Text + "," + ddl_shenpi_zt.SelectedItem.Text
                   + "," + old_shenpi_msg + khgl_shenpi.convert_str(tbx_shenpi_yj.Text,Session["userlevelname"].ToString()+"  "+Session["RealName"].ToString(), 3));
                    khgl_shenpi.Update_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["UserLevelName"].ToString(),
                        Session["IDCard"].ToString(), "[ApproveOponion],[App_Comment],[Oponion_State],[Oponion_DateTime]", ddl_shenpi_zt.SelectedItem.Text
                        + "," + khgl_shenpi.convert_str(tbx_shenpi_yj.Text, Session["RealName"].ToString(), 3)
                        + "," + rbl_shenpi_nextORprevious.SelectedItem.Text + ",getdate()", false);


                    //------------------------
                    //在强制模式时，对其它会签人员办理状态的变更，以及示办理
                    if (lb_shenpi_wei_huiqianren.Text != "空" && cb_shenpi_qzzj.Checked)
                    {
                        khgl_shenpi.Update_AppraiseInfo(Convert.ToInt32(lb_khxd_AppraiseID.Text), opt_fields, ddl_shenpi_zt.SelectedItem.Text
                   + "," + old_shenpi_msg + khgl_shenpi.convert_str(tbx_shenpi_yj.Text, Session["RealName"].ToString(), 3) + " 但 " + lb_shenpi_wei_huiqianren.Text + " 未参与会签审批");

                        khgl_shenpi.Update_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["UserLevelName"].ToString(),
                             Session["IDCard"].ToString(), "[App_Comment],[Oponion_State],[Oponion_DateTime]",
                             khgl_shenpi.convert_str("该会签人员未参与评审 考核被强制流转", Session["RealName"].ToString(), 3)
                             + ",(强制)" + rbl_shenpi_nextORprevious.SelectedItem.Text + ",getdate()", cb_shenpi_qzzj.Checked);

                    }
                  


                    //------------------------
                    //下面向库中插入下一步经办人
                    for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)
                    {
                        if (cbl_shenpi_next_persion.Items[i].Selected==true)
                        khgl_shenpi.insert_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), "[Flow_State],[ApproveName],[ApproveIDCard],[Oponion_State]", rbl_shenpi_step.SelectedItem.Text
                                + "," + cbl_shenpi_next_persion.Items[i].Text.Trim() + "," + cbl_shenpi_next_persion.Items[i].Value.Trim() + ",待办理");
                    }
                    UI_disp_code = 0;

                }

                else

                if (lb_shenpi_shenpimoshi.Text.Trim() == "会签" && lb_shenpi_wei_huiqianren.Text != "空")
                {


                    khgl_shenpi.Update_AppraiseInfo(Convert.ToInt32(lb_khxd_AppraiseID.Text), opt_fields, ddl_shenpi_zt.SelectedItem.Text
                   + "," + old_shenpi_msg + khgl_shenpi.convert_str(Session["RealName"].ToString()+"的会签意见：" + ddl_shenpi_zt.SelectedItem.Text 
                   + "会签评论："+tbx_shenpi_yj.Text, Session["RealName"].ToString(), 3));
                    khgl_shenpi.Update_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["UserLevelName"].ToString(),
                        Session["IDCard"].ToString(), "[ApproveOponion],[App_Comment],[Oponion_State],[Oponion_DateTime]", ddl_shenpi_zt.SelectedItem.Text
                        + "," + khgl_shenpi.convert_str(tbx_shenpi_yj.Text, Session["RealName"].ToString(), 3)
                        + "," + rbl_shenpi_nextORprevious.SelectedItem.Text + ",getdate()", false);
                    UI_disp_code = 0;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('还有会签人员没有审理该考核。您签署的意见将被留存！');</script>");

      

                }
                }
                rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());
                Page_Load(sender, e);
            }

            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");


            }
        }



        protected void btn_shenpi_cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('审批操作取消，数据未同步！');</script>");
            UI_disp_code = 0;
            rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());
            Page_Load(sender, e);
        }

        protected void ddl_qckh_AppGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //这个方法同步填充被考核人员GRIDVIEW。编缉，但不同步到数据表，通过确定扫描GRIDVIEW 对应的单元格。
                string a = "";
                string b = "";
           dv_qicaokaohe.Visible = true;
            btn_appworker_add.Enabled = true;
            ds_appWorker = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text));

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
            for (int i=0; i < ds_appWorker.Tables[0].Rows.Count; i++)
                        {
                            gv_AppWorker.Rows[i].Cells[10].Text = ds_appWorker.Tables[0].Rows[i][10].ToString();                 
                        }

            ds_worker = khgl_qichao.select_WorkerInfo(ddl_qckh_AppGroup.SelectedItem.Text);
            cbl_workers.Items.Clear();
            for (int i = 0; i < ds_worker.Tables[0].Rows.Count; i++)
            {
                //cbl_workers.Items.Add(ds_worker.Tables[0].Rows[i][1].ToString());
                cbl_workers.Items.Add("");
                cbl_workers.Items[i].Text = ds_worker.Tables[0].Rows[i][1].ToString();
                cbl_workers.Items[i].Value = ds_worker.Tables[0].Rows[i][3].ToString();
                for (int j = 0; j < ds_appWorker.Tables[0].Rows.Count; j++)
                {
                    a = ds_appWorker.Tables[0].Rows[j][5].ToString();
                    b = ds_worker.Tables[0].Rows[i][1].ToString();
                    if (a == b)
                        cbl_workers.Items[i].Selected = true;
               
 }
            }

            btn_appworker_add.Enabled  = true;
            cb_qckh_ksfz.Enabled = true;
          
        }


        TextBox tb = new TextBox();
        Button bt = new Button();

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ((GridView)sender).Rows.Count; i++)
            {
                ((GridView)sender).Rows[i].BackColor = System.Drawing.Color.White;
                ((GridView)sender).EditIndex = -1;
              //  gv_AppWorker.Rows[i].Cells[10].Controls[3].Visible = false;
            }
            if (((GridView)sender).SelectedIndex >= 0)
            //表格表头索引是-1，要屏蔽
            {
                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;

                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[10].Controls[1].Visible = true;

             //   gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[10].Controls[3].Visible = true;
                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[10].Controls[1].Focus();
                btn_gv_AppMount_Update.Enabled = true;
            }
            else
                btn_gv_AppMount_Update.Enabled = false;

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
        public void qicaokaohe_init()
        {
            lb_qckh_AppraiseID.Text = Convert.ToString(khgl_qichao.build_newid(Session["RealName"].ToString(), Session["UserLevelName"].ToString(), Session["IDCard"].ToString()));
            lb_qckh_Flow_State.Text = Session["UserLevelName"].ToString();
            ddl_qckh_Applevel.SelectedIndex = 0;
            ddl_qckh_AppKind.SelectedIndex = 0;
            lb_qckh_ApplicantName.Text = Session["RealName"].ToString();
            lb_qckh_AppAmount.Text = "0";
            lb_qckh_TC_DateTime.Text = DateTime.Now.ToString();
            //存在问题，发生时间不应该自动采集
            tbx_qckh_FS_DateTime.ToolTip="参考格式"+  DateTime.Now.ToString().Substring(0,10);
            tbx_qckh_FS_DateTime.Text = "";

            tbx_qckh_AppContent.Text = "";
            tbx_qckh_AppBy.Text = "";

            cb_qckh_ksfz.Checked = false;
            gv_AppWorker.DataSource = null;
            gv_AppWorker.DataBind();

            ddl_qckh_AppGroup.SelectedIndex = -1;

            cbl_workers.Items.Clear();
            cbl_workers.DataBind();

            rbl_qckh_nextORprevious.SelectedIndex = -1;

            btn_gv_AppMount_Update.Enabled = false;
            cbl_qckh_next_persion.Items.Clear();
            cbl_qckh_next_persion.DataBind();
            cb_qckh_is_huiqian.Checked = false;
            rbl_qckh_step.Items.Clear();
            rbl_qckh_step.DataBind();

            tbx_qckh_ksfz.Text = "0";
            cb_qckh_ksfz.Checked = false;

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
            string tmp_value = "";
            decimal tmp_sum = 0;
            for (int j = 0; j < gv_AppWorker.Rows.Count; j++)
            {
                tmp_value = ((TextBox)gv_AppWorker.Rows[j].Cells[10].Controls[1]).Text.Trim();
                if (Regex.IsMatch(tmp_value, @"\d{1,}.\d{1,}|[-]\d{1,}.\d{1,}|\d{1,}|[-]\d{1,}") && (tmp_value != "" || tmp_value == "0"))
                {
                    if (khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), gv_AppWorker.Rows[j].Cells[7].Text.Trim(), "[AppAmount]", tmp_value))
                      //  gv_AppWorker.Rows[j].Cells[9].Controls[1].Visible = false;
                    tmp_sum += Convert.ToDecimal(((TextBox)gv_AppWorker.Rows[j].Cells[10].Controls[1]).Text.Trim());
                }
                else
                {
                    gv_AppWorker.BackColor = System.Drawing.Color.White;
                    gv_AppWorker.Rows[j].BackColor = System.Drawing.Color.Red;
                 //   gv_AppWorker.Rows[j].Cells[9].Controls[1].Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('录入金额字符存在异常！" + e.ToString() + "');</script>");
                }
            }
            lb_qckh_AppAmount.Text = Convert.ToString(tmp_sum);

        }


        protected void cbl_workers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //按CBL_WORKER选择人员向表GV_APPWORKER添减人员




        }

        protected void btn_appworker_add_Click(object sender, EventArgs e)
        {
           decimal  tmp_sum = 0;
           decimal  tmp_ksfz = 0;
            try
            {
                if (Regex.IsMatch(tbx_qckh_ksfz.Text, @"\d{1,}.\d{1,}|[-]\d{1,}.\d{1,}|\d{1,}|[-]\d{1,}") && (tbx_qckh_ksfz.Text != ""))
                {
                    tmp_ksfz = Convert.ToDecimal(tbx_qckh_ksfz.Text);
                }
                else
                {
                    throw new Exception("快速赋值金额填写格式错误！");
                }

                if (khgl_qichao.IsExists("[dzsw].[dbo].[Syl_AppWorkerinfo]", "[AppID] =" + Convert.ToInt32(lb_qckh_AppraiseID.Text)))
                {
                    khgl_qichao.delete_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text));
                }
                for (int i = 0; i < cbl_workers.Items.Count; i++)
                {
                    if (cbl_workers.Items[i].Selected)
                    {
                        if (lb_qckh_ApplicantName.Text == Session["RealName"].ToString().Trim())
                        {
                            khgl_qichao.insert_single_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), cbl_workers.Items[i].Value,
                                "[ApplicantName],[GroupName],[ApplicantIDCard],[AppName],[AppIDCard],[AppAmount],[App_State]",
                                Session["RealName"].ToString().Trim() + "," +ddl_qckh_AppGroup.SelectedItem.Text.Trim()+ "," + Session["IDCard"].ToString().Trim() + ","
                                + cbl_workers.Items[i].Text.Trim() + "," + cbl_workers.Items[i].Value + "," + tmp_ksfz + ",未生效");
                        }
                        else
                            //修改考核，不同之处是修改人可能是管理员或办事员
                            khgl_qichao.insert_single_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), cbl_workers.Items[i].Value,
                                "[ApplicantName],[GroupName],[ApplicantIDCard],[AppName],[AppIDCard],[AppAmount],[App_State]",
                                lb_qckh_ApplicantName.Text + "," + ddl_qckh_AppGroup.SelectedItem.Text.Trim() + "," + khgl_qichao.Get_idcard_str(lb_qckh_ApplicantName.Text) + ","
                                + cbl_workers.Items[i].Text.Trim() + "," + cbl_workers.Items[i].Value + "," + tmp_ksfz + ",未生效");


                    }
                }
                //考核员工的选取必须传入日期是什么意图，有可能是上次被考核的同一人，但如日期不同，则不会相同
                //计算被考核员工总考核金额
                ds_appWorker = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text));
                gv_AppWorker.DataSource = ds_appWorker;
                gv_AppWorker.DataBind();
                for (int i = 0; i < ds_appWorker.Tables[0].Rows.Count; i++)
                    if (ds_appWorker.Tables[0].Rows[i][10].ToString() != "")
                    {

                       gv_AppWorker.Rows[i].Cells[10].Controls[1].Visible = true;
                        
                        ((TextBox)gv_AppWorker.Rows[i].Cells[10].Controls[1]).Text = ds_appWorker.Tables[0].Rows[i][10].ToString();
                        tmp_sum += Convert.ToDecimal(ds_appWorker.Tables[0].Rows[i][10].ToString());
                    }

                lb_qckh_AppAmount.Text = tmp_sum.ToString();
                //--------------------------------------------------------------------------
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");
            }
        }

        protected void rbl_qckh_nextORprevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] step;

                if (rbl_qckh_nextORprevious.SelectedItem.Text == "转交")
                {
                    step = khgl_qichao.get_step_list(Session["userlevelname"].ToString(), rbl_qckh_nextORprevious.SelectedItem.Text, lb_qckh_Flow_State.Text);
                    if (step != null)
                    {
                        if (step.Length >= 1)
                            foreach (string tmp_str in step)
                            {
                                rbl_qckh_step.Items.Add(tmp_str);
                            }
                        rbl_qckh_step.DataBind();
                    }
                    else
                    {
                       throw new Exception("下一步为空，流程运转出错!请联系管理员！");
                    }
                }
                else
                {
                    throw new Exception("没选定下一步操作方式，请选定！");
                }
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('"+err.Message+"');</script>");

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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('只有在会签模式下才允许选择多个人！');</script>");
                    for (int i = 0; i < cbl_qckh_next_persion.Items.Count; i++)

                        cbl_qckh_next_persion.Items[i].Selected = false;
                }
            }
        }

        protected void cb_qckh_is_huiqian_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < cbl_qckh_next_persion.Items.Count; i++)

                cbl_qckh_next_persion.Items[i].Selected = false;
        }

        protected void rbl_gailan_cx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //设置WHERE条件
                string str_where = "";
                if (ddl_fs_or_tc.SelectedItem.Text == "提出时间")
                    //将库内日期为“2017-09”格式
                    str_where += "AND convert(NVARCHAR(7),[TC_DateTime],120)='" + ddl_year.SelectedItem.Text + "-" + ddl_month.SelectedItem.Text + "'";
                if (ddl_fs_or_tc.SelectedItem.Text == "发生时间")
                    str_where += "AND convert(NVARCHAR(7),[FS_DateTime],120)='" + ddl_year.SelectedItem.Text + "-" + ddl_month.SelectedItem.Text + "'";

                if (tbx_tcr_name.Text != "")
                    str_where += "and [ApplicantName] in (" + khgl_select.get_worker_name_str(tbx_tcr_name.Text.ToUpper().Trim()) + ")";
                if (tbx_appid.Text != "")
                {                 
                    str_where += "and [AppID]=" + tbx_appid.Text.Trim();
                }

                //开始重置数据集
                if (ds_AppraiseInfo != null)
                    if (ds_AppraiseInfo.Tables.Count > 0)
                        if (ds_AppraiseInfo.Tables[0].Rows.Count > 0)
                            ds_AppraiseInfo.Clear();
                gv_App_gailan.SelectedIndex = -1;
                gv_App_gailan.PageIndex = 0;
                //总览
                if (rbl_gailan_cx.SelectedIndex == 0)
                {
                    ds_AppraiseInfo = khgl_select.select_zhonglan(str_where);
                    //ds_AppraiseInfo = khgl_select.select_zhonglan(tbx_bg_time.Text, tbx_ed_time.Text);
                    btn_shenpikaohe.Visible = false;
                    btn_xgkh.Visible = false;
                    btn_khgd.Visible = false;
                    btn_sckh.Visible = false;
                    btn_qckh.Visible = true;
                    btn_qzsx.Visible = true;
                    dv_gailan.Visible = true;
                    dv_shenpi.Visible = false;
                    dv_khxd.Visible = false;
                }
                //待办
                if (rbl_gailan_cx.SelectedIndex == 1)
                {
                    ds_AppraiseInfo = khgl_select.select_daiban(Session["IDCARD"].ToString(), Session["UserLevelName"].ToString(), str_where);
                    btn_shenpikaohe.Visible = true;
                    btn_xgkh.Visible = true;
                    btn_khgd.Visible = true;
                    btn_sckh.Visible = true;
                    btn_qckh.Visible = true;
                    btn_qzsx.Visible = true;
                    dv_gailan.Visible = true;
                    dv_shenpi.Visible = false;
                    dv_khxd.Visible = false;
                }
                //已办结
                if (rbl_gailan_cx.SelectedIndex == 2)
                {
                    ds_AppraiseInfo = khgl_select.select_yibanjie(Session["IDCARD"].ToString(), Session["UserLevelName"].ToString(), str_where);

                    btn_shenpikaohe.Visible = false;
                    btn_xgkh.Visible = false;
                    btn_khgd.Visible = false;
                    btn_sckh.Visible = false;
                    btn_qckh.Visible = true;
                    btn_qzsx.Visible = true;
                    dv_shenpi.Visible = false;
                    dv_gailan.Visible = true;
                    dv_khxd.Visible = false;

                }

                if (ds_AppraiseInfo != null)
                {
                    if (ds_AppraiseInfo.Tables.Count > 0)
                        if (ds_AppraiseInfo.Tables[0].Rows.Count > 0)
                        {
                            //gv_App_gailan.PageIndex = 1;
                            gv_App_gailan.DataSource = ds_AppraiseInfo;

                            gv_App_gailan.DataBind();
                        }
                        else
                        {
                            gv_App_gailan.PageIndex = 0;
                            gv_App_gailan.DataSource = null;

                            gv_App_gailan.DataBind();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('没有相关数据！');</script>");
                            Page_Load(sender, e);
                        }


                }
                else
                {
                    gv_App_gailan.PageIndex = 0;
                    gv_App_gailan.DataSource = null;
                    gv_App_gailan.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('没有相关数据！');</script>");
                    Page_Load(sender, e);
                }
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('"+err.Message+"');</script>");
            }
        }

        protected void cb_qckh_ksfz_CheckedChanged(object sender, EventArgs e)
        {

          
            tbx_qckh_ksfz.Enabled = cb_qckh_ksfz.Checked;
            lb_qckh_yuan.Visible = cb_qckh_ksfz.Checked;

            if (cb_qckh_ksfz.Checked)
                btn_appworker_add.Enabled = true;
            else
                btn_appworker_add.Enabled = false;
        }

        protected void btn_xgkh_Click(object sender, EventArgs e)
        {
            try
            {
                //没选择待办项
                if (gv_App_gailan.SelectedIndex == -1)
                    throw new Exception("你没有选择待办项，或待办项为空");
                //不是办事员
                if (Session["UserLevelName"].ToString() != "办事员" )
                    //不是起草人本人
                    if(ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][4].ToString() != Session["IDCard"].ToString().Trim())
                    throw new Exception("如果你不是管理员或起草人本人，则不具备修改该考核的权限！");
                //流程没流转到用户角色位置
                if (gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text.Trim() != Session["UserLevelName"].ToString().Trim())
                    throw new Exception("该流程还未流转到你的角色，数据错误请联系管理员");
                btn_xgkh_ok.Enabled = true;
                btn_appworker_add.Enabled = false;
                cb_qckh_ksfz.Enabled  = false;             
                tbx_qckh_ksfz.Enabled = false;
                lb_qckh_yuan.Enabled = false;
                tbx_qckh_ksfz.Text = "";
                dv_qicaokaohe.Visible = true;
                dv_gailan.Visible = false;
                edit_kaohe_init(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text));
                btn_qckh_ok.Enabled = false;
                UI_disp_code = 1;
                btn_gv_AppMount_Update.Enabled = false;
            }
           catch (Exception err )
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('"+err.Message+"');</script>");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public void edit_kaohe_init(int AppID)
        {

            lb_qckh_AppraiseID.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text;
            lb_qckh_Flow_State.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text;
            lb_qckh_ApplicantName.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[3].Text;
            ddl_qckh_Applevel.SelectedItem.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][5].ToString();

            ddl_qckh_AppKind.SelectedItem.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][6].ToString();
            
            lb_qckh_AppAmount.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[7].Text;
            lb_qckh_TC_DateTime.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[8].Text;
            tbx_qckh_FS_DateTime.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[9].Text;
            foreach(ListItem a in  ddl_qckh_AppGroup.Items)
            {
                if (ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex + 10 * gv_App_gailan.PageIndex][10].ToString() == a.Text)
                    a.Selected = true;
                else
                    a.Selected = false;
            }
            cb_qckh_ksfz.Checked = false;
            ddl_qckh_AppGroup_SelectedIndexChanged(null,null);
            btn_appworker_add.Enabled = false;
            gv_AppWorker.Visible = true;
            btn_gv_AppMount_Update.Enabled = false;
            tbx_qckh_AppContent.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[12].Text;
            tbx_qckh_AppBy.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[13].Text;


            //rbl_qckh_nextORprevious.SelectedItem.Text= ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][14].ToString();

            //           cbl_qckh_next_persion.Items.Clear();
            //           cbl_qckh_next_persion.DataBind();
            //           cb_qckh_is_huiqian.Checked = false;

            //           rbl_qckh_step.Items.Clear();
            //           rbl_qckh_step.DataBind();

            //           tbx_qckh_ksfz.Text = "0";
            //           cb_qckh_ksfz.Checked = false;
            //cbl_workers.Items.Clear();
            //           cbl_workers.DataBind();
            //   gv_AppWorker.DataSource = null;
            //           gv_AppWorker.DataBind();


        }


        public void shenpikaohe_init(int AppID)
        {
            string shenpi_wei_huiqianren = "";
            lb_shenpi_kh_zhongjinger.Text = lb_khxd_AppAmount.Text;
            shenpi_wei_huiqianren = khgl_shenpi.select_wei_huiqianren(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["IDCard"].ToString());
            tbx_shenpi_yj.Text = "";
            cb_shenpi_is_huiqian.Checked = false;
            cb_shenpi_qzzj.Checked = false;
            rbl_shenpi_nextORprevious.SelectedIndex = -1;
            rbl_shenpi_step.Items.Clear();
            rbl_shenpi_step.Visible = false;
            cbl_shenpi_next_persion.Items.Clear();
            cbl_shenpi_next_persion.Visible = true;

            if (shenpi_wei_huiqianren != "空")
            {

                lb_shenpi_wei_huiqianren.Text = shenpi_wei_huiqianren;
            }
            else
            {
                lb_shenpi_wei_huiqianren.Text = "空";
            }
            if (khgl_shenpi.select_shenpi_renshu(Convert.ToInt32(lb_khxd_AppraiseID.Text), lb_khxd_Flow_State.Text.Trim()) > 1)
                lb_shenpi_shenpimoshi.Text = "会签";
            else
                lb_shenpi_shenpimoshi.Text = "独立";

        }


        protected void btn_sckh_Click(object sender, EventArgs e)
        {
            try
            {

                if (gv_App_gailan.SelectedIndex == -1)
                    throw new Exception("还没选择要删除的流程，请选择！");
                if ((Session["UserLevelName"].ToString() != "办事员") && (Session["UserLevelName"].ToString() != gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text))
                    throw new Exception("一般用户只允许在流程运转至起草阶段，才允许删除自己发的流程！请确认流程流转状态！");
                if ((Session["UserLevelName"].ToString() != "办事员") && Session["IDCard"].ToString() !=  gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].Text)
                    throw new Exception("一般用户只允许在流程运转至起草阶段，才允许删除自己发的流程！请确认自己是否为起草人！");

                if ( khgl_shenpi.selectitem_is_exists(Convert.ToInt16(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text.Trim()))==true)
                    if (khgl_shenpi.delete_AppFlow(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text)))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('编号：" + gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text + "的考核已经删除');</script>");
                        rbl_gailan_cx_SelectedIndexChanged(this,e);
                    }

                    else
                    {
                        throw new Exception("编号：" + gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text + "的考核删除出现意外");
                    }
               else
                {
                    throw new Exception("没有要删除的流程，请选择！或者编号：" + gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text + "的考核已经删除，请先择其它项！");
                }
             




            }
            catch (Exception err)
            {
                UI_disp_code = 0;
               
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");

            }
        }

       /// <summary>
       /// 些功能用于升效归档，存在的问题是只写入固定的信息没有与界面审批单形成关系
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void btn_khgd_Click(object sender, EventArgs e)
        {

            if (Session["UserLevelName"].ToString() == "办事员" )
            {
                if (ds_AppraiseInfo != null)
                    if (ds_AppraiseInfo.Tables.Count > 0)
                        if (ds_AppraiseInfo.Tables[0].Rows.Count > 0)
                        {
                            if (rbl_gailan_cx.SelectedItem.Text == "待办理")
                            {
                                for (int i = 0; i < gv_App_gailan.Rows.Count; i++)
                                {
                                    khgl_gl.guidang_AppFlow(Convert.ToInt32(ds_AppraiseInfo.Tables[0].Rows[i][1].ToString()), Session["IDCard"].ToString());
                                }
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('待办表中所有考核数据已经归档！');</script>");
                                rbl_gailan_cx_SelectedIndexChanged(this, e);
                            }


                            else
                            {

                                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('生效操作失败');</script>");

                            }

                        }
            }


            else

                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('你不具备操作生效的权限！');</script>");
            rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());
            UI_disp_code = 0;
        }

        protected void rbl_shenpi_nextORprevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] step;

            rbl_shenpi_step.Visible = false;
            cbl_shenpi_next_persion.Items.Clear();
            cbl_shenpi_next_persion.DataBind();
            cbl_shenpi_next_persion.Visible = false;
            rbl_shenpi_step.Items.Clear();
            rbl_shenpi_step.DataBind();
            switch (rbl_shenpi_nextORprevious.SelectedIndex)
            {
                //0转交
                case 0:
                    {
                        rbl_shenpi_step.Visible = true;

                        step = null;
                        step = khgl_shenpi.get_step_list(Session["userlevelname"].ToString(), rbl_shenpi_nextORprevious.SelectedItem.Text, gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text);
                        if (step != null)
                        {
                            if (step.Length > 0)
                                foreach (string tmp_str in step)
                                {
                                    rbl_shenpi_step.Items.Add(tmp_str);
                                }
                            rbl_shenpi_step.DataBind();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('下一步流转不能为空，流程运转出错！');</script>");
                        }
                        break;
                    }
                //1回退
                case 1:
                    {//从APPRUN表中获取该流程所经节点
                        step = null;
                        rbl_shenpi_step.Visible = true;

                        step = khgl_shenpi.get_step_list(Session["userlevelname"].ToString(), rbl_shenpi_nextORprevious.SelectedItem.Text, gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text);
                        if (step != null)
                        {
                            if (step.Length > 0)
                                foreach (string tmp_str in step)
                                {
                                    rbl_shenpi_step.Items.Add(tmp_str);
                                }
                            rbl_shenpi_step.DataBind();
                        }
                        else
                        {

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('回退步为空！');</script>");
                        }

                        break;
                    }
                //2：会签
                case 2:
                    {
                        rbl_shenpi_step.Visible = false;
                        cbl_shenpi_next_persion.Visible = false;
                        if (lb_shenpi_wei_huiqianren.Text != "空")
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('会签状态，审批意见只做存储，流程不向其它节点运转！如需强制转交，请选钩选强制模式！');</script>");
                        else
                        {
                            rbl_shenpi_nextORprevious.SelectedIndex = -1;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('本步骤没有其它经办要需要会签，请选择其它！');</script>");

                        }
                        break;
                    }
            }
        }

        protected void rbl_shenpi_step_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds_peoples;
            cbl_shenpi_next_persion.Visible = true;
            cbl_shenpi_next_persion.Items.Clear();
            cbl_shenpi_next_persion.DataBind();
            if (rbl_shenpi_nextORprevious.SelectedItem.Text != "" && rbl_shenpi_nextORprevious.SelectedItem.Text != "会签")
            {
                ds_peoples = khgl_qichao.get_jingbanren(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text), rbl_shenpi_nextORprevious.SelectedItem.Text, rbl_shenpi_step.SelectedItem.Text);
                cbl_shenpi_next_persion.Items.Clear();

                for (int i = 0; i < ds_peoples.Tables[0].Rows.Count; i++)
                {
                    cbl_shenpi_next_persion.Items.Add("");
                    cbl_shenpi_next_persion.Items[i].Text = ds_peoples.Tables[0].Rows[i][0].ToString();
                    cbl_shenpi_next_persion.Items[i].Value = ds_peoples.Tables[0].Rows[i][1].ToString();
                }
                cbl_shenpi_next_persion.DataBind();
            }
        }

        protected void cbl_shenpi_next_persion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel_count = 0;
            if(cbl_shenpi_next_persion.Items.Count==0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('没有人可供选择！');</script>");

            for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)
                if (cbl_shenpi_next_persion.Items[i].Selected)
                    sel_count++;
            if (cb_shenpi_is_huiqian.Checked == false)
            {
                if (sel_count > 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('只有在会签模式下才允许选择多个人！');</script>");
                    for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)

                        cbl_shenpi_next_persion.Items[i].Selected = false;
                }
            }
        }

        protected void cb_shenpi_is_huiqian_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)

                cbl_shenpi_next_persion.Items[i].Selected = false;
        }

        protected void btn_qzsx_Click(object sender, EventArgs e)
        {

            try
            {
                if (gv_App_gailan.SelectedIndex == -1)
                    throw new Exception("还没选择要生效的流程，请选择！");
                string weihuiqianren = khgl_gl.select_wei_huiqianren(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["IDCard"].ToString());
                string[] whqr = null;
                //未会签人，有两种情况，包含当前用户与不包含当前用户，封口可以理解为封别人的口，当管理员操作时，意味着封所在未办理人的口。


                if (ck_opt.item("强制生效", 1))
                {
                    if (ds_AppraiseInfo != null)
                        if (ds_AppraiseInfo.Tables.Count > 0)
                            if (ds_AppraiseInfo.Tables[0].Rows.Count > 0)
                            {
                                if (ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][2].ToString() != "生效")
                                {

                                    if (khgl_gl.select_shenpi_renshu(Convert.ToInt32(lb_khxd_AppraiseID.Text), lb_khxd_Flow_State.Text.Trim()) > 1
                                                                      && weihuiqianren != "空")
                                    {
                                        whqr = weihuiqianren.Split(',');
                                        for (int i = 0; i < whqr.Length; i++)
                                        {
                                            khgl_gl.weijingbanren_fengkou(Convert.ToInt32(ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][1].ToString()), khgl_gl.Get_idcard_str(whqr[i].Trim()), Session["IDCard"].ToString());
                                        }
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('有会签人员没有审理该考核。强制生效会忽略这些人员！');</script>");


                                    }


                                    if (khgl_gl.guidang_AppFlow(Convert.ToInt32(ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][1].ToString()), Session["IDCard"].ToString()))

                                        throw new Exception("编号：" +gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text + "已经生效");
                                    else

                                        throw new Exception("强制生效操作失败!");

                                }


                                else
                                {


                                    throw new Exception("该考核已经生效，无需再操作！");
                                }

                            }

                }


                else

                    throw new Exception("你的权限验证失效，请联系管理员！");


            }
            catch (Exception err)
            {
                UI_disp_code = 0;
                rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");
            }
        }

        protected void btn_qzsc_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_App_gailan.SelectedIndex == -1)
                    throw new Exception("还没选择要删除的流程，请选择！");
                if (ck_opt.item("强制删除", 1))
                {
                    if (ds_AppraiseInfo != null)
                        if (ds_AppraiseInfo.Tables.Count > 0)
                            if (ds_AppraiseInfo.Tables[0].Rows.Count > 0 && gv_App_gailan.SelectedIndex != -1)
                            {


                                if (khgl_shenpi.delete_AppFlow(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text)))
                                {

                                    throw new Exception("编号：" + gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text + "的考核已经删除");
                                }

                                else
                                {


                                    throw new Exception("编号：" + gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text + "的考核无法删除");
                                }

                            }
                            else
                            {

                                throw new Exception("没有要删除的流程，请选择！");
                            }
                }



            }
            catch (Exception err)
            {
                UI_disp_code = 0;
                Page_Load(sender, e);
                rbl_gailan_cx_SelectedIndexChanged(null, new EventArgs());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('" + err.Message + "');</script>");

            }

        }

        protected void btn_qzxg_Click(object sender, EventArgs e)
        {
            try
            {
                if (ck_opt.item("强制修改", 1) == false)
                    throw new Exception("你不具备权限使用该功能！");
                if (gv_App_gailan.SelectedIndex == -1)
                    throw new Exception("没有选择要修改的考核，请选择！");
                cb_qckh_ksfz.Enabled = false;
                tbx_qckh_ksfz.Enabled = false;
                lb_qckh_yuan.Visible = false;
                tbx_qckh_ksfz.Text = "";
                dv_qicaokaohe.Visible = true;
                dv_gailan.Visible = false;

                btn_xgkh_ok.Enabled = true;
                btn_qckh_ok.Enabled = false;
                rbl_qckh_nextORprevious.Enabled = false;
                btn_appworker_add.Enabled = false;
                cb_qckh_is_huiqian.Enabled = false;

                UI_disp_code = 1;

           
                edit_kaohe_init(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text));


                Page_Load(sender, e);
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + err.Message + "');</script>");
            }
        }
        protected void btn_qzzj_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserLevelName"].ToString() == "办事员")
                    throw new Exception("办事员由于角色特性，无法向任意步骤强制提交，请用管理员角色处理强制转交");

                if (ck_opt.item("强制转交", 1))
                {
                    if (gv_App_gailan.SelectedIndex != -1)
                    {
                        UI_disp_code = 2;
                        shenpikaohe_init(Convert.ToInt32(lb_khxd_AppraiseID.Text));

                        ddl_shenpi_zt.Items.Add("强制转交");
                        ddl_shenpi_zt.SelectedIndex = 2;
                        ddl_shenpi_zt.Enabled = false;
                        tbx_shenpi_yj.Text = "被：" + Session["RealName"].ToString() + " 强制转交";
                        tbx_shenpi_yj.Enabled = false;


                    }
                    else
                    {
                        UI_disp_code = 0;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请先从表中选择待办项');</script>");

                    }
                }
                Page_Load(sender, e);
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('" + err.Message + "');</script>");
            }
}
 

        protected void gv_App_gailan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
gv_App_gailan.PageIndex = e.NewPageIndex;
            gv_App_gailan.DataSource = ds_AppraiseInfo;
            gv_App_gailan.DataBind();
        }

        protected void btn_khql_Click(object sender, EventArgs e)
        {
            string str_appid = "";
            string str_appcontext = "";
            
            for (int i = 0; i < gv_App_gailan.Rows.Count; i++)
            {
               
               str_appid += gv_App_gailan.Rows[i].Cells[1].Text + "|";
               str_appcontext += gv_App_gailan.Rows[i].Cells[12].Text + "|";
            }
           
            if (gv_App_gailan.Rows.Count > 0)
            {
                str_appid=str_appid.Substring(0, str_appid.Length - 1);
                //Server.Transfer("clean_item_select.aspx?app_id=" + str_appid+"&appcontext="+str_appcontext,true);
          //   Server.Execute("clean_item_select.aspx?ds_ql_items=" + ds_AppraiseInfo, true);
            }
            else

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('要被清理删除的考核项为空');</script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('该功能尚不成熟，正处于测试阶段！');</script>");

            //khgl_gl.clean_kh(tbx_bg_time.Text, tbx_ed_time.Text);

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

            rbl_gailan_cx_SelectedIndexChanged(this,null);

        }

        protected void tbx_qckh_ksfz_TextChanged(object sender, EventArgs e)
        {
            btn_gv_AppMount_Update.Enabled = false;
        }
    }
}