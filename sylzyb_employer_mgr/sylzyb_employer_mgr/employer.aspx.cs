using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
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
        public Check option_ck=new Check();
        public static string sel_string = "";
        public  static string option_sql= "";
        public db db_opt = new db();
        public SqlDataReader dr_select_row ;
        public DataSet ds = new DataSet();
        static  string option_method = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (option_ck.moudle("员工信息管理") == false || System.Web.HttpContext.Current.Session["UserName"].ToString() == "" || System.Web.HttpContext.Current.Session["IDCard"] == null)
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
            btn_emp_add.Visible = option_ck.item("员工-"+"添加员工信息");
            btn_emp_edt.Visible = option_ck.item("员工-"+"修改员工信息");
            btn_emp_del.Visible = option_ck.item("员工-"+"删除员工信息");


            employer_detail.Visible = false;
            btn_ok.Visible = false;
           btn_cancel.Visible = false;

            tbx_id.Enabled = false;
            tbx_WorkerName.Enabled = false;
            tbx_IDCard.Enabled = false;
            tbx_GroupName.Enabled = false;
            tbx_Job.Enabled = false;
            tbx_Duties.Enabled = false;
            tbx_WagesFactor.Enabled = false;
            tbx_DutiesFactor.Enabled = false;

            ds= db_opt.build_dataset(sel_string);
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
                sel_rec = "select * from  [dzsw].[dbo].[Syl_WorkerInfo]  where  ID='" + GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text+"'";
               dr_select_row=db_opt.datareader(sel_rec);
                while (dr_select_row.Read())
                {                
                    tbx_id.Text = dr_select_row["ID"].ToString();
                    tbx_WorkerName.Text = dr_select_row["WorkerName"].ToString();
                    tbx_IDCard.Text = dr_select_row["IDCard"].ToString();
                    tbx_GroupName.Text = dr_select_row["GroupName"].ToString();
                    tbx_Job.Text = dr_select_row["Job"].ToString();
                    tbx_Duties.Text = dr_select_row["Duties"].ToString();
                    tbx_WagesFactor.Text = dr_select_row["WagesFactor"].ToString();
                    tbx_DutiesFactor.Text = dr_select_row["DutiesFactor"].ToString();
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
            employer_detail.Visible = false;
            try
            {


                if (option_sql != "") option_sql = "";
                if (string.Compare(option_method, "insert") == 0)
                    option_sql = "insert into [dzsw].[dbo].[Syl_WorkerInfo] (WorkerName,IDCard,GroupName,Job,Duties,WagesFactor,DutiesFactor)values('"
                    + tbx_WorkerName.Text + "','" + tbx_IDCard.Text + "','" + tbx_GroupName.Text + "','" + tbx_Job.Text + "','" + tbx_Duties.Text + "','"
                    + Convert.ToDecimal(tbx_WagesFactor.Text) + "','" + Convert.ToDecimal(tbx_DutiesFactor.Text) + "')";
                if (string.Compare(option_method, "delete") == 0)
                    option_sql = "delete  from [dzsw].[dbo].[Syl_WorkerInfo] where  ID='" + tbx_id.Text + "'";
                if (string.Compare(option_method, "update") == 0)
                    option_sql = "update  [dzsw].[dbo].[Syl_WorkerInfo] set WorkerName='"
                         + tbx_WorkerName.Text.Trim() + "',IDCard='" + tbx_IDCard.Text.Trim() + "',GroupName='" + tbx_GroupName.Text.Trim()
                         + "',Job='" + tbx_Job.Text.Trim() + "',Duties='" + tbx_Duties.Text.Trim() + "',WagesFactor='" + Convert.ToDecimal(tbx_WagesFactor.Text.Trim())
                         + "',DutiesFactor='" + Convert.ToDecimal(tbx_DutiesFactor.Text.Trim()) + "' where id='" + tbx_id.Text.Trim() + "'";
                if (option_sql != "")
                {
                    db_opt.execsql(option_sql);
                    GridView1.DataSource = db_opt.build_dataset(sel_string);
                    GridView1.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('数据已经同步！');</script>");
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('无效操作！');</script>");
            }
            catch (Exception  opt_err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('录入字段类型错误!"+opt_err.Message.ToString()+"');</script>");
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
            tbx_WagesFactor.Enabled = false;
            tbx_DutiesFactor.Enabled = false;

           tbx_id.Text = "";
            tbx_WorkerName.Text = "";
            tbx_IDCard.Text = "";
            tbx_GroupName.Text = "";
            tbx_Job.Text = "";
            tbx_Duties.Text = "";
            tbx_WagesFactor.Text = "";
            tbx_DutiesFactor.Text = "";
            // Response.Write("<script>alert('操作取消数据未同步！');javascript:history.go(-1);</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "message", "<script>alert('操作取消数据未同步！"+e.ToString()+"');</script>");
       
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
            tbx_WagesFactor.Enabled = true;
            tbx_DutiesFactor.Enabled = true;

             tbx_id.Text = "";
            tbx_WorkerName.Text = "";
            tbx_IDCard.Text = "";
            tbx_GroupName.Text = "";
            tbx_Job.Text = "";
            tbx_Duties.Text = "";
            tbx_WagesFactor.Text = "";
            tbx_DutiesFactor.Text = "";

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
            tbx_WagesFactor.Enabled = false;
            tbx_DutiesFactor.Enabled = false;

            tbx_id.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text;
            tbx_WorkerName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
            tbx_IDCard.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
            tbx_GroupName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
            tbx_Job.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text;
            tbx_Duties.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text;
            tbx_WagesFactor.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[6].Text;
            tbx_DutiesFactor.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[7].Text;
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
            tbx_WagesFactor.Enabled = true;
            tbx_DutiesFactor.Enabled = true;

            tbx_id.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text== "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text;
            tbx_WorkerName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
            tbx_IDCard.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text;
            tbx_GroupName.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
            tbx_Job.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[4].Text;
            tbx_Duties.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[5].Text;
            tbx_WagesFactor.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[6].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[6].Text;
            tbx_DutiesFactor.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[7].Text == "&nbsp;" ? "" : GridView1.Rows[GridView1.SelectedIndex].Cells[7].Text;
        }

       
    }
}