using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
namespace sylzyb_employer_mgr
{
    public partial class data_fx_cht : System.Web.UI.Page
    {
        public db db_opt = new db();
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public DataView dv = new DataView();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 初始化数据集，可以同时初始化并返回多个数据表,当为一个表时，可用于集合内数据分析，当表为多个时，可用于集合间数据比较分析
        /// </summary>
        /// <param name="table_names">用逗号分割的表名</param>
        /// <param name="fields">用逗号分割的表内字段名</param>
        /// <param name="wheres">用逗号分割的条件</param>
        /// <param name="is_comp_rate"></param>
        /// <returns></returns>
        public bool init_ds(string table_names, string fields, string wheres, bool is_comp_rate)
        {
            try
            {

                string[] table_name;
                string[] field;
                string[] where;
                table_name = table_names.Split(',');
                field = fields.Split(',');
                where = wheres.Split(',');
                if (ds != null)
                    ds.Clear();
                for (int i = 0; i < table_name.Length; i++)
                {
                    ds = db_opt.build_dataset("select " + field[i] + " from " + table_name[i] + " where " + where[i]);

                }
                return true;
            }
            catch (Exception err)
            {


                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + err.Message + "');location.href='Login.aspx';</script>");
                return false;
            }
        }
        public bool init_cht(string kind, DataTable dt, string time_field, string fields)
        {
            try
            {
                

                return true;
            }
            catch (Exception err)
            {


                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + err.Message + "');location.href='Login.aspx';</script>");
                return false;
            }
        }
        public bool init_cht(string kind,DataSet ds, string time_fields,string values_fields,string rate_fields)
        {
            try
            {
                switch (kind)
                {
                    case "历史":
                        break;
                    case "占比":
                        break;
                    case "环比":
                        break;
                    case "同比":
                        break;
                    case "类比":
                        break;
                }

                return true;
            }
            catch (Exception err)
            {
                
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('"+err.Message+"');location.href='Login.aspx';</script>");
                return false;
            }
        } 
        protected void btn_fx_ok_Click(object sender, EventArgs e)
        {

        }
    }
}