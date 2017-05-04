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
        /// <param name="username">用户名</param>
        /// <param name="user_idcard">用户身份证号</param>
        /// <returns></returns>
        public int build_newid(string username, string UserLevelName, string user_idcard)
        {

            int i = db_opt.max_id("[AppID]", "[dzsw].[dbo].[Syl_AppraiseInfo]");

            string old_id = db_opt.get_values("AppID", "[dzsw].[dbo].[Syl_SylAppRun]", "[Flow_State] like '%" + UserLevelName+"(起草)%' and ([Oponion_State]='' OR [Oponion_State] is null) and [ApproveIDCard]='" + user_idcard + "'");

            if (old_id != "")
            {
                i = Convert.ToInt32(old_id);
                db_opt.execsql("delete  from[dzsw].[dbo].[Syl_AppWorkerinfo] where appid =" + i);
                return i;
            }
            else
            {
                if (insert_AppraiseInfo(i, "[ApplicantName],[ApplicantIDCard]", username + "," + user_idcard) &&
             insert_AppRun(i, "[Flow_State],[ApproveName],[ApproveIDCard]", UserLevelName + "(起草)," + username + "," + user_idcard))

                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 作用：1，消除字符串中的单引号引发的数据库存取错误，2加入操作者姓名，操作时间，日志化操作信息
        /// </summary>
        /// <param name="cv_str">待转换字符串</param>
        /// <param name="opt_name">操作用户名</param>
        ///  <param name="opt_kind">操作种类：0：仅作单引号，逗号过滤。1：仅添加操作者信息</param>
        /// <returns></returns>
        public string convert_str(string cv_str,string opt_name,int opt_kind)
        {
            if (opt_kind == 0)
            {
                cv_str = cv_str.Replace(",", "+char(44)+");
                cv_str = cv_str.Replace("'", "''");
            }
            if(opt_kind==1)
            cv_str += "'+ Char(13)+Char(10)+'该信息由:" + opt_name + " 编辑于: "+DateTime.Now.ToString()+"'+Char(13)+Char(10)+'";
            if (opt_kind == 3)
            {
                cv_str = cv_str.Replace(",", "+char(44)+");
                cv_str = cv_str.Replace("'", "''");
                cv_str += "'+ Char(13)+Char(10)+'该信息由:" + opt_name + " 编辑于: " + DateTime.Now.ToString() + "'+Char(13)+Char(10)+'";
            }

                return cv_str;
        }

        public String Get_idcard_str(string names)
        {
            string idcard = "";

            names = names.Replace("(超级用户)", "");   //需要将NAMES字符串处理成"'skdks','safdf','sadfasdf')"
           

            data_reader = db_opt.datareader("select IDCard from [dzsw].[dbo].[Syl_WorkerInfo] where WorkerName in ( '" + names + "')");
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
            
            data_reader = db_opt.datareader("select WorkerName from [dzsw].[dbo].[Syl_WorkerInfo] where IDCard like '%" + idcards + "%'");
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
                return db_opt.build_dataset("select distinct ApproveName,[ApproveIDCard] from[dzsw].[dbo].[Syl_SylAppRun] where [Flow_State]='"
                    + StateName + "' and AppID=" + AppID + " and Oponion_State like '%转交%' ");
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
        
            /// <summary>
            /// 选择指定条件的员工数据集。
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
        public DataSet select_WorkerInfo(string where)
        {
            DataSet ds = new DataSet();
            ds = db_opt.build_dataset("select *  from [dzsw].[dbo].[Syl_WorkerInfo] where GroupName='" + where + "'");
            return ds;
        }



        //----------------------------------------------------------------------//




        //----------------------------下面是对表[dzsw].[dbo].[Syl_AppraiseInfo]的操作---------------------------//

        /// <summary>
        /// 向dzsw].[dbo].[Syl_AppraiseInfo]插入考核信息。
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
           new_value = new_value.Replace("+char(44)+", ",");
           key = "AppID," + key;
//需要做一个字符串处理函数，返回值为K,K,K,K V,V,V,V 或K=V,K=V,K=V 唯一的问题是处理好逗号，单引号
            if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppraiseInfo](" + key + ") values (" + new_value + ")"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新表[dzsw].[dbo].[Syl_AppraiseInfo]数据
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
                        db_opt.execsql("  update [dzsw].[dbo].[Syl_AppraiseInfo] set   " + temp_key[i].Trim() + "='" + temp_value[i].Trim().Replace("+char(44)+", ",") + "' where AppID=" + AppID);
                }
                return true;
            }
            else
            {
                return false;
            }
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
        public bool insert_single_AppWorkerInfo(int AppID, string AppIDCard, string key, string value)
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
          
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "AppID=" + AppID + " and AppIDCard='" + AppIDCard + "'") == false)
            {
                db_opt.execsql("insert into [dzsw].[dbo].[Syl_AppWorkerinfo] ( AppID," + key.Trim() + ") values (" + AppID + ",'" + new_value.Trim().Replace("+char(44)+", ",") + "')");
                return true;

            }
            else {
                return false;
            }


        }
        public bool IsExists(string table, string where)
        {
            if (db_opt.IsRecordExist(table, where))
            {
                return true;
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
            string new_update_str = "";
            temp_key = Key.Split(',');
            temp_value = Value.Split(',');
            for (int i = 0; i < temp_key.Length; i++)
            {
                if (temp_key[i].Trim() == "[AppAmount]")
                    new_update_str += temp_key[i].Trim() + "=" + temp_value[i].Trim() + ",";
                else
                    new_update_str += temp_key[i].Trim() + "='" + temp_value[i].Trim() + "',";

            }
            new_update_str = new_update_str.Substring(0, new_update_str.Length - 1);
            new_update_str = new_update_str.Replace("+char(44)+", ",");
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "[AppIDCard]='" + IDCard + "'and [AppID]=" + AppID))
            {
                if (db_opt.execsql("update [dzsw].[dbo].[Syl_AppWorkerinfo] set   " + new_update_str
                       + " where AppID=" + AppID + " and [AppIDCard]='" + IDCard + "'"))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 根据AppID更新指定列数据
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool Update_AppWorkerInfo(int AppID, string Key, string Value)
        {
            string[] temp_key, temp_value;
            string new_update_str = "";
            temp_key = Key.Split(',');
            temp_value = Value.Split(',');
            for (int i = 0; i < temp_key.Length; i++)
            {
                if (temp_key[i].Trim() == "[AppAmount]")
                    new_update_str += temp_key[i].Trim() + "=" + temp_value[i].Trim() + ",";
                else
                    new_update_str += temp_key[i].Trim() + "='" + temp_value[i].Trim()+ "',";

            }
            new_update_str = new_update_str.Substring(0, new_update_str.Length - 1);
            new_update_str = new_update_str.Replace("+char(44)+", ",");
            if (db_opt.execsql("update [dzsw].[dbo].[Syl_AppWorkerinfo] set   " + new_update_str
                      + " where AppID=" + AppID ))
                return true;
            else
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
        public bool delete_AppWorkerInfo(int AppID)
        {
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_AppWorkerinfo]", "[AppID]=" + AppID))

                if (db_opt.execsql("delete from [dzsw].[dbo].[Syl_AppWorkerinfo] where AppID=" + AppID ))
                    return true;
            return false;


        }

        //----------------------------------------------------------------------//


        //----------------------------下面是对表[dzsw].[dbo].[Syl_SylAppRun]的操作---------------------------//

        public bool delete_AppRun(int AppID)
        {
            return true;
        }
        public string select_wei_huiqianren(int AppID,string dangqianbanliren_idcard )
        {
            DataSet ds;
            string ret_str = "";
            ds=db_opt.build_dataset("SELECT[ApproveName] from[dzsw].[dbo].[Syl_SylAppRun] where  appid =" + AppID + " and [Oponion_State] = '待办理' and [ApproveIDCard] <> '" + dangqianbanliren_idcard+"'");
            if (ds != null)
                if (ds.Tables[0] != null)
                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i< ds.Tables[0].Rows.Count; i++)
                        {
                            ret_str+= ds.Tables[0].Rows[i][0].ToString()+",";
                        }
            if (ret_str != "") return ret_str.Substring(0, ret_str.Length - 1);
            else return "空";

        }
        public int select_shenpi_renshu(int AppID,string flow_state)
        {
            DataSet ds;
            int ret_num = 0;
            ds = db_opt.build_dataset("SELECT[ApproveName] from[dzsw].[dbo].[Syl_SylAppRun] where  appid =" + AppID + " and [Flow_State] = '"+flow_state+"'");
            if (ds != null)
                if (ds.Tables[0] != null)
                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ret_num++;
                        }
            if (ret_num != 0) return ret_num;
            else return 0;

        }

        /// <summary>
        /// //主要用于更新审核信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="AppState"></param>
        /// <param name="IDCard"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="is_qiangzhi"></param>
        /// <returns></returns>
        public bool Update_AppRun(int AppID, string AppState, string IDCard, string Key, string Value, bool is_qiangzhi)
        {
            //主要用于更新审核信息
            string[] temp_key, temp_value;

            temp_key = Key.Split(',');
            temp_value = Value.Split(',');
            if (temp_value.Length > 1)
            {
                if (is_qiangzhi == false)
                {
                    for (int i = 0; i < temp_value.Length; i++)
                    {
                        if (temp_key[i] == "[Oponion_DateTime]")
                        {
                            db_opt.execsql("  UPDATE [dzsw].[dbo].[Syl_SylAppRun]  SET  " + temp_key[i].Trim() + "=" + temp_value[i].Trim()
                              + " WHERE [AppID]=" + AppID + " and [Flow_State] like '%" + AppState + "%' and [ApproveIDCard]='" + IDCard + "' and [Oponion_DateTime] is null");
                        }
                        else db_opt.execsql("  UPDATE [dzsw].[dbo].[Syl_SylAppRun]  SET  " + temp_key[i].Trim() + "='" + temp_value[i].Trim().Replace("+char(44)+", ",")
                            + "' WHERE [AppID]=" + AppID + " and [Flow_State] like '%" + AppState + "%' and [ApproveIDCard]='" + IDCard + "'and [Oponion_DateTime] is null");
                    }
                }
                if (is_qiangzhi )//只有在会签模式时需要更新其它会签人数据
                {
                   
                    for (int j = 0; j < temp_value.Length; j++)
                    {
                        if (temp_key[j] == "[Oponion_DateTime]")
                        {
                            db_opt.execsql("  UPDATE [dzsw].[dbo].[Syl_SylAppRun]  SET  " + temp_key[j].Trim() + "=" + temp_value[j].Trim()
                              + " WHERE [AppID]=" + AppID + " and [Flow_State] like '%" + AppState + "%' and [ApproveIDCard]!='" + IDCard + "'and [Oponion_DateTime] is null");
                        }
                        else db_opt.execsql("  UPDATE [dzsw].[dbo].[Syl_SylAppRun]  SET  " + temp_key[j].Trim() + "='" + temp_value[j].Trim().Replace("+char(44)+", ",")
                            + "' WHERE [AppID]=" + AppID + " and [Flow_State] like '%" + AppState + "%' and [ApproveIDCard]!='" + IDCard + "'and [Oponion_DateTime] is null");


                    }
                }
              
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
                for (int j = 0; j < temp_value.Length - 1; j++)
                {
                    new_value += temp_value[j].Trim() + "','";
                }
            new_value = new_value + temp_value[temp_value.Length - 1].Trim() + "'";
            new_value = Convert.ToString(AppID) + ",'" + new_value;
            new_value = new_value.Replace("+char(44)+", ",");
            key = "AppID," + key;
            if (db_opt.execsql("insert into [dzsw].[dbo].[Syl_SylAppRun] (" + key + ") values (" + new_value + ")"))
                return true;
            else
                return false;

        }

        //----------------------------------------------------------------------//
        /// 下面功能用于发考核，修改、删除考核的操作。


        /// <summary>
        /// 下面功能用于发考核，修改、删除考核的操作。
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>nextORprevious
        public bool update_flow(int flow_id, string state_now, string IDCard,string AppContent,
            string AppBy,string nextORprevious,string Applevel,string AppKind,string AppAmount,string FS_DateTime,
            string AppGroup,string AppName_str,string step)
        {
            //将填写的流程信息写入三个表。这里对AppWorkerInfo的列新是部分的，按列形式统一填充的，在每人加扣多少钱的操信息在添加并刷新按钮中已经完成。
            string RealName = Get_name_str(IDCard);
            if (Update_AppRun(flow_id, state_now, IDCard, "[ApproveOponion],[App_Comment],[Oponion_State],[Oponion_DateTime]", convert_str(AppContent, RealName, 0)
                   + "," + convert_str(AppBy, RealName, 0)
               + "," + nextORprevious + ",getdate()", false) &&

             Update_AppraiseInfo(flow_id, "[Flow_State],[Applevel],[AppKind] ,[AppAmount] ,[TC_DateTime] ,[FS_DateTime],[AppGroup],[AppNames] ,[AppContent] ,[AppBy]", step
                 + "," + Applevel + "," + AppKind + "," + AppAmount + ",getdate(),"
                 + FS_DateTime + "," + AppGroup + "," + AppName_str
                 + "," + convert_str(AppContent, RealName, 0)
                 + "," + convert_str(AppBy, RealName, 0)) &&

             Update_AppWorkerInfo(flow_id, "[FS_DateTime],[AppLevel],[AppKind],[AppContent],[AppBy]", FS_DateTime
                + "," + Applevel + "," + AppKind
                 + "," + convert_str(AppContent, RealName, 0)
                 + "," + convert_str(AppBy, RealName, 0)))
                return true;
            else 
             return false;
           
        }
        /// <summary>
        /// 更新两个表的信息 [dzsw].[dbo].[Syl_SylAppRun]，[dzsw].[dbo].[Syl_AppraiseInfo]
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>

        public bool delete_AppFlow(int AppID)
        {
            if (db_opt.execsql("delete  from [dzsw].[dbo].[Syl_AppraiseInfo] where AppID=" + AppID)
                && db_opt.execsql("delete  from [dzsw].[dbo].[Syl_AppWorkerinfo] where AppID=" + AppID) 
                && db_opt.execsql("delete  from [dzsw].[dbo].[Syl_SylAppRun] where AppID=" + AppID))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除考核流程。即使强制模式，一般用户强制生效一个流程也只能是待办项里的。只有管理员或办事员可以将任意节点的考核直接生效。修改，归档，也是同理。
        /// </summary>
        /// <param name="AppID">流程ID</param>
        /// <param name="Flow_State">流程当前状态</param>
        /// <param name="IDCard">操作人身份证号</param>
        /// <param name="UserLevel">操作人角色等级</param>
        /// <param name="Oponion_State">当前办理状态：回退，待办理</param>
        /// <param name="is_qiangzhi">是否为强制模式</param>
        /// <returns></returns>
        public bool delete_AppFlow(int AppID, string Flow_State, string IDCard, string UserLevel, string Oponion_State, string is_qiangzhi)
        {
            if (db_opt.execsql("delete * from [dzsw].[dbo].[Syl_AppraiseInfo] where AppID=" + AppID)
                && db_opt.execsql("delete * from [dzsw].[dbo].[Syl_AppWorkerinfo] where AppID=" + AppID)
                && db_opt.execsql("delete * from [dzsw].[dbo].[Syl_SylAppRun] where AppID=" + AppID))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 归档考核流程，使考核生效。即使强制模式，一般用户强制生效一个流程也只能是待办项里的。只有管理员或办事员可以将任意节点的考核直接生效。修改，删除，也是同理。
        /// </summary>
        /// <param name="AppID">流程ID</param>
        /// <param name="Flow_State">流程当前状态</param>
        /// <param name="IDCard">操作人身份证号</param>
        /// <param name="UserLevel">操作人角色等级</param>
        /// <param name="Oponion_State">当前办理状态：回退，待办理</param>
        /// <param name="is_qiangzhi">是否为强制模式</param>
        /// <returns></returns>
        public bool guidang_AppFlow(int AppID, string ApproveIDCard)
        {
            if (db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "ApproveIDCard='" + ApproveIDCard + "' and  [Oponion_DateTime] is null") == false)
                insert_AppRun(AppID, "[Flow_State],[ApproveName],[ApproveIDCard],[Oponion_State]", "生效," +Get_name_str(ApproveIDCard)+","+ ApproveIDCard+",生效");

            if (db_opt.execsql("update [dzsw].[dbo].[Syl_AppraiseInfo] set [Flow_State]= '生效',[Admin_Opt]='归档',[Admin_Opt_Comment]='考核已经生效，数据已归档！' where AppID = " + AppID)
                && db_opt.execsql("update [dzsw].[dbo].[Syl_AppWorkerinfo]  set [App_State]= '生效' where AppID = " + AppID)
                  && db_opt.execsql("update [dzsw].[dbo].[Syl_SylAppRun] set [ApproveOponion]='归档',[App_Comment]='考核已经生效，数据已归档！' ,[Oponion_State]='生效',[Oponion_DateTime]=getdate() where AppID = " + AppID + " and [Oponion_DateTime] is null"))



                //&& db_opt.execsql("update [dzsw].[dbo].[Syl_SylAppRun] set [ApproveOponion]='归档',[App_Comment]='考核已经生效，数据已归档！' ,[Oponion_State]='生效',[Oponion_DateTime]=getdate() where AppID = " + AppID + " and ApproveIDCard='" + ApproveIDCard + "'"))

            {

                db_opt.execsql("delete [dzsw].[dbo].[Syl_SylAppRun]  where [Oponion_DateTime]<dateadd(month,-2, getdate())");
                db_opt.execsql("delete [dzsw].[dbo].[Syl_AppWorkerinfo]  where [FS_DateTime]<dateadd(year,-2,getdate())");

                return true;
            }
            else
                return false;

        }
        public bool weijingbanren_fengkou(int AppID, string wei_ApproveIDCard, string ApproveIDCard)
        {
            if(db_opt.IsRecordExist("[dzsw].[dbo].[Syl_SylAppRun]", "[AppID]="+ AppID + "and ApproveIDCard='" + wei_ApproveIDCard + "' and  [Oponion_DateTime] is null"))
                db_opt.execsql("update [dzsw].[dbo].[Syl_SylAppRun] set [ApproveOponion]='(强制)归档',[App_Comment]='考核已经被 "+Get_name_str(ApproveIDCard)+ " （强制）生效，数据已归档！' ,[Oponion_State]='（强制）生效',[Oponion_DateTime]=getdate() where AppID = " + AppID + " and ApproveIDCard='" + wei_ApproveIDCard + "'and  [Oponion_DateTime] is null");
       
            return true;
        }

        /// <summary>
        /// 用于选择单条考核流程，返回指定的考核信息，可支持详单数据添充，
        /// </summary>
        /// <param name="flow_id"></param>
        /// <returns></returns>
        public DataRow select_sigleflow(int flow_id)
        {
            DataRow dr = null;
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
            ds = db_opt.build_dataset("select * from [dzsw].[dbo].[Syl_AppWorkerinfo] where AppID="
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

                ds = db_opt.build_dataset("select a.* from [dzsw].[dbo].[Syl_AppraiseInfo] a,[dzsw].[dbo].[Syl_SylAppRun] b where a.[AppID]=b.[AppID] and a.TC_DateTime between '"
                  + bgdatetime + "' and '" + eddatetime + "' and a.[Flow_State]='" + flow_state + "' and b.[ApproveIDCard]='" + idcard
                  + "' and  b.[Oponion_State]='待办理'"
                  + " order by  a.TC_DateTime desc, a.AppID");
            else
                ds = null;
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
     

                ds = db_opt.build_dataset("select distinct a.* from [dzsw].[dbo].[Syl_AppraiseInfo] a,[dzsw].[dbo].[Syl_SylAppRun] b where a.[AppID]=b.[AppID] and a.TC_DateTime between '"
                  + bgdatetime + "' and '" + eddatetime
                 + "' and (b.[Flow_State]='" + flow_state + "' or a.[Flow_State] like '%生效%'or b.[Flow_State] like '%起草%') and( b.[Oponion_State] like '%转交%' or  b.[Oponion_State] like '%回退%'or  b.[Oponion_State] like '%会签%'or  b.[Oponion_State] like '%生效%') and b.[ApproveIDCard]='" + idcard
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
        public string[] get_step_list(int userlevel, string next_OR_previous, string flow_state)
        {
            //这个函数本质就是给出角色名，返回与给定角色名有交接关系的下一级角色名。
            // 1:部长,2:书记,3:主管领导,4:工程师,5:点检组长,6:点检
            //1：五级审批,2：四级审批,3：三级审批,4：二级审批,5：一级审批,6：起草
            string value = "";
            if (next_OR_previous == "转交")
            {
                switch (flow_state)
                {
                    case "办事员":
                        value = "部长,书记,主管领导,工程师,点检组长,安全员,点检";
                        break;
                         
                    case "部长":
                        value = "办事员";
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
                    case "安全员":
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
                    case "办事员":
                        value = "部长,书记,主管领导,工程师,点检组长,安全员,点检";
                        break;
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
                        value = "点检组长,安全员,点检";
                        break;
                    case "点检组长":
                        value = "点检";
                        break;
                    case "安全员":
                        value = "";
                        break;
                    case "点检":
                        value = "";
                        break;
                }

            }

            if (userlevel == 0||userlevel==8)
            {
                value = "部长,书记,主管领导,工程师,点检组长,安全员,点检";

            }

            string[] ret_str;
            if (value != "")
            {
                ret_str = value.Split(',');
                return ret_str;
            }

            return null;

        }

    }
}
