<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="sylzyb_employer_mgr.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <style>
        .login_tb {
            width: 980px;
            text-align: center;
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
           width: 25%;
           height: 22px;
       }
       .auto-style2 {
           width: 25%;
           text-align: right;
           height: 22px;
       }
       .auto-style3 {
           width: 25%;
           text-align: left;
           height: 22px;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="login_tb">

                <tr>

                    <td colspan="4">
                        <asp:Label ID="Label3" runat="server" Text="员工管理" Font-Size="X-Large"></asp:Label></td>
                </tr>
                <tr>
                 <td colspan="4">
                      
                     <asp:RadioButtonList ID="rbtl_mod_sel" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem>员工信息管理</asp:ListItem>
                         <asp:ListItem>考核信息管理</asp:ListItem>
                         <asp:ListItem>用户信息管理</asp:ListItem>
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
                    <td class="auto-style1"></td>
                    <td class="auto-style2">
                        <asp:Label ID="Label2" runat="server" Text="密码："></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tbx_lg_pas" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1"></td>

                </tr>
                <tr>

                    <td colspan="4">
                        <asp:Button ID="btn_login" runat="server" Text="登陆" OnClick="btn_login_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
