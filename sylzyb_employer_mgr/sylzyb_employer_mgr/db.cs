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

    /// <summary>
    /// 执行一个STRING类型的标准SQL语句，返回执行成工与否的BOOL值。一般执行对表的增删改操作
    /// </summary>
    /// <param name="sql">标准SQL语句，需在数据库对对应的表做过测试，并保证期结果状态稳定</param>
    /// <returns>BOOL值，通过executenonquery返回值判断执行成功与否</returns>
    public bool execsql(string sql)
    {
        
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
    /// <summary>
    /// 返回指定表的SELECT结果集
    /// </summary>
    /// <param name="sql">标准SQL的SELECT查询语句</param>
    /// <returns>返回SELECT的结果集</returns>
    public DataSet build_dataset(string sql)
    {
       
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

    /// <summary>
    /// 判断符合条件的记录是否存在返回BOOL值
    /// </summary>
    /// <param name="tablename">数据库内的完整表名</param>
    /// <param name="key">查询的字段，不能是包含多个字段的字符串</param>
    /// <param name="value">查询的值，不能是包含多个值的字符串</param>
    /// <returns>返回是否存在的判定</returns>
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
    /// <summary>
    /// 判断符合条件的记录是否存在返回BOOL值
    /// </summary>
    /// <param name="tablename">数据库内的完整表名</param>
    /// <param name="where">查询条件</param>
    /// <returns>返回是否存在的判定</returns>
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
    /// <summary>
    /// 通过指定表、指定字段、指定条件、返回结果的第一条记录的值
    /// </summary>
    /// <param name="field"></param>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <returns></returns>
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
   /// <summary>
   /// 返回指定字段的最大值（MAX()函数处理结果）
   /// </summary>
   /// <param name="field"></param>
   /// <param name="table"></param>
   /// <returns></returns>
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