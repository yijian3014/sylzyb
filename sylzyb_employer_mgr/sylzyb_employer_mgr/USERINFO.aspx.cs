using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sylzyb_employer_mgr
{
    public partial class USER : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //=================================================================================================按钮点击事件开始
        //-------------------------------------------------------------------------------页面跳转按钮
        protected void Bt_P2_ChangPassWord_Click(object sender, EventArgs e)//修改密码按钮
        {
            //跳转至View1.
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Bt_P2_ChangePower_Click(object sender, EventArgs e)//修改用户权限按钮
        {
            //跳转至View2.
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Bt_P2_ManUser_Click(object sender, EventArgs e)//管理用户按钮
        {
            //跳转至View3.
            MultiView1.ActiveViewIndex = 2;
        }

        protected void Bt_P2_AddUser_Click(object sender, EventArgs e)//新增用户按钮
        {
            //跳转至View4.
            MultiView1.ActiveViewIndex = 3;
        }


        //=================================================================================================按钮点击事件结束
    }
}