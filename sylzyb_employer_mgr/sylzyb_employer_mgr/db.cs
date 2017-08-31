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
        i=sql_cmd.ExecuteNonQuery();
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
        close();
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
  
        SqlCommand cmd = new SqlCommand("select " + key + " from " + tablename + " where " + key + "='" + value + "'" , db_con);
        cmd.CommandType = CommandType.Text;
        // cmd.ExecuteScalar();
      string  ss =Convert.ToString(cmd.ExecuteScalar());
        close();
        if (System.String.Compare(ss, value)==0)
            return true;
        else
            return false;
       
    }
    public bool IsRecordExist(string tablename,string where)
    {
        //拼接KEY,VALUE到SQL SELECT语名句中，通过返回值判定表内是否存在记录
        open();

        SqlCommand cmd = new SqlCommand("select * from " + tablename + " where " +where , db_con);
        cmd.CommandType = CommandType.Text;
        // cmd.ExecuteScalar();
        string ss = Convert.ToString(cmd.ExecuteScalar());
        close();
        if (ss!="")
            return true;
        else
            return false;

    }
    public string get_values(string field, string table, string where)
    {
        string tmp_str = "";
        open();
        SqlDataAdapter rs = new SqlDataAdapter("select top 1 " + field + " from " + table + " where " + where, db_con);
        DataSet ds = new DataSet();
        rs.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                tmp_str += ds.Tables[0].Rows[0][i].ToString().Trim();
            }
            close();
            return tmp_str;
        }
        else return "";
    }
   
    public int max_id(string field,string table)
    {
        //返回最大ID最大值
        string str_maxid = "";
       int id=0;
        open();
        SqlCommand cmd = new SqlCommand("select max("+field+") from "+table, db_con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandType = CommandType.Text;
        str_maxid=cmd.ExecuteScalar().ToString();
        close();
        if (str_maxid =="")
            id= 1;
        else id= Convert.ToInt32(str_maxid) + 1;
        return id;
    }
    public string  get_newid()
    {
        string dt = DateTime.Now.ToString("yyyyMMddHH");

        return dt;
    }
    public string get_userlevelname(int userlevel)
    {
        string str_name = "";
       
        open();
        SqlCommand cmd = new SqlCommand("select top 1 UserLevelName from [dzsw].[dbo].[Syl_UserInfo] where userlevel="+ userlevel, db_con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandType = CommandType.Text;
        str_name = cmd.ExecuteScalar().ToString();
        close();
        if (str_name !="")
            return str_name;
        else
        return "";
    }
}