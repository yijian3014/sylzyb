<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="USERINFO.aspx.cs" Inherits="sylzyb_employer_mgr.USER" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            	<style type="text/css">
		body{background:#eae9e9;text-align:center;}
		div{margin:0 auto;background:#fff;text-align:left;}
                
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 481px; width: 900px">
    
        <asp:Panel ID="Panel1" runat="server" Height="57px" HorizontalAlign="Center">
            <br />
            <asp:Label ID="Lb_P1_Title" runat="server" Text="用户管理" Font-Size="XX-Large"></asp:Label>
        </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" >
                <asp:Button ID="Bt_P2_ChangPassWord" runat="server" Text="修改密码" OnClick="Bt_P2_ChangPassWord_Click" />
                <asp:Button ID="Bt_P2_ChangePower" runat="server" Text="修改权限" OnClick="Bt_P2_ChangePower_Click" />
                <asp:Button ID="Bt_P2_ManUser" runat="server" Text="管理用户" OnClick="Bt_P2_ManUser_Click" />
                <asp:Button ID="Bt_P2_AddUser" runat="server" Text="新增用户" OnClick="Bt_P2_AddUser_Click" />
        </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" >
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center">
                                <asp:Label ID="Label3" runat="server" Text="新密码"></asp:Label>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            3
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            4
                        </asp:View>
                    </asp:MultiView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
