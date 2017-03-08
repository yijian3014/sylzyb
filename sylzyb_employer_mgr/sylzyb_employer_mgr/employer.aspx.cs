using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
        static string option_sql= "";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
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
                    Response.Write("<script language='javascript'>alert('您尚未登陆或登陆超时');location.href='Login.aspx';</script>");
                    Response.End();
                }
                else
                {
                    login_user.Text = System.Web.HttpContext.Current.Session["RealName"].ToString();
                    btn_emp_add.Visible = option_ck.item("添加员工信息");
                    btn_emp_edt.Visible = option_ck.item("修改员工信息");
                    btn_emp_del.Visible = option_ck.item("删除员工信息");
                    sel_string = "select * from [dzsw].[dbo].[Syl_WorkerInfo] where UserName='" + account + "' and UserPassWord='" + password + "'";
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["parent_page"].ToString());
        }

        protected void btn_usr_add_Click(object sender, EventArgs e)
        {
            btn_ok.Visible = true;
            /*

             < asp:BoundField DataField = "ID" HeaderText = "序号" />
   
                    < asp:BoundField DataField = "WorkerName" HeaderText = "姓名" />
      
                       < asp:BoundField DataField = "IDCard" HeaderText = "身份证" />
         
                          < asp:BoundField DataField = "GroupName" HeaderText = "班组" />
            
                             < asp:BoundField DataField = "Job" HeaderText = "岗位" />
               
                                 < asp:BoundField DataField = "Duties" HeaderText = "职务" />
                  

                                    < asp:BoundField DataField = "WagesFactor" HeaderText = "系数" />
                     
                                       < asp:BoundField DataField = "DutiesFactor" HeaderText = "管理奖系数" />
*/

            tbx_id.Text = "";
            tbx_WorkerName.Text = "";
            tbx_IDCard.Text = "";
            tbx_GroupName.Text = "";
            tbx_Job.Text = "";
            tbx_WagesFactor.Text = "";
            tbx_Duties.Text = "";




        }

        protected void btn_usr_del_Click(object sender, EventArgs e)
        {

        }

        protected void btn_usr_edt_Click(object sender, EventArgs e)
        {
            GDFK_BanLi.Visible = true;
            mtd = "Change";
            if (Convert.ToInt16(Session["userid"].ToString().Trim()) / 1000 == 6)
            {
                tbx_usr_acc.Enabled = false;
                tbx_usr_name.Enabled = false;
                ddl_usr_rule.Enabled = false;

            }
            else
            {
                tbx_usr_acc.Enabled = false;
                tbx_usr_name.Enabled = false;
                ddl_usr_rule.Enabled = false;

            }
            tbx_usr_pas.Enabled = true;
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
                sel_rec = "select * from [dzsw].[dbo].[SJ2B_KH_User] where  UserID=" + GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
                get_sing_rec(sel_rec);

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
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            employer_detail.Visible = false;
            btn_ok.Visible = false;
        }

        protected void btn_emp_add_Click(object sender, EventArgs e)
        {
            //添加员工信息
            employer_detail.Visible = true;


        }

        protected void btn_emp_del_Click(object sender, EventArgs e)
        {
            //删除员工信息
            employer_detail.Visible = true;

        }

        protected void btn_emp_edt_Click(object sender, EventArgs e)
        {
            //编辑员工信息
            employer_detail.Visible = true;
        }
    }
}