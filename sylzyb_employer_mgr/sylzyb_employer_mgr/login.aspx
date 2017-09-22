<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="sylzyb_employer_mgr.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Expires" content="0"/> 
<meta http-equiv="Cache-Control" content="no-cache"/> 
<meta http-equiv="Pragma" content="no-cache"/> 
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
    <script type="text/javascript">
        window.history.forward(1);
    </script>
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
                        <asp:Label ID="Label1" runat="server" Text="登录名："></asp:Label>
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
                        <asp:TextBox ID="tbx_lg_pas" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="td_left"></td>

                </tr>
                <tr>

                    <td colspan="4">
                        <asp:Button ID="btn_login" runat="server" Text="登陆" OnClick="btn_login_Click" />
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                    </td>
                </tr>
             
            </table>
        </div>
    </form>
</body>
</html>
