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
    public partial class USER : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();//调用数据库连接方法。
        protected void Page_Load(object sender, EventArgs e)
        {
            //根据Session值验证用户是否登录
            if (Session["RealName"].ToString() == "" || Session["RealName"] == null || Session["IDCard"] == null || Session["IDCard"] == null || Session["UserName"] == null || Session["UserLevel"] == null || Session["UserLevelName"] == null || Session["UserPower"] == null || Session["ModulePower"] == null)
            {
                Response.Write("<script language='javascript'>alert('您尚未登陆或登陆超时');location.href='Login.aspx';</script>");
                Response.End();
            }
            if (!IsPostBack)
            {   //验证用户是否有管理用户的权限
                CheckUserPower(8);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('检查操作');</script>");

            }
            if (CheckUserPower(8))
            {
                TruePowerVisible();
            }
            else
            {
                FalsePowerVisible();
            }
        }
//=================================================================================================程序中调用的方法开始
//-------------------------------------------------------------------------------用户权限判定相关方法开始
        public Boolean CheckUserPower(int n)//验证用户是否具有使用相应功能的权限
        {
            string a = Session["UserPower"].ToString().Substring(n,1);
            if (a == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PC_OnViewChange()//在页面跳转时判断用户权限（PC:PowerCheck）
        { 
            if(CheckUserPower(8))//判断用户是否有权限修改用户权限
            {
                //若用户的UserPower的第8位（不算第一位）为Y则证明用户有权限修改用户权限
            }
            else
            {
                //若用户的UserPower的第8位（不算第一位）不为Y则证明用户有权限修改用户权限。
                FalsePowerVisible();//调用用户没有权限方法
                Response.Write("<script language='javascript'>alert('您没有权限执行此项操作');</script>");//提示用户没有权限执行此项操作
            } 
        }
 
        public void TruePowerVisible()//当用户有权限管理用户时显示相应功能按钮
        {
            if (Bt_P2_ChangePower.Visible || Bt_P2_ManUser.Visible || Bt_P2_AddUser.Visible)
            { }
            else
            {
                Bt_P2_ChangePower.Visible = true;//修改用户权限按钮显示。
                Bt_P2_ManUser.Visible = true;//管理用户权限按钮显示。
                Bt_P2_AddUser.Visible = true;//新增用户按钮显示。
            }

        }
        public void FalsePowerVisible()//当用户没有权限管理时执行此方法。
        {
            if (Bt_P2_ChangePower.Visible || Bt_P2_ManUser.Visible || Bt_P2_AddUser.Visible)//若用户没有相应权限，但功能按钮处于显示状态，则隐藏所有功能按钮，并返回修改密码页面。
            { 
            Bt_P2_ChangePower.Visible = false;//修改用户权限按钮不显示。
            Bt_P2_ManUser.Visible = false;//管理用户权限按钮不显示。
            Bt_P2_AddUser.Visible = false;//新增用户按钮不显示。
            MultiView1.ActiveViewIndex = 0;//返回修改密码页面。
            }

        }
//-------------------------------------------------------------------------------用户权限判定相关方法结束

//-------------------------------------------------------------------------------修改用户权限中的方法开始
        public void RFGVPower()//刷新修改权限页面中的用户信息表
        {
            GV_V2_Power.DataSource = bc.GetDataSet("SELECT * FROM Syl_UserInfo WHERE UserLevel!=0 ORDER BY UserLevel","Syl_UserInfo");
            GV_V2_Power.DataKeyNames = new string[] { "ID"};
            GV_V2_Power.DataBind();
        }

//-------------------------------------------------------------------------------修改用户权限中的方法结束

//=================================================================================================程序中调用的方法结束


//=================================================================================================按钮点击事件开始
        //-------------------------------------------------------------------------------页面跳转按钮开始
        protected void Bt_P2_ChangPassWord_Click(object sender, EventArgs e)//修改密码按钮
        {
            //跳转至View1.
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Bt_P2_ChangePower_Click(object sender, EventArgs e)//修改用户权限按钮
        {
            //跳转至View2.
            MultiView1.ActiveViewIndex = 1;
            if (CheckUserPower(8))
            {
                RFGVPower(); 
            }
            PC_OnViewChange();//调用方法以检查用户权限，并执行相关操作。
            
        }

        protected void Bt_P2_ManUser_Click(object sender, EventArgs e)//管理用户按钮
        {
            //跳转至View3.
            MultiView1.ActiveViewIndex = 2;
            PC_OnViewChange();//调用方法以检查用户权限，并执行相关操作。
        }

        protected void Bt_P2_AddUser_Click(object sender, EventArgs e)//新增用户按钮
        {
            //跳转至View4.
            MultiView1.ActiveViewIndex = 3;
            PC_OnViewChange();//调用方法以检查用户权限，并执行相关操作。
        }
        protected void Bt_P2_ReturnLogin_Click(object sender, EventArgs e)
        {

            //返回登录页面
            Response.Write("<script language='javascript'>location.href='Login.aspx';</script>");
            Response.End();
        }

//-------------------------------------------------------------------------------页面跳转结束

//-------------------------------------------------------------------------------修改密码页面中的按钮开始
        protected void Button1_Click(object sender, EventArgs e)//确认按钮
        {
            if (TB_V1_OldPass.Text == "" || TB_V1_NewPass1.Text == "" || TB_V1_NewPass2.Text == "")//判断页面信息填写是否完整。
            {
                //若页面信息填写不完整，则提示用户继续填写。
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请将页面信息填写完整。');</script>");
            }
            else //若页面信息填写完整，则进入下一步判断。
            {
                if (TB_V1_NewPass1.Text .Equals(TB_V1_NewPass2.Text))//判断用户输入的新密码是否一致。若一致则继续，若不一致，则清空用户输入的新密码。
                {

                    //在Sql中获取此用户的密码（根据此用户的UserName查找）。
                    string UserPas = (bc.SelectSQLReturnObject("Select UserPassWord from SYL_UserInfo where UserName='" + Session["UserName"].ToString()+"'", "Syl_UserInfo")).ToString();

                    if (UserPas.Equals(TB_V1_OldPass.Text))//判断用户输入的旧密码和此用户数据库中的原密码是否一致。若一致，则开始修密码。
                    {
                        if (bc.ExecSQL("update Syl_UserInfo set UserPassWord='" + TB_V1_NewPass1.Text + "' where UserName='" + Session["UserName"].ToString() + "'"))//执行修改密码操作并判断是否执行成功
                        {
                            //清空文本框中的数据。
                            TB_V1_OldPass.Text = "";
                            TB_V1_NewPass1.Text = "";
                            TB_V1_NewPass2.Text = "";
                            //弹出提示修改密码成功（不刷新页面）。
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('密码修改成功');</script>");

                        }
                        else//若失败，清空文本框中的数据，并提示用户检查网络后重新尝试。
                        {
                            //清空文本框中的数据。
                            TB_V1_OldPass.Text = "";
                            TB_V1_NewPass1.Text = "";
                            TB_V1_NewPass2.Text = "";
                            //弹出提示修改密码失败（不刷新页面）。
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('密码修改失败，请检查网络后重新尝试。');</script>");
                        }
                    }
                    else//当用户输入密码与旧密码不一致时，弹出提示
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('旧密码输入错误，请重新输入。');</script>");
                    }
                }
                else//当用户输入的新密码不一致的时候，清空用户输入的新密码，并提示。
                {
                    //清空新密码。
                    TB_V1_NewPass1.Text = "";
                    TB_V1_NewPass2.Text = "";
                    //提示重新输入。
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('输入的新密码不一致，请重新输入。');</script>");
                }

            }
        }

        protected void Bt_P3_V1_Reset_Click(object sender, EventArgs e)//重置按钮
        {
            //将页面中的TextBox里的内容清空。
            TB_V1_OldPass.Text="";
            TB_V1_NewPass1.Text = "";
            TB_V1_NewPass2.Text = "";
        }
//-------------------------------------------------------------------------------修改密码页面中的按钮结束

//-------------------------------------------------------------------------------修改权限页面中的按钮开始
        protected void GV_V2_Power_RowDeleting(object sender, GridViewDeleteEventArgs e)//借用GridView的删除功能，在点击“修改权限”（删除）按钮时触发。
        {
            //Pancel4的Visibl属性等于CheckUserPower返回的值，当其结果为用户有此权限时显示Panel模块，
            Panel4.Visible = CheckUserPower(8);
            Lb_V2_ID.Text = GV_V2_Power.DataKeys[e.RowIndex].Value.ToString();
            Lb_V2_UserPower.Text = bc.SelectSQLReturnObject("SELECT UserPower FROM Syl_UserInfo WHERE ID=" + Lb_V2_ID.Text, "Syl_UserInfo").ToString();
            Lb_V2_ModulePower.Text = bc.SelectSQLReturnObject("SELECT ModulePower FROM Syl_UserInfo WHERE ID=" + Lb_V2_ID.Text, "Syl_UserInfo").ToString();

        }

        protected void Panel4_Load(object sender, EventArgs e)//Panel4在打开时触发
        {
            if (DDL_V2_QXZL.DataValueField == "")//利用DDL_V2_QXZL.DataValueField的值来判断是否首次激活此控件，若是，则为该控件（DropDawnList）赋值，并为CBL_V2_QXMX（CheckBoxList）赋相应的值。
            { 
                //为DDL_V2_QXZL（DropDawnList权限种类）添加相应数据
                string sql = "SELECT DISTINCT(KindName),Kind FROM Syl_UserPower ORDER BY Kind ";
                DDL_V2_QXZL.DataSource = bc.GetDataSet(sql, "Syl_UserPower");
                DDL_V2_QXZL.DataValueField = "Kind";
                DDL_V2_QXZL.DataTextField = "KindName";
                DDL_V2_QXZL.DataBind();


                //修改CBL_V2_QXMX(权限明细CheckBoxList)的内容
                sql = "SELECT PowerID,PowerName FROM Syl_UserPower WHERE Kind=" + DDL_V2_QXZL.SelectedValue + " ORDER BY PowerID";
                CBL_V2_QXMX.DataSource = bc.GetDataSet(sql, "Syl_Userpower");
                CBL_V2_QXMX.DataTextField = "PowerName";
                CBL_V2_QXMX.DataValueField = "PowerID";
                CBL_V2_QXMX.DataBind();


                //根据用户的权限为CBL_V2_QXMX（CheckBoxList权限明细）为相应的节点标记。
                int i,n;//运行中需要的整形变量 i为for循环中CBL_V2_QXMX的当前执行节点序号，n为权限值的相应节点位置。
                string a = "";//运行中需要的字符串变量，存储相应的权限内容。
                if (DDL_V2_QXZL.SelectedValue == "0")
                {   //当DDL_V2_QXZL选中的是进入权限时(Value==0)时，视为当前为进入权限，将a赋值为Lb_V2_ModulePower的Text值。
                    a = Lb_V2_ModulePower.Text;
                }
                else
                {   //当DDL_V2_QXZL选中的不是进入权限时（Value！=0），视为当前为其它功能权限，将a赋值为Lb_V2_UserPower。
                    a = Lb_V2_UserPower.Text;
                }

                for (i = 0; i < CBL_V2_QXMX.Items.Count; i++)
                {
                     n = Convert.ToInt32(CBL_V2_QXMX.Items[i].Value);
                     CBL_V2_QXMX.Items[i].Selected = a.Substring(n, 1) == "Y";
                }

            }

        }

        protected void DDL_V2_QXZL_SelectedIndexChanged(object sender, EventArgs e)//修改DDL_V2_QXZL（权限种类DropDawnList）时触发。
        {

            //修改CBL_V2_QXMX(权限明细CheckBoxList)的内容
            string sql = "SELECT PowerID,PowerName FROM Syl_UserPower WHERE Kind=" + DDL_V2_QXZL.SelectedValue + " ORDER BY PowerID";
            CBL_V2_QXMX.DataSource = bc.GetDataSet(sql, "Syl_Userpower");
            CBL_V2_QXMX.DataTextField = "PowerName";
            CBL_V2_QXMX.DataValueField = "PowerID";
            CBL_V2_QXMX.DataBind();


            //根据用户的权限为CBL_V2_QXMX（CheckBoxList权限明细）为相应的节点标记。
            int i, n;//运行中需要的整形变量 i为for循环中CBL_V2_QXMX的当前执行节点序号，n为权限值的相应节点位置。
            string a = "";//运行中需要的字符串变量，存储相应的权限内容。
            if (DDL_V2_QXZL.SelectedValue == "0")
            {   //当DDL_V2_QXZL选中的是进入权限时(Value==0)时，视为当前为进入权限，将a赋值为Lb_V2_ModulePower的Text值。
                a = Lb_V2_ModulePower.Text;
            }
            else
            {   //当DDL_V2_QXZL选中的不是进入权限时（Value！=0），视为当前为其它功能权限，将a赋值为Lb_V2_UserPower。
                a = Lb_V2_UserPower.Text;
            }

            for (i = 0; i < CBL_V2_QXMX.Items.Count; i++)
            {
                n = Convert.ToInt32(CBL_V2_QXMX.Items[i].Value);
                CBL_V2_QXMX.Items[i].Selected = a.Substring(n, 1) == "Y";
            }
        }


//-------------------------------------------------------------------------------修改权限页面中的按钮结束

//=================================================================================================按钮点击事件结束
    }
}