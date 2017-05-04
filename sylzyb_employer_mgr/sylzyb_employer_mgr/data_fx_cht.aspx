<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="data_fx_cht.aspx.cs" Inherits="sylzyb_employer_mgr.data_fx_cht" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 980px;
            float: 0;
            text-align: center;
            margin: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div  class="auto-style1">
    <table  class="auto-style1">
        <tr>
            <td>
<asp:Label ID="Label3" runat="server" Text="分析种类："></asp:Label>
            </td>
            <td>
<asp:DropDownList ID="DropDownList4" runat="server">
    <asp:ListItem>历史趋势</asp:ListItem>
    <asp:ListItem>占比</asp:ListItem>
    <asp:ListItem>环比</asp:ListItem>
    <asp:ListItem>同比</asp:ListItem>
    <asp:ListItem>类比</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="时间段:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="开始："></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                <asp:Label ID="Label5" runat="server" Text="结束"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="参量："></asp:Label>
            </td>
           
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                </asp:CheckBoxList>
            </td>
             <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">值</asp:ListItem>
                    <asp:ListItem>比率</asp:ListItem>
                </asp:RadioButtonList>

            </td>
           <td>
               <asp:Button ID="btn_fx_ok" runat="server" Text="确定" OnClick="btn_fx_ok_Click" />
            </td>
            
        </tr>
    </table>
    </div>
        <div>
            <rsweb:ReportViewer ID="rv_data_fx_cht" runat="server" Font-Names="Verdana" Font-Size="12pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="500px" ShowBackButton="False" ShowFindControls="False" >
           
        </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
