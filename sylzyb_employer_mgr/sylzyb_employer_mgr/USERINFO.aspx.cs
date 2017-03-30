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
///---------------------------------------------------------------------------------------------------------------------------------------
///=======================================================================================================================================
///                                                                 程序说明
///     此页面为原料车间用户管理页面，设计的主要目的为管理原料车间信息管理平台的用户的的相关信息。其中包括修改密码、修改权限、管理用户、新
///增用户四大模块。
///     此模块涉及的权限为两种：
///     （1）进入权限（ModulePower）。拥有此权限的用户可以进入页面，但若只有此权限，而没有用户信息管理权限，则只能修改密码，不能对用户进行
///          管理。
///     （2）用户信息管理权限（UserPower）。拥有此权限的用户可以管理员工信息，执行修改权限、管理用户、新增用户三种操作。
///     使用的数据表为dzsw中的Syl_UserInfo表。此表包含ID（自增主键）、RealName（用户姓名）、IDCard（身份证号 PS：身份证号不能存在重复）、
///UserName（用户登录名 PS：登录名不能重复）、UserPassWord（登录密码 PS：新增用户默认登录密码为1）、UserLevel（用户级别（职务）编号 PS：0为
///系统管理员，1-8为从部长至其他依次递减）、UserLevelName（级别（职务）名称）、UserPower（用户权限代码 PS：因第一位在程序计算中为0位，所以没
///有使用）、ModulePower（用户进入页面的权限代码 PS：同样从第二位开始使用）。
///     连接数据库的方法在ConnerSql.cs类中。在页面开始处声明BaseClass bc调用数据库连接方法。
///     代码的排列顺序为：
///     （1）页面初始化时所执行的代买；
///     （2）页面中调用的方法；
///     （3）点击跳转至相应页面按钮时执行的方法；
///     （4）在各页面中点击按钮时触发的方法。
///     程序具体运转方式在页面中均有详细注释。若对程序进行修改，一定要编写详细注释，必要时要在程序说明中写明。
///     
///                                                                                                                     2017年3月28日
///                                                                                                                         崔鹏宇
///=======================================================================================================================================
///---------------------------------------------------------------------------------------------------------------------------------------
namespace sylzyb_employer_mgr
{
    public partial class USER : System.Web.UI.Page
    {
        BaseClass bc = new BaseClass();//调用数据库连接方法。
        protected void Page_Load(object sender, EventArgs e)
        {
            //根据Session值验证用户是否登录
            if (Session["RealName"] == "" || Session["RealName"] == null || Session["IDCard"] == null || Session["IDCard"] == null || Session["UserName"] == null || Session["UserLevel"] == null || Session["UserLevelName"] == null || Session["UserPower"] == null || Session["ModulePower"] == null)
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
        public void RfCBL_QXMX(string QXZL)//刷新CBL_QXMX（CheckBoxList权限明细）中的权限明细信息
        {
            //修改CBL_V2_QXMX(权限明细CheckBoxList)的内容
            string sql = "SELECT PowerID,PowerName FROM Syl_UserPower WHERE Kind=" + QXZL + " ORDER BY PowerID";
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

//-------------------------------------------------------------------------------修改用户权限中的方法结束

//-------------------------------------------------------------------------------管理用户界面中的方法开始
        public void RfGV_ManUser()//刷新管理用户界面中的表格
        {
            GV_V3_ManUser.DataSource = bc.GetDataSet("SELECT * FROM Syl_UserInfo WHERE UserLevel>0 ORDER BY UserLevel","Syl_UserInfo");
            GV_V3_ManUser.DataKeyNames = new string[] {"ID"};
            GV_V3_ManUser.DataBind();
        }
//-------------------------------------------------------------------------------管理用户界面中的方法结束

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
            RfGV_ManUser();
        }

        protected void Bt_P2_AddUser_Click(object sender, EventArgs e)//新增用户按钮
        {
            //跳转至View4.
            MultiView1.ActiveViewIndex = 3;
            PC_OnViewChange();//调用方法以检查用户权限，并执行相关操作。
            RfGV_ManUser();
            GV_V3_ManUser.Visible = true;
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
            if (1 == 1)//TB_V1_OldPass.Text == "" || TB_V1_NewPass1.Text == "" || TB_V1_NewPass2.Text == "") //若页面信息填写完整，则进入下一步判断,因可能出现空密码，所以取消。
            {
                if (TB_V1_NewPass1.Text.Equals(TB_V1_NewPass2.Text))//判断用户输入的新密码是否一致。若一致则继续，若不一致，则清空用户输入的新密码。
                {

                    //在Sql中获取此用户的密码（根据此用户的UserName查找）。
                    string UserPas = (bc.SelectSQLReturnObject("Select UserPassWord from SYL_UserInfo where UserName='" + Session["UserName"].ToString() + "'", "Syl_UserInfo")).ToString();

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
                //若页面信息填写不完整，则提示用户继续填写。

            }
            else
            {
                //页面。
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请将页面信息填写完整。');</script>");
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
        protected void GV_V2_Power_PageIndexChanging(object sender, GridViewPageEventArgs e)//GV_V2_Power翻页
        {
            GV_V2_Power.PageIndex = e.NewPageIndex;
            RFGVPower();
        }
        protected void GV_V2_Power_RowDeleting(object sender, GridViewDeleteEventArgs e)//借用GridView的删除功能，在点击“修改权限”（删除）按钮时触发。
        {

            Panel4.Visible = CheckUserPower(8);//Pancel4的Visibl属性等于CheckUserPower返回的值，当其结果为用户有此权限时显示Panel模块。

            Lb_V2_ID.Text = GV_V2_Power.DataKeys[e.RowIndex].Value.ToString();//将选中行的ID主键赋值给Label标签，留待使用。

            Lb_V2_RealName.Text = bc.SelectSQLReturnObject("SELECT RealName FROM Syl_UserInfo WHERE ID=" + Lb_V2_ID.Text, "Syl_UserInfo").ToString();//将选中行的用户姓名赋值给Label标签。

            Lb_V2_UserPower.Text = bc.SelectSQLReturnObject("SELECT UserPower FROM Syl_UserInfo WHERE ID=" + Lb_V2_ID.Text, "Syl_UserInfo").ToString();//将选中行的用户权限赋值给Label标签，留待使用。

            Lb_V2_ModulePower.Text = bc.SelectSQLReturnObject("SELECT ModulePower FROM Syl_UserInfo WHERE ID=" + Lb_V2_ID.Text, "Syl_UserInfo").ToString();//将选中行的模块权限赋值给Label标签，留待使用。

                                                  //为DDL_V2_QXZL（DropDawnList权限种类）添加相应数据
            string sql = "SELECT DISTINCT(KindName),Kind FROM Syl_UserPower ORDER BY Kind ";
            DDL_V2_QXZL.DataSource = bc.GetDataSet(sql, "Syl_UserPower");
            DDL_V2_QXZL.DataValueField = "Kind";
            DDL_V2_QXZL.DataTextField = "KindName";
            DDL_V2_QXZL.DataBind();
            //DDL_V2_QXZL.Items.Insert(0, new ListItem("所有权限", "-1"));//直接显示所有权限，因涉及改动较大所以暂时搁置。


            RfCBL_QXMX(DDL_V2_QXZL.SelectedValue);//根据选定用户的权限值刷新CBL_V2_QXMX（CheckBoxList权限明细）

        }

        protected void Panel4_Load(object sender, EventArgs e)//Panel4在打开时触发
        {
            if (DDL_V2_QXZL.DataValueField == "")//利用DDL_V2_QXZL.DataValueField的值来判断是否首次激活此控件，若是，则为该控件（DropDawnList）赋值，并为CBL_V2_QXMX（CheckBoxList）赋相应的值。
            { /*
                //为DDL_V2_QXZL（DropDawnList权限种类）添加相应数据
                string sql = "SELECT DISTINCT(KindName),Kind FROM Syl_UserPower ORDER BY Kind ";
                DDL_V2_QXZL.DataSource = bc.GetDataSet(sql, "Syl_UserPower");
                DDL_V2_QXZL.DataValueField = "Kind";
                DDL_V2_QXZL.DataTextField = "KindName";
                DDL_V2_QXZL.DataBind();
                //DDL_V2_QXZL.Items.Insert(0, new ListItem("所有权限", "-1"));//直接显示所有权限，因涉及改动较大所以暂时搁置。
                

                RfCBL_QXMX(DDL_V2_QXZL.SelectedValue);//根据选定用户的权限值刷新CBL_V2_QXMX（CheckBoxList权限明细）
               */
            }

        }

        protected void DDL_V2_QXZL_SelectedIndexChanged(object sender, EventArgs e)//修改DDL_V2_QXZL（权限种类DropDawnList）时触发。
        {
            RfCBL_QXMX(DDL_V2_QXZL.SelectedValue);//根据选定用户的权限值刷新CBL_V2_QXMX（CheckBoxList权限明细）
        }

        protected void Bt_V2_Revise_Click(object sender, EventArgs e)//修改按钮
        {
            int i,n = 0;//定义运转程序所需临时变量，i为当前操作CBL_V2_QXMX节点序号，该节点对应权限的位置编号。

            string Power,PowerKind;//临时变量，Power为当前修改的权限编码，PowerKind为当前操作的权限种类（ModulePower或UserPower）

            if (DDL_V2_QXZL.SelectedValue == "0")
            {
                //当选择为进入权限时Power取值为当前Session["ModulerPower"]值。
                Power = Lb_V2_ModulePower.Text;
                
            }
            else
            {
                //当选择为其它权限时，Power取值为当前Session["UserPower"]值。
                Power = Lb_V2_UserPower.Text;

            }

            for (i = 0; i < CBL_V2_QXMX.Items.Count; i++)//遍历当前CBL_V2_QXMX根据用户勾选情况，为变量Power赋值。
            {
                n = Convert.ToInt32(CBL_V2_QXMX.Items[i].Value);//获得当前遍历节点的PowerID（权限位置）

                if (CBL_V2_QXMX.Items[i].Selected)
                {
                    //若当前节点被勾选，则把Power相应位置的值设置为"Y"。
                    Power = Power.Substring(0, n) + "Y" + Power.Substring(n + 1);
                }
                else
                {
                    //若当前节点未被勾选，则把Power相应位置的值设置为"N"。
                    Power = Power.Substring(0, n) + "N" + Power.Substring(n + 1);
                }
            }

            if (DDL_V2_QXZL.SelectedValue == "0")//根据用户选择的权限种类为PowerKind赋值
            {
                //若为进入权限，则将其赋值为"ModulePower"。
                PowerKind = "ModulePower";
            }
            else
            {
                //若为其它权限，则将其赋值为"UserPower"。
                PowerKind = "UserPower";
            }
            
            //生成相应的Update语句
            string sql = "UPDATE SYl_UserInfo SET "+PowerKind+"='" + Power + "'WHERE ID=" + Lb_V2_ID.Text;

            if (bc.ExecSQL(sql))
            {
                //若Update语句执行成功，则提示用户修改成功，并更新相应数据。
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('用户权限修改成功。');</script>");

                if (PowerKind == "ModulePower")//根据修改的权限种类不同，更新不同Label的Text值。
                {
                    Lb_V2_ModulePower.Text = Power;
                }
                else
                {
                    Lb_V2_UserPower.Text = Power;
                }

                RfCBL_QXMX(DDL_V2_QXZL.SelectedValue);

            }
            else
            {
                //若失败，则提示失败，请重新尝试。然后根据程序中记录的权限值，刷新相应数据。
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('修改失败，请重新尝试。');</script>");
                RfCBL_QXMX(DDL_V2_QXZL.SelectedValue);
            }
        }//修改按钮结束

        protected void Bt_V2_Cancel_Click(object sender, EventArgs e)//取消按钮
        {
            //点击取消按钮时，清空修改权限模块中的数据，并隐藏该模块。
            DDL_V2_QXZL.DataTextField = "";
            DDL_V2_QXZL.DataValueField = "";
            Lb_V2_ID.Text = "用户ID";
            Lb_V2_UserPower.Text = "用户权限";
            Lb_V2_ModulePower.Text = "模块权限";
            CBL_V2_QXMX.Items.Clear();
            Panel4.Visible = false;

        }



//-------------------------------------------------------------------------------修改权限页面中的按钮结束

//-------------------------------------------------------------------------------管理用户页面中的按钮开始
        protected void GV_V3_ManUser_PageIndexChanging(object sender, GridViewPageEventArgs e)//GV_V3ManUser翻页
        {
            GV_V3_ManUser.PageIndex = e.NewPageIndex;//不知道干什么的可能是创建新的索引
            RfGV_ManUser();//刷新表格。
        }
        protected void GV_V3_ManUser_RowDeleting(object sender, GridViewDeleteEventArgs e)//借用DDL_V3_ManUser（DropDawnList管理用户）表的删除功能选择需要管理的用户。
        {
            Panel5.Visible = true;//显示用户管理模块

            Lb_V3_ID.Text = GV_V3_ManUser.DataKeys[e.RowIndex].Value.ToString();//将选择用户的ID赋值给Lb_V3_ID（Label用户ID）标签待用。

            //根据选择用户的ID在数据库中查询用户实际姓名赋值给TB_V3_RealName（TextBox用户姓名）。
            TB_V3_RealName.Text = bc.SelectSQLReturnObject("SELECT RealName FROM Syl_UserInfo WHERE ID=" + Lb_V3_ID.Text, "Syl_UserInfo").ToString();

            //根据选择用户的ID在数据库中查询用户身份证号赋值给TB_V3_IDCard（TextBox用户身份证号）。
            TB_V3_IDCard.Text = bc.SelectSQLReturnObject("SELECT IDCard FROM Syl_UserInfo WHERE ID=" + Lb_V3_ID.Text, "Syl_UserInfo").ToString();

            //根据选择用的的ID在数据库中查询用户的级别（UserLevel），并将DDL_V3_LevelName（DropDawnList级别名称）改为相应值
            DDL_V3_LevelName.SelectedIndex = Convert.ToInt32(bc.SelectSQLReturnObject("SELECT UserLevel FROM Syl_UserInfo WHERE ID=" + Lb_V3_ID.Text, "Syl_UserInfo"));

        }

        protected void Bt_V3_Update_Click(object sender, EventArgs e)//当点击修改按钮时触发。
        {
            if (Convert.ToInt32(DDL_V3_LevelName.SelectedValue) > 0)//检查用户是否选择了正确的职务（不能选择第一项“-请选择-”）。
            {
                if (TB_V3_RealName.Text != "" && TB_V3_IDCard.Text != "")//判断用户是否输入了姓名及身份证号。
                {
                    if (TB_V3_IDCard.Text.Length == 18)//判断输入的身份证号是否为18位
                    {
                        if (Convert.ToInt32(bc.SelectSQLReturnObject("SELECT COUNT(ID) FROM Syl_UserInfo WHERE IDCard='" + TB_V3_IDCard.Text + "' AND ID!=" + Lb_V3_ID.Text, "Syl_UserInfo")) == 0)//判断修改后的身份证号是否与其它用户相同。
                        {
                            //定义sql变量，并根据用户所修改的信息将其赋值为UPDATE语句
                            string sql = "UPDATE Syl_UserInfo SET RealName='" + TB_V3_RealName.Text + "',UserLevel=" + DDL_V3_LevelName.SelectedValue + ",UserLevelName='" + DDL_V3_LevelName.SelectedItem + "',IDCard='" + TB_V3_IDCard.Text + "' WHERE ID=" + Lb_V3_ID.Text;

                            if (bc.ExecSQL(sql))//执行Sql语句并判断是否成功。
                            {
                                //若成功，刷新表格并提示。
                                RfGV_ManUser();//刷新GV_V3_ManUser表。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('修改成功。');</script>");
                            }
                            else
                            {
                                //若不成功，则提示，并刷新表格。
                                RfGV_ManUser();//刷新GV_V3_ManUser表。
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('修改失败，请重试。');</script>");
                            }
                        }
                        else
                        {
                            //当身份证号与其他用户相同时提示。
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('身份证号与其他用户重复，请检查。');</script>");

                        }
                    }
                    else
                    {
                        //当输入的身份证号位数不正确时提示
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('身份证号位数不正确，请检查。');</script>");

                    }
                }
                else
                {
                    //当姓名与身份证号为空时提示。
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('用户姓名与身份证号不得为空！');</script>");
                
                }
            }
            else
            {
                //当选择了错误的职务（选择了“-请选择-”）时提示。
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请选择用户职务。');</script>");

            }
        }

        protected void Bt_V3_ClearPas_Click(object sender, EventArgs e)//点击“清空密码”按钮时触发。
        {
            //清空所选用户的密码。
            if (bc.ExecSQL("UPDATE Syl_UserInfo SET UserPassWord='' WHERE ID=" + Lb_V3_ID.Text))//判断是否修改成功。
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('修改成功。');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('修改失败，请重试。');</script>");
            }
            
        }
        protected void Bt_V3_DeleUser_Click(object sender, EventArgs e)//删除用户时触发
        {
            if (bc.ExecSQL("DELETE FROM Syl_UserInfo where ID=" + Lb_V3_ID.Text))
            {
                RfGV_ManUser();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('删除成功。');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('修删除失败，请重试。');</script>");
            }
        }
        protected void Bt_V3_Cancel_Click(object sender, EventArgs e)//点击取消按钮触发
        {
            Panel5.Visible = false;
            TB_V3_RealName.Text = "";
            TB_V3_IDCard.Text = "";
        }



//-------------------------------------------------------------------------------管理用户页面中的按钮结束

//-------------------------------------------------------------------------------新增用户页面中的按钮开始
        protected void Bt_V4_UserAdd_Click(object sender, EventArgs e)
        {
            if (TB_V4_RealName.Text != "" && TB_V4_UserName.Text != "" && TB_V4_IDCard.Text != "")//判断页面信息是否填写完整。
            {
                if (Convert.ToInt32(DDL_V4_LevelName.SelectedValue) > 0)//判断是否选择了用户职务。
                {
                    if (TB_V4_IDCard.Text.Length == 18)//判断身份证号是否输入了18位。
                    {
                        if (Convert.ToInt32(bc.SelectSQLReturnObject("SELECT COUNT(UserName) FROM Syl_UserInfo WHERE UserName='" + TB_V4_UserName.Text+"'", "Syl_UserInfo")) == 0)//判断数据库中是否有与所添加用户相同的用户名。
                        {
                            if (Convert.ToInt32(bc.SelectSQLReturnObject("SELECT COUNT(IDCard) FROM Syl_UserInfo WHERE IDCard='" + TB_V4_IDCard.Text+"'", "Syl_UserInfo")) == 0)//判断数据库中是否有与所添加用户相同的身份证号。
                            {
                                //定义变量，并为期附全否的权限值，留待生成Sql语句。
                                string UserPower = "ANNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN";
                                string ModulePower = "NNNNNNNNNNN";
                                //定义变量，并根据页面输入信息将其赋值为相应的Sql插入语句
                                string sql = "INSERT INTO Syl_UserInfo(RealName,IDCard,UserName,UserPassWord,UserLevel,UserLevelName,UserPower,ModulePower)VALUES";
                                sql += "('" + TB_V4_RealName.Text + "','" + TB_V4_IDCard.Text + "','" + TB_V4_UserName.Text + "','1',"+DDL_V4_LevelName.SelectedValue+",'"+DDL_V4_LevelName.SelectedItem+"','"+UserPower+"','"+ModulePower+"')";
                                if (bc.ExecSQL(sql))//执行Sql语句，并判断是否执行成功。
                                {
                                    //若成功，则提示。
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('添加成功，初始密码为1。请移至权限管理页面，为此用户添加相应权限。');</script>");
                                }
                                else//Sql语句执行失败提示
                                {
                                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('添加失败，请重试。');</script>");
                                }
                            }
                            else//有重复的身份证号提示。
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('重复的身份证号，请检查后重新输入。');</script>");
                            }
                        }
                        else//有重复的用户名提示。
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('重复的用户名，请重新输入。');</script>");
                        }
                    }
                    else//身份证号输入位数不足提示。
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('身份证号位数不足，请检查。');</script>");
                    }
                }
                else//没有选择相应职务提示。
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请选择用户职务。');</script>");
                }
            }
            else//页面信息填写不完整提示。
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请将页面信息填写完整。');</script>");
            }
        }

        protected void Bt_V4_Cancel_Clear(object sender, EventArgs e)//点击重置按钮时触发。
        {

            //清空页面信息。
            TB_V4_RealName.Text = "";
            DDL_V4_LevelName.SelectedIndex = 0;
            TB_V4_UserName.Text = "";
            TB_V4_IDCard.Text = "";

        }
//-------------------------------------------------------------------------------新增用户页面中的按钮结束

        //=================================================================================================按钮点击事件结束
    }
}
///程序结束
///哦吼吼！！！