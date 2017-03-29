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
    <div style="height: 999px; width: 900px">
    
        <asp:Panel ID="Panel1" runat="server" Height="57px" HorizontalAlign="Center">
            <br />
            <asp:Label ID="Lb_P1_Title" runat="server" Text="用户管理" Font-Size="XX-Large"></asp:Label>
        </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" >
                <asp:Button ID="Bt_P2_ChangPassWord" runat="server" Text="修改密码" OnClick="Bt_P2_ChangPassWord_Click" />
                <asp:Button ID="Bt_P2_ChangePower" runat="server" Text="修改权限" OnClick="Bt_P2_ChangePower_Click" />
                <asp:Button ID="Bt_P2_ManUser" runat="server" Text="管理用户" OnClick="Bt_P2_ManUser_Click" />
                <asp:Button ID="Bt_P2_AddUser" runat="server" Text="新增用户" OnClick="Bt_P2_AddUser_Click" />
                <asp:Button ID="Bt_P2_ReturnLogin" runat="server" OnClick="Bt_P2_ReturnLogin_Click" Text="返回登录" />
        </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" >
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG1" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                            
                                <asp:Label ID="Label3" runat="server" Text="旧密码："></asp:Label>
                            
                                <asp:Label ID="Lb_P3_V1_XJG1" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:TextBox ID="TB_V1_OldPass" runat="server" TextMode="Password"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG2" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="新密码："></asp:Label>
                                <asp:Label ID="Lb_P3_V1_XJG2" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:TextBox ID="TB_V1_NewPass1" runat="server" TextMode="Password"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG3" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来"></asp:Label>
                                <asp:Label ID="Lb_P3_V1_XJG3" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:Label ID="Label7" runat="server" Text="确认新密码："></asp:Label>
                                <asp:TextBox ID="TB_V1_NewPass2" runat="server" TextMode="Password"></asp:TextBox>
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG4" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Lb_P3_V1_XJG6" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:Button ID="Bt_P3_V1_OK" runat="server" OnClick="Button1_Click" Text="确认" Width="79px" />
                                <asp:Label ID="Lb_P3_V1_XJG4" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:Button ID="Bt_P3_V1_Reset" runat="server" OnClick="Bt_P3_V1_Reset_Click" style="height: 21px" Text="重置" Width="79px" />
                                <asp:Label ID="Lb_P3_V1_XJG5" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                            
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <br />
                            <asp:GridView ID="GV_V2_Power" runat="server" AutoGenerateColumns="False" BorderStyle="None" CellPadding="3" EnableModelValidation="True" HorizontalAlign="Center" Width="647px" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" OnRowDeleting="GV_V2_Power_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="RealName" HeaderText="姓名"  ItemStyle-HorizontalAlign="Center"  >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserLevelName" HeaderText="职务" ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IDCard" HeaderText="身份证号" HeaderStyle-Width="200" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="登录名" ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:CommandField DeleteText="修改权限" ShowDeleteButton="True" ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            <br />
                            <asp:Panel ID="Panel4" runat="server" OnLoad="Panel4_Load">
                                <asp:Label ID="Label8" runat="server" Text="权限种类："></asp:Label>
                                <asp:DropDownList ID="DDL_V2_QXZL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_V2_QXZL_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="Lb_V2_ID" runat="server" Text="ID"></asp:Label>
                                <asp:Label ID="Lb_V2_UserPower" runat="server" Text="用户权限"></asp:Label>
                                <asp:Label ID="Lb_V2_ModulePower" runat="server" Text="进入权限"></asp:Label>
                                <br />
                                <asp:Label ID="Label9" runat="server" Text="权限明细："></asp:Label>
                                <asp:CheckBoxList ID="CBL_V2_QXMX" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </asp:Panel>
                            <br />
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
