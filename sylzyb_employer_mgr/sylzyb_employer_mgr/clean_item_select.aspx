<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clean_item_select.aspx.cs" Inherits="sylzyb_employer_mgr.clean_item_select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
    <link href="clean_item_select.css" rel="stylesheet" type="text/css" /> 
    <title></title>
</head>
 
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddl_select" runat="server">
                <asp:ListItem>自由选择</asp:ListItem>
                <asp:ListItem>全选</asp:ListItem>
                <asp:ListItem>返选</asp:ListItem>
                <asp:ListItem>清除选择</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBoxList ID="cbl_kh_items" runat="server">
            </asp:CheckBoxList>
            <asp:GridView ID="gv_ql_items" runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
