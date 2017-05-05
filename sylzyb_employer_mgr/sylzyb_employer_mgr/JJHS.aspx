<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JJHS.aspx.cs" Inherits="sylzyb_employer_mgr.JJHS" %>
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
    <div style="width: 900px; height: 767px;">
    
        <asp:Panel ID="Panel1" runat="server" Height="68px" HorizontalAlign="Center">
            <br />
            <asp:Label ID="Label1" runat="server" Text="奖金核算" Font-Size="XX-Large"></asp:Label>
        </asp:Panel>
        <asp:Button ID="Bt_Select" runat="server" Height="21px" Text="奖金查询" Width="90px" OnClick="Bt_Select_Click" />
        <asp:Button ID="Bt_Input" runat="server" Height="21px" Text="奖金录入" Width="90px" OnClick="Bt_Input_Click" />
        <asp:Button ID="Bt_Return" runat="server" Height="21px" Text="返回首页" Width="90px" OnClick="Bt_Return_Click" />
        <br />
<br />
    <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                    <asp:Label ID="Label2" runat="server" Text="年份:"></asp:Label><asp:DropDownList ID="DDL_V1_Year" runat="server" Height="16px">
                    </asp:DropDownList>
                    <asp:Label ID="Label14" runat="server" ForeColor="White" Text="大间隔"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="月份："></asp:Label>
                    <asp:DropDownList ID="DDL_V1_Month" runat="server">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label15" runat="server" ForeColor="White" Text="大间隔"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="类别："></asp:Label><asp:DropDownList ID="DDL_V1_Kind" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_V1_Kind_SelectedIndexChanged">
                        <asp:ListItem>班组</asp:ListItem><asp:ListItem>个人</asp:ListItem>
                        <asp:ListItem>总奖</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label16" runat="server" ForeColor="White" Text="大间隔"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text="班别：" Visible="False"></asp:Label>
                    <asp:DropDownList ID="DDL_V1_Group" runat="server" Visible="False">
                        <asp:ListItem>甲班</asp:ListItem>
                        <asp:ListItem>乙班</asp:ListItem>
                        <asp:ListItem>丙班</asp:ListItem>
                        <asp:ListItem>丁班</asp:ListItem>
                        <asp:ListItem>综合组</asp:ListItem>
                        <asp:ListItem>铸铁组</asp:ListItem>
                        <asp:ListItem>污泥组</asp:ListItem>
                        <asp:ListItem>机关</asp:ListItem>
                        <asp:ListItem>车间领导</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="Bt_V1_Select" runat="server" Text="查询" Width="75px" OnClick="Bt_V1_Select_Click" />
                </asp:Panel>
                <br />
                <asp:GridView ID="GV_V1_Group" runat="server" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="G_BonusDate" HeaderText="奖金月份" />
                        <asp:BoundField DataField="G_GroupName" HeaderText="班组" />
                        <asp:BoundField DataField="G_Coefficient" HeaderText="系数" />
                        <asp:BoundField DataField="G_BaseBonus" HeaderText="分值" />
                        <asp:BoundField DataField="G_DueBonus" HeaderText="应得奖金" />
                        <asp:BoundField DataField="G_PlantApp" HeaderText="厂部考核" />
                        <asp:BoundField DataField="G_DepartmentApp" HeaderText="部门考核" />
                        <asp:BoundField DataField="G_ActualBonus" HeaderText="实际奖金" />
                        <asp:BoundField DataField="AverageBonus" HeaderText="平均奖" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <asp:GridView ID="GV_V1_Person" runat="server" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" Font-Size="11pt" ForeColor="#333333" HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="P_BonusDate" HeaderText="奖金月份" />
                        <asp:BoundField DataField="WorkerName" HeaderText="员工姓名" />
                        <asp:BoundField DataField="P_Coefficient" HeaderText="系数" />
                        <asp:BoundField DataField="P_BaseBonus" HeaderText="分值" />
                        <asp:BoundField DataField="P_DueBonus" HeaderText="应得奖金" />
                        <asp:BoundField DataField="P_PlantApp" HeaderText="厂部考核" />
                        <asp:BoundField DataField="P_DepartmentApp" HeaderText="部门考核" />
                        <asp:BoundField DataField="P_GroupApp" HeaderText="班组考核" />
                        <asp:BoundField DataField="P_Other1" HeaderText="全勤奖" />
                        <asp:BoundField DataField="P_Other2" HeaderText="自主改善" />
                        <asp:BoundField DataField="DutyBonus" HeaderText="管理奖" />
                        <asp:BoundField DataField="P_ActualBonus" HeaderText="实际奖金" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <asp:GridView ID="GV_V1_Base" runat="server" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="BonusDate" HeaderText="奖金月份" />
                        <asp:BoundField DataField="BonusKind" HeaderText="奖金种类" />
                        <asp:BoundField DataField="BonusMoney" HeaderText="奖金金额" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <br />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center">
                    <asp:Label ID="Label6" runat="server" Text="年份:"></asp:Label>
                    <asp:DropDownList ID="DDL_V2_Year" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="很大间隔"></asp:Label>
                    <asp:Label ID="Label7" runat="server" Text="月份："></asp:Label>
                    <asp:DropDownList ID="DDL_V2_Month" runat="server">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="厂发总奖："></asp:Label>
                    <asp:Label ID="Label11" runat="server" ForeColor="White" Text="间隔"></asp:Label>
                    <asp:TextBox ID="TB_V2_CFZJ" runat="server" MaxLength="10"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="车间领导奖金："></asp:Label>
                    <asp:TextBox ID="TB_V2_LDJJ" runat="server" MaxLength="10"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="管理奖："></asp:Label>
                    <asp:Label ID="Label12" runat="server" ForeColor="White" Text="大间隔"></asp:Label>
                    <asp:TextBox ID="TB_V2_GLJ" runat="server" MaxLength="10"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label17" runat="server" ForeColor="White" Text="true"></asp:Label>
                    <br />
                    <asp:Button ID="Bt_V2_Insert" runat="server" OnClick="Bt_V2_Insert_Click" Text="检查" Width="75px" />
                </asp:Panel>
            </asp:View>
            <asp:View ID="View3" runat="server">
                3</asp:View><asp:View ID="View4" runat="server">
                4</asp:View></asp:MultiView></div></form></body></html>