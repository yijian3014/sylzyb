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
        public Check ck=new Check();
        string pageName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            System.Web.HttpContext.Current.Session["RealName"] = "";
            System.Web.HttpContext.Current.Session["IDCard"] = "";
            System.Web.HttpContext.Current.Session["UserName"] = "";
            System.Web.HttpContext.Current.Session["UserLevel"] = "";
            System.Web.HttpContext.Current.Session["UserLevelName"] = "";
            System.Web.HttpContext.Current.Session["UserPower"] = "";
            System.Web.HttpContext.Current.Session["ModulePower"] = "";
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (ck.user(tbx_lg_nm.Text.Trim(), tbx_lg_pas.Text.Trim()))
            {
                if (ck.moudle(rbtl_mod_sel.SelectedItem.Text)==false)
                    Response.Write("<java script>alert('你没有权限使用该功能！')</java script>");

                else
                    pageName = rbtl_mod_sel.SelectedItem.Value.ToString().Trim();
                    Response.Redirect(pageName);
            }
            else
                Response.Write("<script>alert('你没有权限使用该功能！')</script>");
        }

    }
}