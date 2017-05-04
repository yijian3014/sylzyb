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
    <div style="height: 1723px; width: 900px">
    
        <asp:Panel ID="Panel1" runat="server" Height="57px" HorizontalAlign="Center">
            <br />
            <asp:Label ID="Lb_P1_Title" runat="server" Text="用户管理" Font-Size="XX-Large"></asp:Label>
        </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" >
                <br />
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
                            <asp:GridView ID="GV_V2_Power" runat="server" AutoGenerateColumns="False" BorderStyle="None" CellPadding="3" EnableModelValidation="True" HorizontalAlign="Center" Width="647px" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" OnRowDeleting="GV_V2_Power_RowDeleting" OnPageIndexChanging="GV_V2_Power_PageIndexChanging">
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
                            <br />
                            <asp:Panel ID="Panel4" runat="server" OnLoad="Panel4_Load" Visible="False">
                                <asp:Label ID="Label10" runat="server" Text="员工姓名："></asp:Label>
                                <asp:Label ID="Lb_V2_RealName" runat="server" Text="Label"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="Label8" runat="server" Text="权限种类："></asp:Label>
                                <asp:DropDownList ID="DDL_V2_QXZL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_V2_QXZL_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:Label ID="Lb_V2_ID" runat="server" Text="ID" ForeColor="White"></asp:Label>
                                <asp:Label ID="Lb_V2_UserPower" runat="server" Text="用户权限" ForeColor="White"></asp:Label>
                                <asp:Label ID="Lb_V2_ModulePower" runat="server" Text="进入权限" ForeColor="White"></asp:Label>
                                <br />
                                <asp:Label ID="Label9" runat="server" Text="权限明细："></asp:Label>
                                <asp:CheckBoxList ID="CBL_V2_QXMX" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                <br />
                                <asp:Button ID="Bt_V2_Revise" runat="server" Text="修改" OnClick="Bt_V2_Revise_Click" Width="100px" />
                                
                                &nbsp;&nbsp;
                                
                                <asp:Button ID="Bt_V2_Cancel" runat="server" Text="取消" OnClick="Bt_V2_Cancel_Click" Width="100px" />
                            </asp:Panel>
                            <br />
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <br />
                            <asp:GridView ID="GV_V3_ManUser" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" HorizontalAlign="Center" OnRowDeleting="GV_V3_ManUser_RowDeleting" Width="647px" OnPageIndexChanging="GV_V3_ManUser_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="RealName" HeaderText="姓名" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserLevelName" HeaderText="职务" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IDCard" HeaderStyle-Width="200" HeaderText="身份证号" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="登录名" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:CommandField DeleteText="修改权限" ItemStyle-HorizontalAlign="Center" ShowDeleteButton="True">
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
                            <asp:Panel ID="Panel5" runat="server" HorizontalAlign="Center" Visible="False">
                                <asp:Label ID="Label11" runat="server" Text="姓名："></asp:Label>
                                <asp:TextBox ID="TB_V3_RealName" runat="server" Width="119px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:Label ID="Label12" runat="server" Text="职务："></asp:Label>
                                <asp:DropDownList ID="DDL_V3_LevelName" runat="server" Font-Size="Medium">
                                    <asp:ListItem Value="-1">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="1">部长</asp:ListItem>
                                    <asp:ListItem Value="2">书记</asp:ListItem>
                                    <asp:ListItem Value="3">主管领导</asp:ListItem>
                                    <asp:ListItem Value="4">工程师</asp:ListItem>
                                    <asp:ListItem Value="5">点检组长</asp:ListItem>
                                    <asp:ListItem Value="6">点检</asp:ListItem>
                                    <asp:ListItem Value="7">安全员</asp:ListItem>
                                    <asp:ListItem Value="8">办事员</asp:ListItem>
                                    <asp:ListItem Value="9">其它</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:Label ID="Label13" runat="server" Text="身份证号：" Font-Size="Medium"></asp:Label>
                                <asp:TextBox ID="TB_V3_IDCard" runat="server" Width="206px" MaxLength="18"></asp:TextBox>
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG12" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来"></asp:Label>
                                <asp:Label ID="Lb_P3_V1_DJG11" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大~"></asp:Label>
                                <asp:Label ID="Label22" runat="server" Font-Size="Small" ForeColor="#666666" Text="若身份证号最后一位为“X”，必须输入大写的“X”"></asp:Label>
                                <br />
                                <asp:Label ID="Lb_V3_ID" runat="server" Text="ID" ForeColor="White"></asp:Label>
                                <br />
                                <asp:Button ID="Bt_V3_Update" runat="server" style="margin-bottom: 0px" Text="修改" Width="100px" OnClick="Bt_V3_Update_Click" />
                                &nbsp;
                                <asp:Button ID="Bt_V3_ClearPas" runat="server" style="margin-bottom: 0px" Text="清空密码" Width="100px" OnClick="Bt_V3_ClearPas_Click" />
                                &nbsp;
                                <asp:Button ID="Bt_V3_DeleUser" runat="server" style="margin-bottom: 0px" Text="删除用户" Width="100px" OnClick="Bt_V3_DeleUser_Click" />
                                &nbsp;
                                <asp:Button ID="Bt_V3_Cancel" runat="server" OnClick="Bt_V3_Cancel_Click" style="margin-bottom: 0px" Text="取消" Width="100px" />
                            </asp:Panel>
                            <br />
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <br />
                            <asp:Panel ID="Panel6" runat="server">
                                <asp:Label ID="Lb_P3_V1_DJG5" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Label18" runat="server" Text="姓名："></asp:Label>
                                <asp:Label ID="Lb_P3_V1_XJG7" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:TextBox ID="TB_V4_RealName" runat="server" Height="19px" Width="119px"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG6" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Label19" runat="server" Text="职务："></asp:Label>
                                <asp:Label ID="Lb_P3_V1_XJG8" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                                <asp:DropDownList ID="DDL_V4_LevelName" runat="server" Font-Size="Medium">
                                    <asp:ListItem Value="-1">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="1">部长</asp:ListItem>
                                    <asp:ListItem Value="2">书记</asp:ListItem>
                                    <asp:ListItem Value="3">主管领导</asp:ListItem>
                                    <asp:ListItem Value="4">工程师</asp:ListItem>
                                    <asp:ListItem Value="5">点检组长</asp:ListItem>
                                    <asp:ListItem Value="6">点检</asp:ListItem>
                                    <asp:ListItem Value="7">安全员</asp:ListItem>
                                    <asp:ListItem Value="8">办事员</asp:ListItem>
                                    <asp:ListItem Value="9">其它</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG8" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Label21" runat="server" Text="登录名："></asp:Label>
                                <asp:Label ID="Lb_P3_V1_XJG9" runat="server" ForeColor="White" Text="隔"></asp:Label>
                                <asp:TextBox ID="TB_V4_UserName" runat="server" Width="119px"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG7" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Label20" runat="server" Font-Size="Medium" Text="身份证号："></asp:Label>
                                <asp:TextBox ID="TB_V4_IDCard" runat="server" MaxLength="18" Width="206px"></asp:TextBox>
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG10" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Label ID="Label23" runat="server" Font-Size="Small" ForeColor="#666666" Text="若身份证号最后一位为“X”，必须输入大写的“X”"></asp:Label>
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="Lb_P3_V1_DJG9" runat="server" ForeColor="White" Text="我是一个大大的间隔还是有点不够大再来点啊"></asp:Label>
                                <asp:Button ID="Bt_V4_UserAdd" runat="server" OnClick="Bt_V4_UserAdd_Click" style="margin-bottom: 0px" Text="添加" Width="100px" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Bt_V4_Clear" runat="server" OnClick="Bt_V4_Cancel_Clear" style="margin-bottom: 0px" Text="重置" Width="100px" />
                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
