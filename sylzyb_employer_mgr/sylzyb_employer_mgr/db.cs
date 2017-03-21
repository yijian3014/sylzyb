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
        return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
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

    public bool execsql(string sql)
    {
        //执行一个增删改操作
        int i = 0;
        open();
        SqlCommand sql_cmd = new SqlCommand(sql, db_con);
        sql_cmd.ExecuteNonQuery();
        close();
        if (i > 0)
            return true;
        else
            return false;
    }
    public DataSet build_dataset(string sql)
    {
        //返回一个数据集
        open();
        SqlDataAdapter rs = new SqlDataAdapter(sql, db_con);
        DataSet ds = new DataSet();
        rs.Fill(ds);
        return ds;
      
    }

    public SqlDataReader datareader(string sql)
    {
        //返回一个数据集指针
        open();
        SqlCommand cmd = new SqlCommand(sql, db_con);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public bool IsRecordExist(string tablename,string key,string value)
    {
        //拼接KEY,VALUE到SQL SELECT语名句中，通过返回值判定表内是否存在记录
        open();
        SqlCommand cmd = new SqlCommand("select " + key + " from " + tablename + " where " + key + "=," + value + "'", db_con);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteScalar();

        if (System.String.Compare(cmd.ExecuteScalar().ToString(), value)==0)
            return true;
        else
            return false;
    }
    public int max_id()
    {
        //返回最大ID最大值
        open();
        SqlCommand cmd = new SqlCommand("select max(appid) from [dzsw].[dbo].[Syl_AppraiseInfo]", db_con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandType = CommandType.Text;
       
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
    
}