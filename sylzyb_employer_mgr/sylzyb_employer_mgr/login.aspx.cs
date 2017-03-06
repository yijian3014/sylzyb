using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace sylzyb_employer_mgr
{
    public partial class login : System.Web.UI.Page
    {
        public db db_opt;
        public DataSet user_ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public bool chek_account(string acc_name, string acc_pass)
        {
 string usr_sql = "select * from [dzsw].[dbo].[Syl_UserInfo] where UserName=" + tbx_lg_nm.Text.Trim() + " and UserPassWord=" + tbx_lg_pas.Text.Trim();
            user_ds = db_opt.build_dataset(usr_sql);
            if (user_ds.Tables[0].Rows.Count > 0)
            {
                Session["lg_name"] = user_ds.Tables[0].Rows[0][2].ToString();
                Session["rule_code"] = user_ds.Tables[0].Rows[0][5].ToString();
                return true;

            }
            return false;

        }
        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (Convert.ToUInt64(Session["rule_code"]) / 1 != 0)
                Response.Write("<java script>alert('你没有权限使用该功能！')</java script>");
            else
                Response.Redirect("employer_mgr.aspx");

        }
    }
}