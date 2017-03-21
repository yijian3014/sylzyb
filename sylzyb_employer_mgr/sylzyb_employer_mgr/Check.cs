using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.SessionState;

namespace sylzyb_employer_mgr
{

    public class Check : IHttpModule
    {

        public db db_opt=new db();
        public DataSet ds;
        public string errmsg;
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //此处放置清除代码。
            if (db_opt != null )
            db_opt.close();
            if (ds != null)
                ds.Clear();
        }

        public void Init(HttpApplication context)
        {
            // 下面是如何处理 LogRequest 事件并为其 
            // 提供自定义日志记录实现的示例
            context.LogRequest += new EventHandler(OnLogRequest);

        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //可以在此处放置自定义日志记录逻辑
        }
        public bool user(string account, string password)
        {
            //通过用户名密码验证用户权限，并初化必要的SESSIONID及界面初始化序列字符串
         
            string usr_sql = "select * from [dzsw].[dbo].[Syl_UserInfo] where UserName='" + account + "' and UserPassWord='" + password+"'";
            ds = db_opt.build_dataset(usr_sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                System.Web.HttpContext.Current.Session["SuperUser"] = "false";
                System.Web.HttpContext.Current.Session["RealName"] = ds.Tables[0].Rows[0][1].ToString();
                System.Web.HttpContext.Current.Session["IDCard"] = ds.Tables[0].Rows[0][2].ToString();
                System.Web.HttpContext.Current.Session["UserName"] = ds.Tables[0].Rows[0][3].ToString();
                System.Web.HttpContext.Current.Session["UserLevel"] = ds.Tables[0].Rows[0][5].ToString();
                System.Web.HttpContext.Current.Session["UserLevelName"] = ds.Tables[0].Rows[0][6].ToString();
                System.Web.HttpContext.Current.Session["UserPower"] = ds.Tables[0].Rows[0][7].ToString();
                System.Web.HttpContext.Current.Session["ModulePower"] = ds.Tables[0].Rows[0][8].ToString();

                Dispose();
                return true;
            }
            else
            {
                Dispose();
                return false;
            }
        }
        public bool moudle(string moudlename)
        {
            //验证用户是否具备选择目标模块的使用权
            int ss;
            string bb = "";
            try
            {
               
                string mod_chk_sql = "select * from [dzsw].[dbo].[Syl_UserPower] where PowerName='" + moudlename + "'";
                ds = db_opt.build_dataset(mod_chk_sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ss = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()) ;
                    bb = System.Web.HttpContext.Current.Session["ModulePower"].ToString();
                    if (System.Web.HttpContext.Current.Session["ModulePower"].ToString().Substring(Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()) - 1, 1) == "Y")
                    {

                        System.Web.HttpContext.Current.Session["ISSuperUser"] = "true";

                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["SuperUser"] = "false";
                    }
                    if (System.Web.HttpContext.Current.Session["ModulePower"].ToString().Substring(Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()), 1) == "Y")                      
                 {

                            return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
               
                errmsg = e.ToString();
                Dispose();
                return false;
            }
        }
        public bool item(string itemname)
        {
            //通过控件名，及界面初始化序列字符串，返回界面的操作属性TRUE OR FALSE
            try
            {
               
                string mod_chk_sql = "select * from [dzsw].[dbo].[Syl_UserPower] where PowerName='" + itemname + "'";
                ds = db_opt.build_dataset(mod_chk_sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (System.Web.HttpContext.Current.Session["UserPower"].ToString().Substring(Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()), 1) == "Y")
                    {
                        
                        Dispose();
                        return true;
                    }
                    else
                    {
                        Dispose();
                        return false;
                    }
                }
                else
                { Dispose();
                    return false;
            } }
            catch (Exception e)
            {
                errmsg = e.ToString();
                Dispose();
                return false;
            }

        }
    }
}
