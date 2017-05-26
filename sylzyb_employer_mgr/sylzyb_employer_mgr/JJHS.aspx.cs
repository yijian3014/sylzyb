using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using Microsoft.VisualBasic;
namespace sylzyb_employer_mgr
{
    
    public partial class JJHS : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //根据Session值验证用户是否登录
            if (Session["RealName"] == "" || Session["RealName"] == null || Session["IDCard"] == null || Session["IDCard"] == null || Session["UserName"] == null || Session["UserLevel"] == null || Session["UserLevelName"] == null || Session["UserPower"] == null || Session["ModulePower"] == null)
            {
                Response.Write("<script language='javascript'>alert('您尚未登陆或登陆超时');location.href='Login.aspx';</script>");
                Response.End();
            }

            if(!IsPostBack)
            {
                CheckPower();//判断用户是否具有相应权限，并执行对应的操作。
            }

            CheckPower();//判断用户是否具有相应权限，并执行对应的操作。
        }
//=================================================================================================程序通用方法开始
//-------------------------------------------------------------------用户权限相关方法开始
        public Boolean CheckUserPower(int n)//根据用户相应权限编码，判断用户是否具有相关权限
        { 
            string a=Session["UserPower"].ToString().Substring (n,1);
            if (a == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CheckPower()//根据用户权限编码的检查结果，执行对应的操作。
        {
            if (CheckUserPower(9) || CheckUserPower(10))//判断用户是否至少拥有奖金查询以及奖金核算之中的一种权限。
            {
                if (Bt_Input.Visible)
                {
                    if (!CheckUserPower(10))//若用户没有奖金核算权限（第10位，PS：为进入if判断所以取判断的相反值）则隐藏相应按钮，并跳转至奖金查询页面
                    {
                        //隐藏相应按钮。
                        Bt_Input.Visible = false;
                        //跳转至奖金查询页面。
                        MultiView1.ActiveViewIndex = 0;
                        ChangeV1DateDDL();//根据当前日期自动修改日期DropDownList中的数据及选中项。

                    }
                }
                if (Bt_Select.Visible)//若奖金查询按钮显示，则对用户权限进行相应判断。
                {
                    if (!CheckUserPower(9))//若用户没有奖金查询权限，则跳转至奖金录入界面，并隐藏奖金查询按钮。
                    {
                        Bt_Select.Visible = false;
                        MultiView1.ActiveViewIndex = 1;
                        ChangeV2DateDDL();//根据当前日期修改各DropDawnList中的信息及选中项
                    }
                }

            }

            else//若一种都没有则返回登录页面
            {
                //返回登录页面
                Response.Write("<script language='javascript'>alert('您没有相应的权限。');location.href='Login.aspx';</script>");
                Response.End();
            }
        }


//-------------------------------------------------------------------用户权限相关方法结束
//-------------------------------------------------------------------页面跳转相关方法开始
        protected void Bt_Select_Click(object sender, EventArgs e)//跳转值奖金查询页面
        {
            MultiView1.ActiveViewIndex = 0;
            ChangeV1DateDDL();//根据当前日期自动修改日期DropDownList中的数据及选中项。
                      
        }
        protected void Bt_Input_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ChangeV2DateDDL();//根据当前日期修改各DropDawnList中的信息及选中项
        }
//-------------------------------------------------------------------页面跳转相关方法结束
//=================================================================================================程序通用方法结束


//=================================================================================================各页面使用的代码开始
//-------------------------------------------------------------------奖金查询页面中的代码开始
        public void ChangeV1DateDDL()//根据当前日期修改各DropDawnList中的信息及选中项
        { 
            //分别为年、月变量赋值。
            int Y=DateTime.Now.Year;
            int M = DateTime.Now.Month;

            for (int i = 5; i >= -2; i--)//利用for循环为DDL_V1_Year添加前5年以及后1年的年份
            {
                DDL_V1_Year.Items.Insert(5-i, new ListItem((Y - i).ToString(), (Y - i).ToString()));
            }

            if (M == 1)//若本月为1月则自动选择上一年的12月。
            {
                DDL_V1_Month.SelectedIndex = 11;
                DDL_V1_Year.SelectedIndex = 4;
            }
            else
            {
                //若不是1月则自动选择本年的上一个月。
                DDL_V1_Month.SelectedIndex = M-2;
                DDL_V1_Year.SelectedIndex = 5;
            }
            
        }
//--------------------------------声明刷新表格方法开始
        public void Update_GVGroup()//班组奖金表
        {
            string sql;//定义sql语句变量。
            sql = "SELECT * FROM Syl_Bonus_Group WHERE G_BonusDate=" + DDL_V1_Year.SelectedValue.ToString() + DDL_V1_Month.SelectedValue.ToString() + " ORDER BY OrderOfShow";
            GV_V1_Group.DataSource = bc.GetDataSet(sql, "Syl_Bonus_Group");
            GV_V1_Group.DataKeyNames = new string[] { "ID" };
            GV_V1_Group.DataBind();
        }
        public void Update_GVPerson()//个人奖金表
        {
            string sql;//定义sql语句变量。
            sql = "SELECT * FROM Syl_Bonus_Person WHERE P_BonusDate=" + DDL_V1_Year.SelectedValue.ToString() + DDL_V1_Month.SelectedValue.ToString() + " AND P_GroupName='" + DDL_V1_Group.SelectedValue.ToString() + "'";
            GV_V1_Person.DataSource = bc.GetDataSet(sql, "Syl_Bonus_Person");
            GV_V1_Person.DataKeyNames = new string[] { "ID" };
            GV_V1_Person.DataBind();
        }
        public void Update_GVBase()//基础奖金表
        {
            string sql;//定义sql语句变量。
            sql = "SELECT * FROM Syl_Bonus_Base WHERE BonusDate=" + DDL_V1_Year.SelectedValue.ToString() + DDL_V1_Month.SelectedValue.ToString();
            GV_V1_Base.DataSource = bc.GetDataSet(sql, "Syl_Bonus_Base");
            GV_V1_Base.DataKeyNames = new string[] { "ID" };
            GV_V1_Base.DataBind();
        }
//--------------------------------声明刷新表格方法结束
        protected void DDL_V1_Kind_SelectedIndexChanged(object sender, EventArgs e)//更改DDL_V1_Kind中的选项时触发。
        {
            if (DDL_V1_Kind.SelectedValue.ToString() == "个人")//若选择个人则显示班组选择相应控件。
            {
                Label5.Visible = true;
                DDL_V1_Group.Visible = true;
            }
            else
            {
                //若选择班组则隐藏班组选择相应控件。
                Label5.Visible = false;
                DDL_V1_Group.Visible = false;
            }
        }

        protected void Bt_V1_Select_Click(object sender, EventArgs e)//点击查询按钮时触发
        {
            

            if (DDL_V1_Kind.SelectedItem.ToString() == "班组")//若选择为班组，根据页面信息刷新GV_V1_Group表的相应信息。
            {
                //刷新班组奖金表
                Update_GVGroup();
                //改变显示的表格。
                GV_V1_Group.Visible = true;
                GV_V1_Person.Visible = false;
                GV_V1_Base.Visible = false;

            }
            else if (DDL_V1_Kind.SelectedItem.ToString() == "个人")//若选择不是班组，根据页面信息刷新GV_V1_Person表的相应信息。
            {
                //刷新个人奖金表
                Update_GVPerson();
                //改变显示的表格。
                GV_V1_Person.Visible = true;
                GV_V1_Group.Visible = false;
                GV_V1_Base.Visible = false;
            }
            else
            {
                //刷新基础奖金表
                Update_GVBase();
                //改变显示的表格。
                GV_V1_Base.Visible = true;
                GV_V1_Group.Visible = false;
                GV_V1_Person.Visible = false;
            }
            
        }


                
//-------------------------------------------------------------------奖金查询页面中的代码结束

//-------------------------------------------------------------------奖金录入页面中的代码开始
        public void ChangeV2DateDDL()//根据当前日期修改各DropDawnList中的信息及选中项
        {
            //分别为年、月变量赋值。
            int Y = DateTime.Now.Year;
            int M = DateTime.Now.Month;

            for (int i = 5; i >= -2; i--)//利用for循环为DDL_V1_Year添加前5年以及后1年的年份
            {
                DDL_V2_Year.Items.Insert(5 - i, new ListItem((Y - i).ToString(), (Y - i).ToString()));
            }

            if (M == 1)//若本月为1月则自动选择上一年的12月。
            {
                DDL_V2_Month.SelectedIndex = 11;
                DDL_V2_Year.SelectedIndex = 4;
            }
            else
            {
                //若不是1月则自动选择本年的上一个月。
                DDL_V2_Month.SelectedIndex = M - 2;
                DDL_V2_Year.SelectedIndex = 5;
            }
        }

        protected void Bt_V2_Insert_Click(object sender, EventArgs e)//点击检查（提交）按钮时触发（PS：打开页面后，首次点击会弹出一个提示框，内容为输入的信息。再次点击才会提交。）
        {
            if (TB_V2_CFZJ.Text != "" && TB_V2_LDJJ.Text != "" && TB_V2_GLJ.Text != "" && TB_V2_ZZGS.Text != "" && TB_V2_QT1.Text != "" && TB_V2_QT2.Text != "" && TB_V2_QT3.Text != "")
            {   //检查是否有文本框为空。
                int n;//为检查所输入内容是否是整数创建的变量。
                if (int.TryParse(TB_V2_CFZJ.Text, out n) && int.TryParse(TB_V2_LDJJ.Text, out n) && int.TryParse(TB_V2_GLJ.Text, out n) && int.TryParse(TB_V2_ZZGS.Text, out n) && int.TryParse(TB_V2_QT1.Text, out n) && int.TryParse(TB_V2_QT2.Text, out n) && int.TryParse(TB_V2_QT3.Text, out n))
                {//检查输入内容是否为整数。

                    if (Convert.ToBoolean(Label17.Text))
                    {   //Label17.Text初始内容为true，首次点击时会弹出提示框提醒用户检查所输入的内容，然后将Label17的Text改为false，Bt_V2_Insert的Text改为提交。
                        Label17.Text = "false";
                        Bt_V2_Insert.Text = "提交";
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请检查：\\n奖金日期为：" + DDL_V2_Year.Text + DDL_V2_Month.Text + "。\\n厂发总奖为：" + TB_V2_CFZJ.Text + "。\\n自主改善将为："+TB_V2_ZZGS.Text+"。\\n车间领导奖金为：" + TB_V2_LDJJ.Text + "。\\n管理奖为：" + TB_V2_GLJ.Text + "。\\n其它一为："+TB_V2_QT1.Text+"。\\n其它二为："+TB_V2_QT2.Text+"。\\n其它三为："+TB_V2_QT3.Text+"。\\n若检查无误请点击提交按钮，提交数据。');</script>");
                    }
                    else
                    {//第二次点击Bt_V2_Insert时执行存储过程，将数据提交至数据库。
                        Bt_V2_Insert.Visible = false;//点击提交后为防止用户重复点击，将提交按钮隐藏。

                        string sql, sqlString;
                        sqlString = "server=DBCLUSERVER;uid=admin;pwd=admin;database=dzsw";
                        sql = "Syl_Bonus_BaseInput";
                        SqlConnection sqlCon = new SqlConnection(sqlString);
                        SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@CFZJ", SqlDbType.Decimal).Value =Convert.ToInt32(TB_V2_CFZJ.Text);
                        sqlCmd.Parameters.Add("@ZZGS", SqlDbType.Decimal).Value = TB_V2_ZZGS.Text;
                        sqlCmd.Parameters.Add("@LDJJ", SqlDbType.Decimal).Value = TB_V2_LDJJ.Text;
                        sqlCmd.Parameters.Add("@GLJ", SqlDbType.Decimal).Value = TB_V2_GLJ.Text;
                        sqlCmd.Parameters.Add("@QT1", SqlDbType.Decimal).Value = TB_V2_QT1.Text;
                        sqlCmd.Parameters.Add("@QT2", SqlDbType.Decimal).Value = TB_V2_QT2.Text;
                        sqlCmd.Parameters.Add("@QT3", SqlDbType.Decimal).Value = TB_V2_QT3.Text;
                        sqlCmd.Parameters.Add("@BonusDate", SqlDbType.Int).Value = DDL_V2_Year.Text + DDL_V2_Month.Text;
                        sqlCon.Open();
                        if (sqlCmd.ExecuteNonQuery() > 0)
                        {//检查执行存储过程的返回行数，若不大于0则视为执行失败，提醒用户重试。
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('提交成功。');</script>");
                            Bt_V2_Insert.Visible = true;//存储过程执行完毕后显示提交按钮。
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('提交失败，请重试。');</script>");
                            Bt_V2_Insert.Visible = true;//存储过程执行完毕后显示提交按钮。
                        }
                        sqlCon.Close();
                    }
                }
                else
                {//有输入内容不是整数。
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('只能输入整数，请检查。');</script>");
                }
            }
            else
            {//页面信息填写不完整。
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请将页面信息填写完整。');</script>");
            }


        }



//-------------------------------------------------------------------奖金录入页面中的代码结束
//-------------------------------------------------------------------返回登录界面
        protected void Bt_Return_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>location.href='Login.aspx';</script>");
            Response.End();
        }

        protected void GV_V1_Group_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GV_V1_Group.EditIndex = e.NewEditIndex;
            Update_GVGroup();
        }

        protected void GV_V1_Group_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.GV_V1_Group.EditIndex = e.RowIndex;
            string ID = this.GV_V1_Group.DataKeys[e.RowIndex].Value.ToString();
            string Sql;
            string G_Coefficient = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim();
            Sql = "Update Syl_Bonus_Group Set G_Coefficient=" + G_Coefficient;

            string G_BaseBonus = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Trim();
            Sql += ", G_BaseBonus =" + G_BaseBonus;

            string G_DueBonus = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim();
            Sql += ", G_DueBonus=" + G_DueBonus;

            string G_PlantApp = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[5].Controls[0]).Text.Trim();
            Sql += ", G_PlantApp=" + G_PlantApp;

            string G_DepartmentApp = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[6].Controls[0]).Text.Trim();
            Sql += ", G_DepartmentApp=" + G_DepartmentApp; 

            string G_ZZGS = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[7].Controls[0]).Text.Trim();
            Sql += ", G_ZZGS=" + G_ZZGS;

            string G_QT1 = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[8].Controls[0]).Text.Trim();
            Sql += ", G_QT1=" + G_QT1;

            string G_QT2 = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[9].Controls[0]).Text.Trim();
            Sql += ", G_QT2=" + G_QT2;

            string G_QT3 = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[10].Controls[0]).Text.Trim();
            Sql += ", G_QT3=" + G_QT3;

            string G_ActualBonus = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[11].Controls[0]).Text.Trim();
            Sql += ", G_ActualBonus=" + G_ActualBonus;

            string AverageBonus = ((TextBox)this.GV_V1_Group.Rows[e.RowIndex].Cells[12].Controls[0]).Text.Trim();
            Sql += ", AverageBonus=" + AverageBonus;

            Sql += "Where ID="+ID;
            if (bc.ExecSQL(Sql))
            {
                Response.Write("<script language=javascript> alert('更改数据成功！')</script>");
                this.GV_V1_Group.EditIndex = -1;
            }
            Update_GVGroup();
        }

        protected void GV_V1_Group_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GV_V1_Group.EditIndex = -1;
            Update_GVGroup();
        }

//=================================================================================================各页面使用的代码结束
    }
}

