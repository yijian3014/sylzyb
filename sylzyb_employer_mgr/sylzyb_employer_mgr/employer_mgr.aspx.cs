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
        public static string sel_string = "";
        static string mtd = "";
        db ds = new db();
        public DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                login_user.Text = Session["lg_name"].ToString();
            }
            if (Convert.ToInt64(Session["rule_code"].ToString().Trim()) / 1000 == 6)
            {
                btn_usr_add.Visible = true;
                btn_usr_del.Visible = true;
                sel_string = "select * from [dzsw].[dbo].[Syl_WorkerInfo] where userid<2000 or  userid= " + Convert.ToInt16(Session["userid"].ToString().Trim()) + "order by UserID";
            }
            else
            {
                btn_usr_add.Visible = false;
                btn_usr_del.Visible = false;
                sel_string = "select * from [dzsw].[dbo].[Syl_WorkerInfo] where userid= " + Convert.ToInt16(Session["userid"].ToString().Trim()) + " order by UserID";
            }

            }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["parent_page"].ToString());
        }

        protected void btn_usr_add_Click(object sender, EventArgs e)
        {

        }

        protected void btn_usr_del_Click(object sender, EventArgs e)
        {

        }

        protected void btn_usr_edt_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}