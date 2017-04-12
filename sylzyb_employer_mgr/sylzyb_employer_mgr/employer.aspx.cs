using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

/*
界面权限定义
员工信息查询：2
添加员工信息：3
编辑员工信息：5
删除员工信息：7

*/
namespace sylzyb_employer_mgr
{
    public partial class employer_mgr : System.Web.UI.Page
    {
        public Check option_ck = new Check();
        public static string sel_string = "";
        public static string option_sql = "";
        public db db_opt = new db();
        public SqlDataReader dr_select_row;
        public DataSet ds = new DataSet();
        static string option_method = "";
        private int module_kind = 0;
        private int item_kind = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (option_ck.Module("员工信息管理", module_kind) == false || System.Web.HttpContext.Current.Session["UserName"].ToString() == "" || System.Web.HttpContext.Current.Session["IDCard"] == null)
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
                    login_user.Text = System.Web.HttpContext.Current.Session["RealName"].ToString();
                    sel_string = "select * from [dzsw].[dbo].[Syl_WorkerInfo] ";
                }
            }
            btn_emp_add.Visible = option_ck.item("添加员工信息", item_kind);
            btn_emp_edt.Visible = option_ck.item("修改员工信息", item_kind);
            btn_emp_del.Visible = option_ck.item("删除员工信息", item_kind);


            employer_detail.Visible = false;
            btn_ok.Visible = false;
            btn_cancel.Visible = false;

            tbx_id.Enabled = false;
            tbx_WorkerName.Enabled = false;
            tbx_IDCard.Enabled = false;
            tbx_GroupName.Enabled = false;
            tbx_Job.Enabled = false;
            tbx_Duties.Enabled = false;
            tbx_SalaryCoefficient.Enabled = false;
            tbx_DutyCoefficient.Enabled = false;
            ddl_is_paiqian.Enabled = false;

            ds = db_opt.build_dataset(sel_string);
            GridView1.DataSource = ds;
            GridView1.DataBind();

            login_user.Text = System.Web.HttpContext.Current.Session["RealName"].ToString();

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
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
                employer_detail.Visible = true;
                GridView1.Rows[GridView1.SelectedIndex].BackColor = System.Drawing.Color.BlanchedAlmond;
                string sel_rec = "";
                sel_rec = "select * from  [dzsw].[dbo].[Syl_WorkerInfo]  where  ID='" + GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text + "'";
                dr_select_row = db_opt.datareader(sel_rec);
                while (dr_select_row.Read())
                {
                    tbx_id.Text = dr_select_row["ID"].ToString();
                    tbx_WorkerName.Text = dr_select_row["WorkerName"].ToString();
                    tbx_IDCard.Text = dr_select_row["IDCard"].ToString();
                    tbx_GroupName.Text = dr_select_row["GroupName"].ToString();
                    tbx_Job.Text = dr_select_row["Job"].ToString();
                    tbx_Duties.Text = dr_select_row["Duties"].ToString();
                    tbx_SalaryCoefficient.Text = dr_select_row["SalaryCoefficient"].ToString();
                    tbx_DutyCoefficient.Text = dr_select_row["DutyCoefficient"].ToString();
                    ddl_is_paiqian.SelectedItem.Text = dr_select_row["Is_PaiQian"].ToString();
                }
            }
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
 
        protected void btn_ok_Click(object sender, EventArgs e)
        {
            btn_ok.Visible = false;
                     Boolean bool_isexist = db_opt.IsRecordExist("[dzsw].[dbo].[Syl_WorkerInfo]", "IDCard", tbx_IDCard.Text.Trim());
            if (option_sql != "") option_sql = "";
            try
            {
                Boolean bool_idcardFormat = Regex.IsMatch(tbx_IDCard.Text.Trim(), @"^\d{15}$") && tbx_IDCard.Text.Length == 15 || Regex.IsMatch(tbx_IDCard.Text.Trim(), @"^\d{17}[0-9Xx]$") && tbx_IDCard.Text.Length == 18;

                if (bool_idcardFormat == false)
                {
                    throw new Exception("身份证号: " + tbx_IDCard.Text.Trim());
                }
                if (Regex.IsMatch(tbx_SalaryCoefficient.Text, @"^\d{1,}$|^\d{1,}.\d{1,}$") == false)
                {
                    throw new Exception("工资系数: " + tbx_SalaryCoefficient.Text);
                }
                if (Regex.IsMatch(tbx_DutyCoefficient.Text, @"^\d{1,}$|^\d{1,}.\d{1,}$") == false)
                {
                    throw new Exception("管理奖系统:" + tbx_DutyCoefficient.Text);
                }



                if (string.Compare(option_method, "insert") == 0 && !bool_isexist)
                {
                  
                    option_sql = "insert into [dzsw].[dbo].[Syl_WorkerInfo] (WorkerName,IDCard,GroupName,Job,Duties,SalaryCoefficient,DutyCoefficient,Is_PaiQian)values('"
                    + tbx_WorkerName.Text + "','" + tbx_IDCard.Text.Trim() + "','"
                    + tbx_GroupName.Text + "','" + tbx_Job.Text + "','" + tbx_Duties.Text + "','"
                    + Convert.ToDecimal(tbx_DutyCoefficient.Text.Trim()) + "','"
                    + Convert.ToDecimal(tbx_DutyCoefficient.Text.Trim()) + "','"
                    +ddl_is_paiqian.SelectedItem.Text+"')";
                }
                else
                 if (string.Compare(option_method, "insert") == 0 && bool_isexist)
                    throw new Exception("用户已经存在: " + tbx_IDCard.Text.Trim());

                if (string.Compare(option_method, "delete") == 0 && bool_isexist)
                    option_sql = "delete  from [dzsw].[dbo].[Syl_WorkerInfo] where  ID='" + tbx_id.Text + "'";
                else
                     if (string.Compare(option_method, "delete") == 0 && !bool_isexist)
                    throw new Exception("用户不存在: " + tbx_IDCard.Text.Trim());

                if (string.Compare(option_method, "update") == 0 && bool_isexist)
                {

                    option_sql = "update  [dzsw].[dbo].[Syl_WorkerInfo] set WorkerName='" + tbx_WorkerName.Text.Trim()
                         + "',IDCard='" + tbx_IDCard.Text.Trim()
                         + "',GroupName='" + tbx_GroupName.Text.Trim() + "',Job='" + tbx_Job.Text.Trim() + "',Duties='" + tbx_Duties.Text.Trim()
                         + "',SalaryCoefficient='" + Convert.ToDecimal(tbx_SalaryCoefficient.Text.Trim())
                         + "',DutyCoefficient='" + Convert.ToDecimal(tbx_DutyCoefficient.Text.Trim())
                         + "',Is_PaiQian='" + ddl_is_paiqian.SelectedItem.Text
                        + "' where id='" + tbx_id.Text.Trim() + "'";
                }
                else
                     if (string.Compare(option_method, "update") == 0 && !bool_isexist)
                    throw new Exception("用户不存在: " + tbx_IDCard.Text.Trim());

                if (option_sql != "")
                {
                    db_opt.execsql(option_sql);
                    GridView1.DataSource = db_opt.build_dataset(sel_string);
                    GridView1.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('数据已经同步！');</script>");
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('无效操作！');</script>");


                employer_detail.Visible = false;
            }
            catch (Exception opt_err)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('录入字段类型或格式错误! " + opt_err.Message.ToString() + "');</script>");
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            employer_detail.Visible = false;
            btn_ok.Visible = false;

            employer_detail.Visible = false;
            option_method = "";
            btn_ok.Visible = false;
            btn_cancel.Visible = false;

            tbx_id.Enabled = false;
            tbx_WorkerName.Enabled = false;
            tbx_IDCard.Enabled = false;
            tbx_GroupName.Enabled = false;
            tbx_Job.Enabled = false;
            tbx_Duties.Enabled = false;
            tbx_SalaryCoefficient.Enabled = false;
            tbx_DutyCoefficient.Enabled = false;
            ddl_is_paiqian.Enabled = false;

            tbx_id.Text = "";
            tbx_WorkerName.Text = "";
            tbx_IDCard.Text = "";
            tbx_GroupName.Text = "";
            tbx_Job.Text = "";
            tbx_Duties.Text = "";
            tbx_SalaryCoefficient.Text = "";
            tbx_DutyCoefficient.Text = "";
            ddl_is_paiqian.SelectedIndex = -1;
            // Response.Write("<script>alert('操作取消数据未同步！');javascript:history.go(-1);</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('操作取消数据未同步！" + e.ToString() + "');</script>");

        }

        protected void btn_emp_add_Click(object sender, EventArgs e)
        {
            //添加员工信息
            employer_detail.Visible = true;
            option_method = "insert";
            btn_ok.Visible = true;
            btn_cancel.Visible = true;

            tbx_id.Enabled = false;
            tbx_WorkerName.Enabled = true;
            tbx_IDCard.Enabled = true;
            tbx_GroupName.Enabled = true;
            tbx_Job.Enabled = true;
            tbx_Duties.Enabled = true;
            tbx_SalaryCoefficient.Enabled = true;
            tbx_DutyCoefficient.Enabled = true;
            ddl_is_paiqian.Enabled = true;

            tbx_id.Text = db_opt.max_id("id", "[dzsw].[dbo].[Syl_WorkerInfo]").ToString(); 
            tbx_WorkerName.Text = "";
            tbx_IDCard.Text = "";
            tbx_GroupName.Text = "";
            tbx_Job.Text = "";
            tbx_Duties.Text = "";
            tbx_SalaryCoefficient.Text = "";
            tbx_DutyCoefficient.Text = "";
            ddl_is_paiqian.SelectedIndex = -1;
        }

        protected void btn_emp_del_Click(object sender, EventArgs e)
        {
            //删除员工信息
            employer_detail.Visible = true;
            option_method = "delete";
            btn_ok.Visible = true;
            btn_cancel.Visible = true;

            tbx_id.Enabled = false;
            tbx_WorkerName.Enabled = false;
            tbx_IDCard.Enabled = false;
            tbx_GroupName.Enabled = false;
            tbx_Job.Enabled = false;
            tbx_Duties.Enabled = false;
            tbx_SalaryCoefficient.Enabled = false;
            tbx_DutyCoefficient.Enabled = false;
            ddl_is_paiqian.Enabled = false; 

            tbx_id.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text;
            tbx_WorkerName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
            tbx_IDCard.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
            tbx_GroupName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
            tbx_Job.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text;
            tbx_Duties.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text;
            tbx_SalaryCoefficient.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[6].Text;
            tbx_DutyCoefficient.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[7].Text;
            ddl_is_paiqian.SelectedItem.Text= GridView1.Rows[GridView1.SelectedIndex].Cells[8].Text;
        }

        protected void btn_emp_edt_Click(object sender, EventArgs e)
        {
            //编辑员工信息
            employer_detail.Visible = true;
            option_method = "update";
            btn_ok.Visible = true;
            btn_cancel.Visible = true;
            tbx_id.Enabled = false;
            tbx_WorkerName.Enabled = true;
            tbx_IDCard.Enabled = true;
            tbx_GroupName.Enabled = true;
            tbx_Job.Enabled = true;
            tbx_Duties.Enabled = true;
            tbx_SalaryCoefficient.Enabled = true;
            tbx_DutyCoefficient.Enabled = true;
            ddl_is_paiqian.Enabled = true;


            if (GridView1.SelectedIndex==0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('还没选择要编辑的员工！');</script>");
            else
            {
                tbx_id.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text;
                tbx_WorkerName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
                tbx_IDCard.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
                tbx_GroupName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
                tbx_Job.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text;
                tbx_Duties.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text;
                tbx_SalaryCoefficient.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[6].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[6].Text;
                tbx_DutyCoefficient.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[7].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[7].Text;
                ddl_is_paiqian.SelectedItem.Text= GridView1.Rows[GridView1.SelectedIndex].Cells[8].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[8].Text;

            }

        }

        protected void btn_info_cx_Click(object sender, EventArgs e)
        {

        }
    }
}