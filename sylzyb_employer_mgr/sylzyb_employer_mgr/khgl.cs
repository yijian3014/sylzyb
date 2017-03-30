using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace sylzyb_employer_mgr
{
    public class khgl : IHttpModule
    {
        public db db_opt = new db();
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
        /// <summary>
        /// 生成新的考核ID，同时初始化对应的表
        /// </summary>
        /// <param name="username"></param>
        /// <param name="user_idcard"></param>
        /// <returns></returns>
        public int build_newid(string username, string user_idcard)
        {

            int i = db_opt.max_id("[AppID]", "[dzsw].[dbo].[Syl_AppraiseInfo]");

            string old_id = db_opt.get_values("AppID", "[dzsw].[dbo].[Syl_SylAppRun]", "[Flow_State]='点检' and ([Oponion_State]='' OR [Oponion_State] is null) and [ApproveIDCard]='" + user_idcard + "'");

            if (old_id != "")
            {
                i = Convert.ToInt32(old_id);
                db_opt.execsql("delete  from[dzsw].[dbo].[Syl_AppWorkerinfo] where appid =" + i);
                return i;
            }
            else
            {
                if (insert_AppraiseInfo(i, "[ApplicantName],[ApplicantIDCard]", username + "," + user_idcard) &&
             insert_AppRun(i, "[Flow_State],[ApproveName],[ApproveIDCard]", "点检," + username + "," + user_idcard))

                    return i;
            }
            return -1;
        }
        public String Get_idcard_str(string names)
        {
            string idcard = "";

            names = names.Replace(",", "','");   //需要将NAMES字符串处理成"'skdks','safdf','sadfasdf')"

            data_reader = db_opt.datareader("select IDCard from [dzsw].[dbo].[Syl_WorkerInfo] where WorkerName in( '" + names + "'");
            while (data_reader.Read())
            {
                idcard = idcard + data_reader["IDCard"].ToString() + ",";

            }
            return idcard.Substring(0, idcard.Length - 1);//去掉末尾逗号
        }

        public string Get_name_str(string idcards)

        {
            string name = "";
            idcards = idcards.Replace(",", "','"); //需要将IDCARDS字符串处理成"'skdks','safdf','sadfasdf')"
            data_reader = db_opt.datareader("select WorkerName from [dzsw].[dbo].[Syl_WorkerInfo] where IDCard in '%" + idcards + "%'");
            while (data_reader.Read())
            {
                name = name + data_reader["WorkerName"].ToString() + ",";

            }
            return name.Substring(0, name.Length - 1);

        }

        /// <summary>
        /// 取得下一步，经办人信息，无论这个流程方向是下一步转交还是回退
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="direction"></param>
        /// <param name="StateName"></param>
        /// <returns></returns>
        public DataSet get_jingbanren(int AppID, string direction, string StateName)
        {
            //伴随经办人刷新动作返回对应经办人数据 集。
            //注意跳转时流程状态与人员的同步问题，应用方向发生偏转，在客户端选择对应流程节点时，返回对应结点的经办人。当操作确定后写入所选字符串
            //回退跳转，下一步跳转，间隔跳转，以及跳转过程中的人员选择问题，
            //回退跳转：人员来源于运行表，下一步跳转：人员来源于用户表，间隔跳转：同下一步跳转类似。
            //运行表经办人员记录新增问题：按APPID,经办人字符串添加。
            if (System.String.Compare(direction, "转交") == 0)
            {
                return db_opt.build_dataset("select [RealName],[IDCard]   from [dzsw].[dbo].[Syl_UserInfo] where UserLevelName='" + StateName + "'");

            }
            if (System.String.Compare(direction, "回退") == 0)
            {
                return db_opt.build_dataset("select ApproveName,[ApproveIDCard] from[dzsw].[dbo].[Syl_SylAppRun] where [Flow_State]='"
                    + StateName + "' and AppID=" + AppID);
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
        public DataSet select_WorkerInfo(string where)
        {
            DataSet ds = new DataSet();
            ds = db_opt.build_dataset("select *  from [dzsw].[dbo].[Syl_WorkerInfo] where GroupName='" + where + "'");
            return ds;
        }



        //----------------------------------------------------------------------//




        //----------------------------下面是对表[dzsw].[dbo].[Syl_AppraiseInfo]的操作---------------------------//
        public bool insert_AppraiseInfo(int AppID, string key, string value)
        {
            string[] temp_key, temp_value;
            string new_value = "";
            temp_key = key.Split(',');
            temp_value = value.Split(',');
            if (temp_value.Length > 1)
                for (int j = 0; j < temp_value.Length - 1; j++)
                {
                    new_value += temp_value[j].Trim() + "','";
                }
            new_value = new_value + temp_value[temp_value.Length - 1].Trim() + "'";//用于封口单引号
            new_value = Convert.ToString(AppID) + ",'" + new_value;

            key = "AppID," + key;

            if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppraiseInfo](" + key + ") values (" + new_value + ")"))
                return true;
            else
                return false;
        }
        public bool Update_AppraiseInfo(int AppID, string key, string value)
        {
            string[] temp_key, temp_value;
            string new_value = "";
            temp_key = key.Split(',');
            temp_value = value.Split(',');
            if (temp_key.Length > 1)
                for (int j = 0; j < temp_value.Length - 1; j++)
                {

                    new_value += temp_value[j].Trim() + "','";
                }
            new_value = new_value + temp_value[temp_value.Length - 1].Trim();
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppraiseInfo]", "AppID", Convert.ToString(AppID)))
            {
                for (int i = 0; i < temp_key.Length; i++)
                {
                    if (temp_key[i].Trim() == "[TC_DateTime]")
                        db_opt.execsql("  update [dzsw].[dbo].[Syl_AppraiseInfo] set   " + temp_key[i].Trim() + "=" + temp_value[i].Trim() + " where AppID=" + AppID);
                    else
                        db_opt.execsql("  update [dzsw].[dbo].[Syl_AppraiseInfo] set   " + temp_key[i].Trim() + "='" + temp_value[i].Trim() + "' where AppID=" + AppID);
                }
                return true;
            }
            else
            {

                return false;
            }


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
        /// <summary>
        /// 向[dzsw].[dbo].[Syl_AppWorkerinfo]插入多条被考核员工信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool insert_AppWorkerInfo(int AppID, string key, string value)
        {
            //string[] temp_key, temp_value;

            //temp_key = key.Split(',');
            //temp_value = value.Split(',');
            //for (int i = 0; i <= temp_key.Length; i++)

            //    if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", temp_key[i].Trim(), temp_value[i].Trim()))
            //        continue;
            //    else
            //    if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppWorkerinfo] ( AppID," + temp_key[i].Trim() + ") values (" + AppID + ",'" + temp_value[i].Trim() + "')") == false)
            //        return false;
            return true;
        }

        /// <summary>
        ///  向[dzsw].[dbo].[Syl_AppWorkerinfo]插入单条被考核员工信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool insert_single_AppWorkerInfo(int AppID, string key, string value)
        {

            string[] temp_key, temp_value;
            string new_value = "";
            temp_key = key.Split(',');
            temp_value = value.Split(',');
            if (temp_key.Length > 1)
                for (int j = 0; j < temp_value.Length-1 ; j++)
                {
                    new_value += temp_value[j].Trim() + "','";
                }
            new_value = new_value + temp_value[temp_value.Length - 1].Trim();
            for (int i = 0; i < temp_key.Length; i++)
            {
                if (temp_key[i] == "[AppIDCard]")
                    //说明：需要判定[dzsw].[dbo].[Syl_AppWorkerinfo]表内是否已经存在该考核ID不存在则随意插入，存在需判定要插入的被考核人是否已经在表内，存在不插入，不存在则插入。
                    if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "AppID", Convert.ToString(AppID)))
                    {
                        if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "AppID=" + AppID + " and " + temp_key[i].Trim() + "='" + temp_value[i].Trim() + "'") == false)
                            if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppWorkerinfo] ( AppID," + key.Trim() + ") values (" + AppID + ",'" + new_value.Trim() + "')"))
                                return true;
                    }
                    else
                    {
                        if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppWorkerinfo] ( AppID," + key.Trim() + ") values (" + AppID + ",'" + new_value.Trim() + "')"))
                            return true;
                    }
            }
            return false;

        }
        /// <summary>
        /// 更新被考核员工信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="IDCard"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool Update_AppWorkerInfo(int AppID, string IDCard, string Key, string Value)
        {
            string[] temp_key, temp_value;

            temp_key = Key.Split(',');
            temp_value = Value.Split(',');
            for (int i = 0; i <= temp_key.Length; i++)

                if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "[AppIDCard]='" + IDCard + "'and [AppID]=" + AppID))
                    if (temp_key[i].Trim() == "[AppAmount]")
                    {
                        if (db_opt.execsql("update [dzsw].[dbo].[Syl_AppWorkerinfo] set   " + temp_key[i].Trim() + "=" + temp_value[i].Trim()
                       + " where AppID=" + AppID + " and [AppIDCard]='" + IDCard + "'"))

                            return true;
                    }
                    else
                    {
                        if (db_opt.execsql("update [dzsw].[dbo].[Syl_AppWorkerinfo] set   " + temp_key[i].Trim() + "='" + temp_value[i].Trim()
                            + "' where AppID=" + AppID + " and [AppIDCard]='" + IDCard + "'"))

                            return true;
                    }
            return false;
        }
        /// <summary>
        /// 删除或取消指的员工考核信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public bool delete_AppWorkerInfo(int AppID, string idcard)
        {
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "[AppIDCard]", idcard))

                if (db_opt.execsql("delete from [dzsw].[dbo].[Syl_AppWorkerinfo] where AppID=" + AppID
                     + " and [AppIDCard]='" + idcard + "'"))
                    return true;
            return false;


        }


        //----------------------------------------------------------------------//


        //----------------------------下面是对表[dzsw].[dbo].[Syl_SylAppRun]的操作---------------------------//

        public bool delete_AppRun(int AppID)
        {
            return true;
        }

        public bool Update_AppRun(int AppID, string key, string value)
        {
            //主要用于管理员对信息强制修改。

            return true;
        }
        public bool Update_AppRun(int AppID, string AppState, string IDCard, string Key, string Value)
        {
            //主要用于更新审核信息
            string[] temp_key, temp_value;
            
            temp_key = Key.Split(',');
            temp_value = Value.Split(',');
            if (temp_value.Length > 1)
                for (int j = 0; j < temp_value.Length ; j++)
                {
                    if (db_opt.execsql("  UPDATE [dzsw].[dbo].[Syl_SylAppRun]  SET  "+ temp_key[j].Trim()+ "=" + temp_value[j].Trim()
                        + " WHERE [AppID]=" + AppID + " and [Flow_State]='" + AppState + "' and [ApproveIDCard]='" + IDCard + "'") == false)
                    {break; }
                
                }
       
            return true;
        }

        public String[] back_select_jinbanren(int AppID, string AppState)
        {
            //返加上一步经办人员列表，注意：不同的是只返回上一步处理过流程的经办人[dzsw].[dbo].[Syl_SylAppRun]
            string[] name = null;
            return name;
        }
        /// <summary>
        /// 用于插入多条经办人信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool insert_AppRun(int AppID, string key, string value)
        {

            string[] temp_key, temp_value;
            string new_value = "";
            temp_key = key.Split(',');
            temp_value = value.Split(',');
            if (temp_value.Length > 1)
                for (int j = 0; j < temp_value.Length-1 ; j++)
                {
                    new_value += temp_value[j].Trim() + "','";
                }
            new_value = new_value + temp_value[temp_value.Length - 1].Trim()+"'";
            new_value = Convert.ToString(AppID) + ",'" + new_value;
            key = "AppID," + key;
            if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_SylAppRun] (" + key + ") values (" + new_value + ")"))
                    return true;
                else
                    return false;
          
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
        /// <summary>
        /// 更新两个表的信息 [dzsw].[dbo].[Syl_SylAppRun]，[dzsw].[dbo].[Syl_AppraiseInfo]
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>
        public bool update_flow(int flow_id)
        {
            //Update_AppRun(flow_id, "", "");
            //Update_AppraiseInfo(flow_id,,);


            return true;
        }
        public bool delete_flow(int flow_id)
        {
            return true;
        }

        /// <summary>
        /// 用于选择单条考核流程，返回指定的考核信息，可支持详单数据添充，
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>
        public DataRow select_sigleflow(int flow_id)
        {
            DataRow dr = null; ;
            return dr;
        }

        /// <summary>
        /// 从Syl_AppWorkerinfo表对被考核员工进行选择。主要用于修改被考核员工的考核信息修改后的即时显示。
        /// </summary>
        /// <param name="flow_id"></param>
        /// <param name="bgtime"></param>
        /// <param name="edtime"></param>
        /// <returns></returns>
        public DataSet select_appworkerinfo(int flow_id, string bgtime, string edtime)
        {
            //返回被考核的人员
            DataSet ds = new DataSet();
            ds = db_opt.build_dataset("select a.* from [dzsw].[dbo].[Syl_AppWorkerinfo] a,[dzsw].[dbo].[Syl_SylAppRun] b where a.AppID=b.AppID AND a.AppID="
                + flow_id);
            return ds;
        }


        /// <summary>
        /// 返回指定条件的考核流程数据集，主要用于总览
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <returns></returns>
        public DataSet select_zhonglan(string bgdatetime, string eddatetime)
        {

            DataSet ds = new DataSet();
            ds = db_opt.build_dataset("select * from [dzsw].[dbo].[Syl_AppraiseInfo] where TC_DateTime between '"
                + bgdatetime + "' and '" + eddatetime
            + "' order by  TC_DateTime desc, AppID");

            return ds;
        }
        /// <summary>
        ///  //返回待办考核流程数据集，主要用于填充待办
        /// </summary>
        /// <param name="bgdatetime"></param>
        /// <param name="eddatetime"></param>
        /// <param name="idcard"></param>
        /// <param name="flow_state"></param>
        /// <returns></returns>
        public DataSet select_daiban(string bgdatetime, string eddatetime, string idcard, string flow_state)
        {

            DataSet ds = new DataSet();
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "[ApproveIDCard]", idcard) && (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "[Oponion_State]", "待办理")
                || db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "[Oponion_State]", "回退")))

                ds = db_opt.build_dataset("select * from [dzsw].[dbo].[Syl_AppraiseInfo] a,[dzsw].[dbo].[Syl_SylAppRun] b where a.[AppID]=b.[AppID] and a.TC_DateTime between '"
                  + bgdatetime + "' and '" + eddatetime+"' and a.[Flow_State]='" + flow_state
                  + "' order by  a.TC_DateTime desc, a.AppID");
            else ds = null;
            return ds;

        }
        /// <summary>
        ///  //返回已办结考核流程数据集，主要用于填充已办结
        /// </summary>
        /// <param name="bgdatetime"></param>
        /// <param name="eddatetime"></param>
        /// <param name="idcard"></param>
        /// <param name="flow_state"></param>
        /// <returns></returns>
        public DataSet select_yibanjie(string bgdatetime, string eddatetime, string idcard, string flow_state)
        {

            DataSet ds = new DataSet();
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "[ApproveIDCard]", idcard) && db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "[Oponion_State]", "转交"))

                ds = db_opt.build_dataset("select * from [dzsw].[dbo].[Syl_AppraiseInfo] a,[dzsw].[dbo].[Syl_SylAppRun] b where a.[AppID]=b.[AppID] and a.TC_DateTime between '"
                  + bgdatetime + "' and '" + eddatetime
                 + "a.[Flow_State]<>'" + flow_state + "'"
                  + "' order by  a.TC_DateTime desc, a.AppID");
            return ds;
        }
        public bool update_shenpi_field(string idcard, string flow_id, string field1, string field2, string field3)
        {
            //更新流程审批字段内容，操作的是flow_run表
            return true;
        }
        /// <summary>
        /// 根据选择流转的方向 返回流程结点。
        /// </summary>
        /// <param name="next_OR_previous"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string[] get_step_list(int userlevel,string next_OR_previous, string flow_state)
        {
            //这个函数本质就是给出角色名，返回与给定角色名有交接关系的下一级角色名。
            // 1:部长,2:书记,3:主管领导,4:工程师,5:点检组长,6:点检
            //1：五级审批,2：四级审批,3：三级审批,4：二级审批,5：一级审批,6：起草
            string value = "";
            if (next_OR_previous == "转交")
            {
                switch (flow_state)
                {
                    case "部长":
                        value = "";
                        break;
                    case "书记":
                        value = "部长";
                        break;
                    case "主管领导":
                        value = "书记";
                        break;
                    case "工程师":
                        value = "主管领导";
                        break;
                    case "点检组长":
                        value = "工程师";
                        break;
                    case "点检":
                        value = "点检组长,工程师";
                        break;

                }
             
            }
            if (next_OR_previous == "回退")
            {
                switch (flow_state)
                {
                    case "部长":
                        value = "书记";
                        break;
                    case "书记":
                        value = "主管领导";
                        break;
                    case "主管领导":
                        value = "工程师";
                        break;
                    case "工程师":
                        value = "点检组长,点检";
                        break;
                    case "点检组长":
                        value = "点检";
                        break;
                    case "点检":
                        value = "";
                        break;
                }

            }

            if (userlevel ==0)
            {
                     value = "部长,书记,主管领导,工程师,点检组长,点检";

            }
           
            string[] ret_str;
           if(value!="")
            {
                ret_str = value.Split(',');
                return ret_str;              
            }
            
            return null;

        }

    }
}
