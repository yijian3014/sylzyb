using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;



namespace sylzyb_employer_mgr
{
    public partial class clean_item_select : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddl_select.SelectedIndex = 0;
            ///获取待选择项APPID
            //string[] appid;
            //string[] appcontext;
            //appid= Request["app_id"].ToString().Split('|');
            //appcontext = Request["appcontext"].ToString().Split('|');
            //for (int i = 0; i < appid.Length; i++)
            //{
            //    cbl_kh_items.Items.Add("");
            //    cbl_kh_items.Items[i].Value  = appid[i].ToString();
            //    cbl_kh_items.Items[i].Text= appcontext[i].ToString();
            //}
            gv_ql_items.DataSource = Request["ds_ql_items"];
            gv_ql_items.DataBind();
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('"+Request["app_id"].ToString()+"');</script>");

        }




    }
}