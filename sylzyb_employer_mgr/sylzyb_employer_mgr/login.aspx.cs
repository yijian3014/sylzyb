using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sylzyb_employer_mgr
{
    public partial class login : System.Web.UI.Page
    {
        public db db_opt;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string usr_sql = "select * from YuanL_KH_User where UserName=" + tbx_lg_nm.Text.Trim() + " and UserPassWord=" + tbx_lg_pas.Text.Trim();
            

        }
    }
}