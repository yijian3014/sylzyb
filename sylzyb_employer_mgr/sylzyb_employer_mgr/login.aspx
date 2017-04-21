<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="sylzyb_employer_mgr.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <style>
       .dv{
           width:980px;
           float:none;
           margin:auto;
           text-align:center;
       }
        .login_tb {
            width: 980px;
            text-align: center;
            height:300px
        }

        .lb_nm_mm {
            width: 25%;
            text-align: right;
        }

        .tbx_nm_mm {
            width: 25%;
            text-align: left;
        }

        .td_left {
            width: 25%;
        }

        .td_right {
            width: 25%;
        }
     
       .auto-style1 {
           text-align: left;
       }
     
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dv">
            <table class="login_tb">

                <tr>

                    <td colspan="4">
                        <asp:Label ID="Label26" runat="server" Text="原料作业部信息管理平台" Font-Bold="True" Font-Size="X-Large"  ></asp:Label> 
                </tr>
                <tr>
                 <td colspan="4">
                      
                     <asp:RadioButtonList ID="rbtl_mod_sel" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem Value="KHXXGL.ASPX" Selected="True">考核信息管理</asp:ListItem>
                         <asp:ListItem Value="EMPLOYER.aspx">员工信息管理</asp:ListItem>
                         
                         <asp:ListItem Value="USERINFO.ASPX">用户信息管理</asp:ListItem>
                         <asp:ListItem Value="JJHS.ASPX">奖金核算</asp:ListItem>
                         <asp:ListItem Value="Report.aspx">报表</asp:ListItem>
                     </asp:RadioButtonList>
                 </td>
                </tr>
                <tr>
                    <td class="td_left"></td>
                    <td class="lb_nm_mm">
                        <asp:Label ID="Label1" runat="server" Text="登陆名："></asp:Label>
                    </td>
                    <td class="tbx_nm_mm">
                        <asp:TextBox ID="tbx_lg_nm" runat="server"></asp:TextBox>
                    </td>
                    <td class="td_right"></td>
                </tr>
                <tr>
                    <td class="td_left"></td>
                    <td class="lb_nm_mm">
                        <asp:Label ID="Label2" runat="server" Text="密码："></asp:Label>
                    </td>
                    <td class="tbx_nm_mm">
                        <asp:TextBox ID="tbx_lg_pas" runat="server"></asp:TextBox>
                    </td>
                    <td class="td_left"></td>

                </tr>
                <tr>

                    <td colspan="4">
                        <asp:Button ID="btn_login" runat="server" Text="登陆" OnClick="btn_login_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;" colspan="4">
                        <asp:Label ID="Label3" runat="server" Text="平台所有操作生成的数据属测试数据，未正式生效！" ForeColor="Red" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
