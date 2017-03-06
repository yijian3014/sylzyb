using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// db 的摘要说明
/// </summary>
public class db
{
    public SqlConnection db_con;
       public string getconstr()
    {
        return System.Configuration.ConfigurationManager.AppSettings["jdbconn"].ToString();
    }
    public void open()
    {
        string constr = getconstr();
        db_con = new SqlConnection(constr);
        db_con.Open();

    }
    public void close()
    { db_con.Dispose();
        db_con.Close();
    }

    public void execsql(string sql)
    {
        open();
        SqlCommand sql_cmd = new SqlCommand(sql, db_con);
        sql_cmd.ExecuteNonQuery();
        close();
    }
    public DataSet build_dataset(string sql)
    {
        open();
        SqlDataAdapter rs = new SqlDataAdapter(sql, db_con);
        DataSet ds = new DataSet();
        rs.Fill(ds);
        return ds;
      
    }

    public SqlDataReader datareader(string sql)
    {
        open();
        SqlCommand cmd = new SqlCommand(sql, db_con);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

}