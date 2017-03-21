using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace sylzyb_employer_mgr
{
    public class khgl : IHttpModule
    {
      public   db db_opt = new db();
         SqlDataReader data_reader;

        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //此处放置清除代码。
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

 //---------------------------下面是公共方法
        public int build_newid()
        {
            //生成新的考核ID
          
            return  db_opt.max_id();

        }

        public String Get_idcard_str(string names)
        {
            string idcard = "";

            names=names.Replace(",","','");   //需要将NAMES字符串处理成"'skdks','safdf','sadfasdf')"
       
            data_reader = db_opt.datareader("select IDCard from [dzsw].[dbo].[Syl_WorkerInfo] where WorkerName in( '" + names + "'");
            while ( data_reader.Read())
            {
                idcard = idcard + data_reader["IDCard"].ToString()+",";
                
            }
            return  idcard.Substring(0,idcard.Length-1);//去掉末尾逗号
        }

        public string Get_name_str(string idcards)

        {
            string name = "";
            idcards=idcards.Replace(",", "','"); //需要将IDCARDS字符串处理成"'skdks','safdf','sadfasdf')"
            data_reader = db_opt.datareader("select WorkerName from [dzsw].[dbo].[Syl_WorkerInfo] where IDCard in '%" +idcards + "%'");
            while(data_reader.Read())
            { 
            name = name + data_reader["WorkerName"].ToString()+",";
            
            }
            return name.Substring(0,name.Length-1);


        }

       
        public DataSet  get_jingbanren(int AppID, string direction, string StateName)
        {
            //伴随经办人刷新动作返回对应经办人数据 集。
            //注意跳转时流程状态与人员的同步问题，应用方向发生偏转，在客户端选择对应流程节点时，返回对应结点的经办人。当操作确定后写入所选字符串
            //回退跳转，下一步跳转，间隔跳转，以及跳转过程中的人员选择问题，
            //回退跳转：人员来源于运行表，下一步跳转：人员来源于用户表，间隔跳转：同下一步跳转类似。
            //运行表经办人员记录新增问题：按APPID,经办人字符串添加。
            if (System.String.Compare(direction, "next") == 0)
            {
               return  db_opt.build_dataset("select [RealName] ,[IDCard]  from [dzsw].[dbo].[Syl_UserInfo] where UserLevelName='" + StateName + "'");
              
            }
            if (System.String.Compare(direction, "previous") == 0)
            {
                return db_opt.build_dataset("select ApproveName ,ApproveIDCard from[dzsw].[dbo].[Syl_SylAppRun] where [Flow_State]='" 
                    + StateName + "' and AppID="+AppID);
            }
            return null;
        }

      

        //----------------------------------------------------------------------//



        //----------------------------下面是对表[dzsw].[dbo].[Syl_UserInfo]的操作---------------------------//

        public String[] next_select_jinbanren(int AppID, string AppState)
        {
            //返回下一结点人员列表，包括IDCARD 用于填充界面人员选择CHECKBOXLIST.特点一上步全部经办人员[dzsw].[dbo].[Syl_UserInfo]
            string[] name = null;
            return name;
        }
        //----------------------------------------------------------------------//

        //----------------------------下面是对表[dzsw].[dbo].[Syl_WorkerInfo]的操作---------------------------//
        public DataSet select_workerInfo(int IDCard)
        {
            DataSet ds = new DataSet();
            return ds;
        }

        //----------------------------------------------------------------------//




        //----------------------------下面是对表[dzsw].[dbo].[Syl_AppraiseInfo]的操作---------------------------//
        public bool insert_AppraiseInfo(int AppID, string key, string value)
        {
            if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppraiseInfo] (" + key + ") values (" + value + ")"))
                return true;
            else
                return false;
        }
        public bool Update_AppraiseInfo(int AppID, string key, string value)
        {
            return true;
        }
        public bool Delete_AppraiseInfo(int AppID)
        {
            return true;
        }
        public DataSet select_AppraiseInfo(int AppID)
        {
            DataSet ds = new DataSet();
            return ds;
        }
        public DataSet select_AppraiseInfo(string fieldWhere)
        {
            DataSet ds = new DataSet();
            return ds;
        }
        public DataSet select_AppraiseInfo(string bgtime, string edtime)
        {
            DataSet ds = new DataSet();
            return ds;
        }


        //----------------------------------------------------------------------//



        //----------------------------下面是对表[dzsw].[dbo].[Syl_AppWorkerinfo]的操作---------------------------//
        public bool insert_AppWorkerInfo(int AppID, string key, string value)
        {
            string[] temp_key, temp_value;

            temp_key = key.Split(',');
            temp_value = value.Split(',');
            for (int i = 0; i <= temp_key.Length; i++)
                if ( db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppWorkerinfo] (" + temp_key[i] + ") values (" + temp_value[i] + ")") == false)
                return false;
            return true;
        }
        public bool  Update_AppWorkerInfo(int AppID, string IDCard, string[] Key, string[] Value, string kh_shenxiao_is_false)
        {
            return true;
        }
        public bool Update_AppWorkerInfo(int AppID, string[] key, string[] value)
        {
            return true;
        }
       

        //----------------------------------------------------------------------//


        //----------------------------下面是对表[dzsw].[dbo].[Syl_SylAppRun]的操作---------------------------//
        public bool delete_AppRun(int AppID)
        {
            return true;
        }

        public  bool Update_AppRun(int AppID, string[] key, string[] value)
        {
            //主要用于管理员对信息强制修改。

            return true;
        }
 public bool Update_AppRun(int AppID, string AppState, string IDCard, string[] Key, string[] Value)
        {
            //主要用于更新审核信息
            return true;
        }

        public String[] back_select_jinbanren(int AppID, string AppState)
        {
            //返加上一步经办人员列表，注意：不同的是只返回上一步处理过流程的经办人[dzsw].[dbo].[Syl_SylAppRun]
            string[] name = null;
            return name;
        }
        public bool insert_AppRun(int AppID, string key, string value)
        {
            //下面方法存在问题，只能同步一条记录，现在是多条记录
            string[] temp_key, temp_value;

            temp_key = key.Split(',');
            temp_value = value.Split(',');
           for(int i=0;i<=temp_key.Length;i++)
           
                if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_SylAppRun] (" + temp_key[i] + ") values (" + temp_value[i] + ")")==false)
                  return false; 
            return true;
        }

        //----------------------------------------------------------------------//



        /// <summary>
        /// 下面功能用于发考核，修改、删除考核的操作。
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>
        public bool insert_flow(int flow_id, string[] key, string[] value)
        {
            //插入新生成的考核，除此之外还包括插入被考核员工考核金额，下一步待办人人员列表写入考核运行表。

            if (insert_AppraiseInfo(flow_id, key[0], value[0]) && insert_AppRun(flow_id, key[1], value[1]) && insert_AppWorkerInfo(flow_id, key[3], value[3]))

                return true;
            else
            {
                delete_flow(flow_id);
                return false;
            }
        }
        public bool update_flow(int flow_id)
        {
           //修改考核信息
            return true;
        }
        public bool delete_flow(int flow_id)
        {
            return true;
        }
     
        /// <summary>
        /// 用于选择单条考核流程，可支持详单数据添充，
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>
        public DataRow select_flow(int flow_id)
        {
            //返回指定的考核信息
            DataRow dr = null; ;
            return dr;
        }

        /// <summary>
        /// 下面用于对被考核员工进行选择、新增、修改、删除，主要修改金额字段。
        /// </summary>
        /// <param name="flow_id"></param>
        /// <param name="bgtime"></param>
        /// <param name="edtime"></param>
        /// <returns></returns>
        public DataSet select_appworkerinfo(int flow_id,string bgtime,string edtime)
        {
            //返回被考核的人员
            DataSet ds = new DataSet();
            return ds;
        }
        public bool insert_appworkerinfo(int flow_id, string idcard)
        {
            //添加被考核人员信息
            return true;
        }
        public bool update_appworkerinfo(int flow_id, string idcard)
        {
            //更新被考核人员信息
            return true;

        }
        public bool delete_appworkerinfo(int flow_id, string idcard)
        {
            //删除指定的被考核人员信息
            return true;
        }

        /// <summary>
        /// 下面用于根据登陆用户角色，填充返回对应数据集，分辩用户角色的条件是[dzsw].[dbo].[Syl_UserInfo][dzsw].[dbo].[Syl_UserInfo]的USERLEVEL
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <returns></returns>
        public DataSet select_all(string param1, string param2, string param3)
        {
            //返回指定条件的考核流程数据集，主要用于总览
            DataSet ds = new DataSet(); ;
            return ds;
        }
        public DataSet select_daiban(string idcard, string flow_state)
        {
            //返回待办考核流程数据集，主要用于填充待办
            DataSet ds = new DataSet();
            return ds;

        }
        public DataSet select_yibanjie(string idcard, string flow_state)
        {
            //返回已办结考核流程数据集，主要用于填充已办结
            DataSet ds = new DataSet();
            return ds;
        }
        public bool update_shenpi_field(string idcard, string flow_id, string field1, string field2, string field3)
        {
            //更新流程审批字段内容，操作的是flow_run表
            return true;
        }
    }

}
