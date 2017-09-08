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
        public Check ck = new Check();
        string pageName = "";
        private int module_kind = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            System.Web.HttpContext.Current.Session["ISSuperUser"] = "false";
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
            try
            {

                if (ck.user(tbx_lg_nm.Text.Trim(), tbx_lg_pas.Text.Trim()) != true)
                    throw new Exception("你的登陆信息输入错误!");
                if (ck.Module(rbtl_mod_sel.SelectedItem.Text, module_kind) == false)
                {
                    throw new Exception("你没有权限使用该功能！");
                }
                else
                {
                    // int i= System.String.Compare(System.Web.HttpContext.Current.Session["ISSuperUser"].ToString(), "true") ;
                    // 0:相等，其它不等。
                    if (System.String.Compare(System.Web.HttpContext.Current.Session["ISSuperUser"].ToString(), "true") == 0)
                        System.Web.HttpContext.Current.Session["RealName"] = System.Web.HttpContext.Current.Session["RealName"].ToString() + "(超级用户)";
                    pageName = rbtl_mod_sel.SelectedItem.Value.ToString().Trim();
                    Response.Redirect(pageName);
                }

            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script>alert('" + err.Message + "')</script>");
            }
        }

    }
}