using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
///BaseClass 的摘要说明
/// </summary>
public class BaseClass
{
    public BaseClass()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    #region 执行SQL语句
    public SqlDataReader datareader(string sql)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public Boolean ExecSQL(string sQueryString)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        con.Open();
        SqlCommand dbCommand = new SqlCommand(sQueryString, con);
        try
        {
            dbCommand.ExecuteNonQuery();
            con.Close();
        }
        catch (System.Exception)
        {
            con.Close();
            return false;
        }
        return true;
    }
    #endregion
    #region 返回数据源的数据集
    public System.Data.DataSet GetDataSet(string sQueryString, string TableName)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        con.Open();
        SqlDataAdapter dbAdapter = new SqlDataAdapter(sQueryString, con);
        DataSet dataset = new DataSet();
        dbAdapter.Fill(dataset, TableName);
        con.Close();
        return dataset;
    }
    #endregion
    #region 返回单个值（第一行第一列）
    public object SelectSQLReturnObject(string sQueryString, string TableName)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        SqlCommand cmd = new SqlCommand(sQueryString, con);
        con.Open();
        object obj = cmd.ExecuteScalar();
        con.Close();
        return obj;
    }
    #endregion
}


