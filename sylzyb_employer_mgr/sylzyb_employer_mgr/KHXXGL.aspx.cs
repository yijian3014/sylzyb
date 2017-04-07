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
        public static string sel_string = "select * from [dzsw].[dbo].[Syl_AppraiseInfo] where  TC_DateTime between  dateadd(month,-2,getdate()) and getdate()  order by Applevel AppKind desc, AppGroup,TC_DateTime";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        public static string lb;

        Check ck_opt = new Check();
        private int module_kind = 0;

        khgl khgl_qichao = new khgl();
        khgl khgl_select = new khgl();
        khgl khgl_shenpi = new khgl();
        DataSet ds_worker = new DataSet();
        DataSet ds_appWorker = new DataSet();
        DataSet ds_SylAppRun = new DataSet();
       static  DataSet ds_AppraiseInfo = new DataSet();

        public static int UI_disp_code = 0;



        protected void Page_Load(object sender, EventArgs e)
        {


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

                }
                else
                {
                    tbx_ed_time.Text = DateTime.Now.ToString();
                    tbx_bg_time.Text = DateTime.Now.AddMonths(-1).ToString();


                    login_user.Text = System.Web.HttpContext.Current.Session["RealName"].ToString();
                    ds_AppraiseInfo = khgl_select.select_zhonglan(tbx_bg_time.Text, tbx_ed_time.Text);
                    gv_App_gailan.DataSource = ds_AppraiseInfo;
                    gv_App_gailan.DataBind();
                    btn_sckh.Visible = false;
                    btn_khgd.Visible = false;
                    btn_xgkh.Visible = false;



                    UI_disp_code = 0;
                }

            }
            switch (UI_disp_code)
            {
                //登陆 查询 删除考核 归档考核、强制升效
                case 0:
                    {
                        rbl_gailan_cx.Focus();
                        dv_qicaokaohe.Visible = false;
                        dv_gailan.Visible = true;

                        dv_khxd.Visible = false;
                        dv_shenpi.Visible = false;



                      
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
                        break;
                    }          
            }

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
      考核依据: lb_khxd_AppBy
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
            if (ds_AppraiseInfo.Tables[0].Rows.Count > 0)
            {
                lb_khxd_AppraiseID.Text = ds_AppraiseInfo.Tables[0].Rows[0][1].ToString();
                lb_khxd_Flow_State.Text = ds_AppraiseInfo.Tables[0].Rows[0][2].ToString();
                lb_khxd_ApplicantName.Text = ds_AppraiseInfo.Tables[0].Rows[0][3].ToString();
                lb_khxd_ApplicantIDCard.Text = ds_AppraiseInfo.Tables[0].Rows[0][4].ToString();
                lb_khxd_AppKind.Text = ds_AppraiseInfo.Tables[0].Rows[0][5].ToString();
                lb_khxd_AppAmount.Text = ds_AppraiseInfo.Tables[0].Rows[0][6].ToString();
                lb_khxd_TC_DateTime.Text = ds_AppraiseInfo.Tables[0].Rows[0][7].ToString();
                lb_khxd_FS_DateTime.Text = ds_AppraiseInfo.Tables[0].Rows[0][8].ToString();
                lb_khxd_AppGroup.Text = ds_AppraiseInfo.Tables[0].Rows[0][9].ToString();
                lb_khxd_AppNames.Text = ds_AppraiseInfo.Tables[0].Rows[0][10].ToString();
                lb_khxd_AppContent.Text = ds_AppraiseInfo.Tables[0].Rows[0][12].ToString();
                lb_khxd_AppBy.Text = ds_AppraiseInfo.Tables[0].Rows[0][11].ToString();
                lb_khxd_step_1_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[0][13].ToString();
                tbx_khxd_step_1_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[0][14].ToString();
                lb_khxd_step_2_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[0][15].ToString();
                tbx_khxd_step_2_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[0][16].ToString();
                lb_khxd_step_3_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[0][17].ToString();
                tbx_khxd_step_3_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[0][18].ToString();
                lb_khxd_step_4_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[0][19].ToString();
                tbx_khxd_step_4_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[0][20].ToString();
                lb_khxd_step_5_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[0][21].ToString();
                tbx_khxd_step_5_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[0][22].ToString();

            }*/

            if (gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text != "")
            {
                lb_khxd_AppraiseID.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text;
                lb_khxd_Flow_State.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text;
                lb_khxd_ApplicantName.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[3].Text;
                lb_khxd_ApplicantIDCard.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].Text;
                lb_khxd_Applevel.Text= gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[5].Text;
                lb_khxd_AppKind.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[6].Text;
                lb_khxd_AppAmount.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[7].Text;
                lb_khxd_TC_DateTime.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[8].Text;
                lb_khxd_FS_DateTime.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[9].Text;
                lb_khxd_AppGroup.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[10].Text;
                lb_khxd_AppNames.Text = gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[11].Text;
                lb_khxd_AppContent.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][12].ToString();
                lb_khxd_AppBy.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][13].ToString();
                lb_khxd_step_1_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][14].ToString();
                tbx_khxd_Step_1_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][15].ToString();
                lb_khxd_step_2_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][16].ToString();
                tbx_khxd_step_2_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][17].ToString();
                lb_khxd_step_3_Oponion.Text= ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][18].ToString();
                tbx_khxd_step_3_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][19].ToString();
                lb_khxd_step_4_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][20].ToString();
                tbx_khxd_step_4_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][21].ToString();
                lb_khxd_step_5_Oponion.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][22].ToString();
                tbx_khxd_step_5_Comment.Text = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][23].ToString();
            }
            ds_appWorker = khgl_select.select_appworkerinfo(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].Text), tbx_bg_time.Text, tbx_ed_time.Text);
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

        protected void btn_shenpikaohe_Click(object sender, EventArgs e)
        {


            if (gv_App_gailan.SelectedIndex != -1)
            {
                UI_disp_code = 2;
                shenpikaohe_init(Convert.ToInt32(lb_khxd_AppraiseID.Text));
                Page_Load(sender, e);

            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('请先从表中选择待办项');</script>");
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
            UI_disp_code = 1;
            cb_qckh_ksfz.Visible = false;
            tbx_qckh_ksfz.Visible = false;
            tbx_qckh_ksfz.Enabled = false;
            lb_qckh_yuan.Visible = false;
            tbx_qckh_ksfz.Text = "";
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
                    rbl_gailan_cx_SelectedIndexChanged(sender, e);
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

            string AppName_str = "";
            for (int i = 0; i < cbl_workers.Items.Count; i++)
            {
                if (cbl_workers.Items[i].Selected)
                    AppName_str += cbl_workers.Items[i].Text.Trim() + " ";
            }
            if ((rbl_qckh_nextORprevious.SelectedIndex != -1) && (cbl_qckh_next_persion.SelectedIndex != -1) && (AppName_str != "" && rbl_qckh_step.SelectedIndex != -1))
            {
                dv_qicaokaohe.Visible = false;
                dv_gailan.Visible = true;
                btn_appworker_add.Visible = false;

                khgl_qichao.Update_AppRun(Convert.ToInt32(lb_qckh_AppraiseID.Text), lb_qckh_Flow_State.Text, Session["IDCard"].ToString(), "[ApproveOponion],[App_Comment],[Oponion_State],[Oponion_DateTime]",
                     khgl_shenpi.convert_str(tbx_qckh_AppContent.Text.Replace(",", "+char(44)+"), Session["RealName"].ToString(), 0)
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text.Replace(",","+char(44)+"), Session["RealName"].ToString(), 0)
                + "," + rbl_qckh_nextORprevious.SelectedItem.Text + ",getdate()", 0);

                khgl_qichao.Update_AppraiseInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[Flow_State],[Applevel],[AppKind] ,[AppAmount] ,[TC_DateTime] ,[FS_DateTime],[AppGroup],[AppNames] ,[AppContent] ,[AppBy]", rbl_qckh_step.SelectedItem.Text
                    + "," + ddl_qckh_Applevel.SelectedItem.Text.Trim() + "," + ddl_qckh_AppKind.SelectedItem.Text.Trim() + "," + lb_qckh_AppAmount.Text + ",getdate(),"
                    + tbx_qckh_FS_DateTime.Text.Trim() + "," + ddl_qckh_AppGroup.SelectedItem.Text.Trim() + "," + AppName_str.Trim()
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppContent.Text.Replace(",", "+char(44)+"), Session["RealName"].ToString(), 0)
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text.Replace(",", "+char(44)+"), Session["RealName"].ToString(), 0));

                khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[FS_DateTime],[AppLevel],[AppKind],[AppContent],[AppBy]", tbx_qckh_FS_DateTime.Text
                   + "," + ddl_qckh_Applevel.SelectedItem.Text.Trim() + "," + ddl_qckh_AppKind.SelectedItem.Text.Trim()
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppContent.Text.Replace(",", "+char(44)+"), Session["RealName"].ToString(), 0)
                    + "," + khgl_shenpi.convert_str(tbx_qckh_AppBy.Text.Replace(",", "+char(44)+"), Session["RealName"].ToString(), 0));
                for (int i = 0; i < cbl_qckh_next_persion.Items.Count; i++)
                {
                    if (cbl_qckh_next_persion.Items[i].Selected)
                        khgl_qichao.insert_AppRun(Convert.ToInt32(lb_qckh_AppraiseID.Text), "[Flow_State],[ApproveName],[ApproveIDCard],[Oponion_State]", rbl_qckh_step.SelectedItem.Text
                        + "," + cbl_qckh_next_persion.Items[i].Text.Trim() + "," + cbl_qckh_next_persion.Items[i].Value.Trim() + ",待办理");
                }
                UI_disp_code = 0;
                Page_Load(sender, e);
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('可能的错误有：1，未指定下一步转交内容。2，未指定下一步经办人员。3，被考核人员，不允许为空！');</script>");
        }

        protected void btn_qckh_cancel_Click(object sender, EventArgs e)
        {
            dv_qicaokaohe.Visible = false;
            dv_gailan.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('操作取消数据未同步！该功能尚未完善!" + e.ToString() + "');</script>");
            UI_disp_code = 0;
            Page_Load(sender, e);

        }

        protected void btn_shenpi_ok_Click(object sender, EventArgs e)
        {
            string opt_fields = "";
            string old_shenpi_msg = "";


            switch (Convert.ToUInt32(Session["UserLevel"].ToString()))
            {
                //0管理员，1部长，2书记，3分管领导、4工程师、5分管组长、6点检、7、办事员

                ////case 0:
                //    {
                //        opt_fields = "[step_1_Oponion],[step_1_Comment] ,[step_2_Oponion] ,[step_2_Comment] ,[step_3_Oponion],[step_3_Comment],[step_4_Oponion],[step_4_Comment] ,[step_5_Oponion] ,[step_5_Comment]";

                //        break;
                //    }
                //1部长
                case 1:
                    {
                        opt_fields = "[step_5_Oponion] ,[step_5_Comment]";
                        old_shenpi_msg = tbx_khxd_step_5_Comment.Text;
                        break;
                    }
                //2书记，
                case 2:
                    {
                        opt_fields = "[step_4_Oponion],[step_4_Comment] ";
                        old_shenpi_msg = tbx_khxd_step_4_Comment.Text;
                        break;
                    }
                //3分管领导
                case 3:
                    {
                        opt_fields = "[step_3_Oponion],[step_3_Comment]";
                        old_shenpi_msg = tbx_khxd_step_3_Comment.Text;
                        break;
                    }
                //4工程师
                case 4:
                    {
                        opt_fields = "[step_2_Oponion] ,[step_2_Comment]";
                        old_shenpi_msg = tbx_khxd_step_2_Comment.Text;
                        break;
                    }
                //5分管组长
                case 5:
                    {
                        opt_fields = "[step_1_Oponion],[step_1_Comment] ";
                        old_shenpi_msg = tbx_khxd_Step_1_Comment.Text;
                        break;
                    }
                ////6点检
                //case 6:
                //    {
                //        opt_fields = "[AppContent],[AppBy]";
                //        old_shenpi_msg = tbx_khxd_step_5_Comment.Text;
                //        break;
                //    }

                //7办事员
                case 7:
                    {
                        opt_fields = "[Admin_Opt] ,[Admin_Opt_Comment]";
                        old_shenpi_msg = ds_AppraiseInfo.Tables[0].Rows[gv_App_gailan.SelectedIndex][24].ToString();
                        break;
                    }
            }

            if (lb_shenpi_shenpimoshi.Text.Trim() == "独立" || lb_shenpi_shenpimoshi.Text.Trim() == "会签" && (lb_shenpi_wei_huiqianren.Text == "空" || (lb_shenpi_wei_huiqianren.Text != "空" && cb_shenpi_qzzj.Checked == true)))
            {
                if ((rbl_shenpi_nextORprevious.SelectedIndex != -1) && (cbl_shenpi_next_persion.SelectedIndex != -1) && (rbl_shenpi_step.SelectedIndex != -1))
                {
                    khgl_shenpi.Update_AppraiseInfo(Convert.ToInt32(lb_khxd_AppraiseID.Text), "[Flow_State]," + opt_fields, rbl_shenpi_step.SelectedItem.Text + "," + ddl_shenpi_zt.SelectedItem.Text
                    + "," + old_shenpi_msg + khgl_shenpi.convert_str(tbx_shenpi_yj.Text, Session["RealName"].ToString(), 1));
                    khgl_shenpi.Update_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["UserLevelName"].ToString(), Session["IDCard"].ToString(), "[ApproveOponion],[App_Comment],[Oponion_State],[Oponion_DateTime]", ddl_shenpi_zt.SelectedItem.Text
                        + "," + khgl_shenpi.convert_str(tbx_shenpi_yj.Text, Session["RealName"].ToString(), 1) + ",转交,getdate()", 0);
                    //------------------------
                    //在强制模式时，要不要加入对其它会签人员办理状态的变更？
                    if (cb_shenpi_qzzj.Checked)
                        khgl_shenpi.Update_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["UserLevelName"].ToString(),
                            Session["IDCard"].ToString(), "[App_Comment],[Oponion_State],[Oponion_DateTime]",
                            khgl_shenpi.convert_str("该会签人员未参与评审考核被强制转交", Session["RealName"].ToString(), 1) + ",(强制)转交,getdate()".Replace(",","+char(44)+"), 1);


                    //------------------------
                    //下面向库中插入下一步经办人
                    for (int i = 0; i < cbl_shenpi_next_persion.Items.Count; i++)
                    {
                        khgl_shenpi.insert_AppRun(Convert.ToInt32(lb_khxd_AppraiseID.Text), "[Flow_State],[ApproveName],[ApproveIDCard],[Oponion_State]", rbl_shenpi_step.SelectedItem.Text
                                + "," + cbl_shenpi_next_persion.Items[i].Text.Trim() + "," + cbl_shenpi_next_persion.Items[i].Value.Trim() + ",待办理");
                    }
                    UI_disp_code = 0;
                    Page_Load(sender, e);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('可能的错误有：1，未指定下一步转交内容。2，未指定下一步经办人员。3，被考核人员，不允许为空！');</script>");
                }
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('有会签人员没有审理该考核。要强制转交请选择“强制”模式。');</script>");

        }

        protected void btn_shenpi_cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('审批操作取消数据未同步！该功能尚未完善!" + e.ToString() + "');</script>");
            UI_disp_code = 0;
            Page_Load(sender,e);
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
            cb_qckh_ksfz.Visible = true;
            tbx_qckh_ksfz.Visible = true;
        }


        TextBox tb = new TextBox();
        Button bt = new Button();

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ((GridView)sender).Rows.Count; i++)
            {
                ((GridView)sender).Rows[i].BackColor = System.Drawing.Color.White;
                ((GridView)sender).EditIndex = -1;
                gv_AppWorker.Rows[i].Cells[9].Controls[3].Visible = false;
            }
            if (((GridView)sender).SelectedIndex >= 0)
            //表格表头索引是-1，要屏蔽
            {
                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;

                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[9].Controls[1].Visible = true;

                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[9].Controls[3].Visible = true;
                gv_AppWorker.Rows[gv_AppWorker.SelectedIndex].Cells[9].Controls[1].Focus();

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
            ddl_qckh_Applevel.SelectedIndex = 0;
            ddl_qckh_AppKind.SelectedIndex = 0;
            lb_qckh_ApplicantName.Text = Session["RealName"].ToString();
            lb_qckh_AppAmount.Text = "0";
            lb_qckh_TC_DateTime.Text = DateTime.Now.ToString();
            tbx_qckh_FS_DateTime.Text = DateTime.Now.ToString();

            tbx_qckh_AppContent.Text = "";
            tbx_qckh_AppBy.Text = "";
            
            gv_AppWorker.DataSource = null;
            gv_AppWorker.DataBind();

            ddl_qckh_AppGroup.SelectedIndex = -1;

            cbl_workers.Items.Clear();
            cbl_workers.DataBind();

            rbl_qckh_nextORprevious.SelectedIndex = -1;
            

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
                tmp_value = ((TextBox)gv_AppWorker.Rows[j].Cells[9].Controls[1]).Text.Trim();
                if (Regex.IsMatch(tmp_value, @"\d{1,}.\d{1,}|[-]\d{1,}.\d{1,}|\d{1,}|[-]\d{1,}") && (tmp_value != "" || tmp_value == "0"))
                {
                    if (khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), gv_AppWorker.Rows[j].Cells[6].Text.Trim(), "[AppAmount]", tmp_value))
                        gv_AppWorker.Rows[j].Cells[9].Controls[3].Visible = false;
                    tmp_sum += Convert.ToDecimal(((TextBox)gv_AppWorker.Rows[j].Cells[9].Controls[1]).Text.Trim());
                }
                else
                {
                    gv_AppWorker.BackColor = System.Drawing.Color.White;
                    gv_AppWorker.Rows[j].BackColor = System.Drawing.Color.Red;
                    gv_AppWorker.Rows[j].Cells[9].Controls[3].Visible = true;
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
            decimal tmp_sum = 0;
            decimal tmp_ksfz = 0;
            if (Regex.IsMatch(tbx_qckh_ksfz.Text, @"\d{1,}.\d{1,}|[-]\d{1,}.\d{1,}|\d{1,}|[-]\d{1,}") && (tbx_qckh_ksfz.Text != ""))
            {
                tmp_ksfz = Convert.ToDecimal(tbx_qckh_ksfz.Text);
            }
            else
            {
                tmp_ksfz = 0;
            }



            for (int i = 0; i < cbl_workers.Items.Count; i++)
            {
                if (cbl_workers.Items[i].Selected)
                {
                    if (khgl_qichao.IsExists("[dzsw].[dbo].[Syl_AppWorkerinfo]", "[AppID] =" + Convert.ToInt32(lb_qckh_AppraiseID.Text) + " and [AppIDCard]='" + cbl_workers.Items[i].Value + "'") == false)
                    {
                        khgl_qichao.insert_single_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), cbl_workers.Items[i].Value, "[ApplicantName],[ApplicantIDCard],[AppName],[AppIDCard],[AppAmount],[App_State]", Session["RealName"].ToString().Trim()
                          + "," + Session["IDCard"].ToString().Trim() + "," + cbl_workers.Items[i].Text.Trim() + "," + cbl_workers.Items[i].Value + "," + tmp_ksfz + ",未生效");
                    }
                    else
                    {
                        khgl_qichao.Update_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), cbl_workers.Items[i].Value, "[AppAmount]", tmp_ksfz.ToString());
                    }
                }
                else
                    khgl_qichao.delete_AppWorkerInfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), cbl_workers.Items[i].Value);
            }
            ds_appWorker = khgl_qichao.select_appworkerinfo(Convert.ToInt32(lb_qckh_AppraiseID.Text), tbx_bg_time.Text, tbx_ed_time.Text);
            gv_AppWorker.DataSource = ds_appWorker;
            gv_AppWorker.DataBind();
            for (int i = 0; i < ds_appWorker.Tables[0].Rows.Count; i++)
                if (ds_appWorker.Tables[0].Rows[i][9].ToString() != "")
                {

                    gv_AppWorker.Rows[i].Cells[9].Controls[1].Visible = true;
                    ((TextBox)gv_AppWorker.Rows[i].Cells[9].Controls[1]).Text = ds_appWorker.Tables[0].Rows[i][9].ToString();
                    tmp_sum += Convert.ToDecimal(ds_appWorker.Tables[0].Rows[i][9].ToString());
                }

            lb_qckh_AppAmount.Text = tmp_sum.ToString();
        }


        protected void rbl__qckh_nextORprevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] step;
             
            if (rbl_qckh_nextORprevious.SelectedItem.Text == "转交")
            {
                step = khgl_qichao.get_step_list(Convert.ToInt32(Session["userlevel"].ToString()), rbl_qckh_nextORprevious.SelectedItem.Text, lb_qckh_Flow_State.Text);
                if (step != null)
                {
                    if (step.Length > 1)
                        foreach (string tmp_str in step)
                        {
                            rbl_qckh_step.Items.Add(tmp_str);
                        }
                    rbl_qckh_step.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('下一步为空，流程运转出错！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('起草人删除考核功能未完善');</script>");
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
   if(ds_AppraiseInfo!=null)
                if(ds_AppraiseInfo.Tables.Count>0)
                    if(ds_AppraiseInfo.Tables[0].Rows.Count>0)
            ds_AppraiseInfo.Clear();
            if (rbl_gailan_cx.SelectedIndex == 0)
            {
                ds_AppraiseInfo = khgl_select.select_zhonglan(tbx_bg_time.Text, tbx_ed_time.Text);
                btn_shenpikaohe.Visible = false;
                btn_xgkh.Visible = false;
                btn_khgd.Visible = false;
                btn_sckh.Visible = false;
                btn_qckh.Visible = true;
                btn_qzsx.Visible = true;
                dv_gailan.Visible = true;
                dv_shenpi.Visible = false;
            }
            if (rbl_gailan_cx.SelectedIndex == 1)
            {
                ds_AppraiseInfo = khgl_select.select_daiban(tbx_bg_time.Text, tbx_ed_time.Text, Session["IDCARD"].ToString(), Session["UserLevelName"].ToString());
                btn_shenpikaohe.Visible = true;
                btn_xgkh.Visible = true;
                btn_khgd.Visible = true;
                btn_sckh.Visible = true;
                btn_qckh.Visible = true;
                btn_qzsx.Visible = true;
                dv_gailan.Visible = false;
                dv_shenpi.Visible = false;
            }
            if (rbl_gailan_cx.SelectedIndex == 2)
            {
                ds_AppraiseInfo = khgl_select.select_yibanjie(tbx_bg_time.Text, tbx_ed_time.Text, Session["IDCARD"].ToString(), Session["UserLevelName"].ToString());

                btn_shenpikaohe.Visible = false;
                btn_xgkh.Visible = false;
                btn_khgd.Visible = false;
                btn_sckh.Visible = false;
                btn_qckh.Visible = true;
                btn_qzsx.Visible = true;
                dv_shenpi.Visible = false;
                dv_gailan.Visible = true;

            }

            if (ds_AppraiseInfo != null)
            {
                if (ds_AppraiseInfo.Tables.Count > 0)
                    if (ds_AppraiseInfo.Tables[0].Rows.Count > 0)
                    {
                        gv_App_gailan.DataSource = ds_AppraiseInfo;
                        gv_App_gailan.DataBind();
                    }
                else
                    {
                        gv_App_gailan.DataSource = "";
                        gv_App_gailan.DataBind();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('没有相关数据！');</script>");
                        
                        Page_Load(sender,e);
                    }


            }
            else
            {
                gv_App_gailan.DataSource = "";
                gv_App_gailan.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('没有相关数据！');</script>");
                Page_Load(sender, e);
            }
        }

        protected void cb_qckh_ksfz_CheckedChanged(object sender, EventArgs e)
        {

            tbx_qckh_ksfz.Visible = cb_qckh_ksfz.Checked;
            tbx_qckh_ksfz.Enabled = cb_qckh_ksfz.Checked;
            lb_qckh_yuan.Visible = cb_qckh_ksfz.Checked;

        }

        protected void btn_xgkh_Click(object sender, EventArgs e)
        {

            cb_qckh_ksfz.Visible = false;
            tbx_qckh_ksfz.Visible = false;
            tbx_qckh_ksfz.Enabled = false;
            lb_qckh_yuan.Visible = false;
            tbx_qckh_ksfz.Text = "";
            dv_qicaokaohe.Visible = true;
            dv_gailan.Visible = false;
            if ((Convert.ToInt32(Session["UserLevel"].ToString()) == 0) || (Convert.ToInt32(Session["UserLevel"].ToString()) == 7) || 
                (gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].ToString() == Session["IDCard"].ToString())&&
                gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].ToString()== Session["UserLevelName"].ToString())
            {
                edit_kaohe_init(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[4].ToString()))
            }


        }
        public bool edit_kaohe_init(int AppID)
        {
           

            return true;
        }


        public void shenpikaohe_init(int AppID)
        {
            string shenpi_wei_huiqianren = "";
            lb_shenpi_kh_zhongjinger.Text = lb_khxd_AppAmount.Text;
            shenpi_wei_huiqianren = khgl_shenpi.select_wei_huiqianren(Convert.ToInt32(lb_khxd_AppraiseID.Text), Session["IDCard"].ToString());
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
            if( khgl_shenpi. delete_AppFlow(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].ToString())))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('编号："+gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].ToString()+"的考核已经删除');</script>");
            else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert(编号：" + gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].ToString() + "的考核无法删除');</script>");


        }


        protected void btn_khgd_Click(object sender, EventArgs e)
        {
            if (khgl_shenpi.guidang_AppFlow(Convert.ToInt32(gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[1].ToString())))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('待办表中所有考核数据已经归档！');</script>");
            else

                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('待办表中考核数据归档失败！');</script>");

        }

        protected void rbl_shenpi_nextORprevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] step;
             
            rbl_shenpi_step.Visible = false;
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
                        step = khgl_shenpi.get_step_list(Convert.ToInt32(Session["userlevel"].ToString()), rbl_shenpi_nextORprevious.SelectedItem.Text, gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text);
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
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('下一步为空，流程运转出错！');</script>");
                        }
                        break;
                    }
                //1回退
                case 1:
                    {//从APPRUN表中获取该流程所经节点
                        step = null;
                        rbl_shenpi_step.Visible = true;
                       
                        step = khgl_shenpi.get_step_list(Convert.ToInt32(Session["userlevel"].ToString()), rbl_shenpi_nextORprevious.SelectedItem.Text, gv_App_gailan.Rows[gv_App_gailan.SelectedIndex].Cells[2].Text);
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
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('回退步为空，流程运转出错！');</script>");
                        }

                        break;
                    }
                //2：会签
                case 2:
                    {
                        rbl_shenpi_step.Visible = false;
                        cbl_shenpi_next_persion.Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('会签状态，审批意见只做存储，流程不向其它节点运转！');</script>");
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
            if (rbl_shenpi_nextORprevious.SelectedItem.Text != ""&& rbl_shenpi_nextORprevious.SelectedItem.Text != "会签")
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
    }
}