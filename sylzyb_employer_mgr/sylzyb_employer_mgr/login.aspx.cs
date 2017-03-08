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
        public Check ck;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (ck.moudle(rbtl_mod_sel.SelectedValue.ToString()) && ck.user(tbx_lg_nm.Text.Trim(), tbx_lg_pas.Text.Trim()))
                Response.Write("<java script>alert('你没有权限使用该功能！')</java script>");
            else
                Response.Redirect("employer_mgr.aspx");
        }

    }
}