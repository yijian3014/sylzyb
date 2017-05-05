<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_khzj.aspx.cs" Inherits="sylzyb_employer_mgr.rpt_khzj" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          
      <div id="dv_khzj">
        <div style="text-align:center;margin:0 auto;"  >
           <table style="text-align:center;width:950px;float:0;margin:auto;">
               <tr>
                   <td>
  <asp:TextBox ID="tbx_bg_date" runat="server" Enabled="False" Width="94px"></asp:TextBox> 
                   </td>
                    <td>
  <asp:Button ID="btn_bg_date" runat="server" Text="(发生)开始时间" OnClick="btn_bg_date_Click" Width="99px" />
                   </td>
                    <td>
  <asp:TextBox ID="tbx_ed_date" runat="server" Enabled="False" Width="94px"></asp:TextBox> 
                   </td>
                    <td>
<asp:Button ID="btn_ed_time" runat="server" Text="(发生)结束时间" OnClick="btn_ed_time_Click" Width="99px" />
                   </td>
                    <td>
  <asp:Label ID="Label5" runat="server" Text="级别：" ></asp:Label>
                   </td>
                   <td>
                       <asp:DropDownList ID="ddl_lcjb" runat="server" Height="16px" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="ddl_lcjb_SelectedIndexChanged">
                           <asp:ListItem>全部</asp:ListItem>
                           <asp:ListItem>厂部考核</asp:ListItem>
                           <asp:ListItem>作业部考核</asp:ListItem>
                           <asp:ListItem>班组考核</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td>
                       <asp:Label ID="Label6" runat="server" Text="类别："></asp:Label>
                   </td>
                   <td>
                       <asp:DropDownList ID="ddl_lclb" runat="server" Height="16px" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="ddl_lclb_SelectedIndexChanged">
                           <asp:ListItem>全部</asp:ListItem>
                           <asp:ListItem>日常考核</asp:ListItem>
                           <asp:ListItem>事故通报</asp:ListItem>
                           <asp:ListItem>自主改善</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                    <td>
   <asp:Label ID="Label7" runat="server" Text="状态："></asp:Label>
                   </td>
                    <td>
 
            <asp:DropDownList ID="ddl_lczt" runat="server" Height="16px" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="ddl_lczt_SelectedIndexChanged">
                <asp:ListItem>全部</asp:ListItem>
                <asp:ListItem>生效</asp:ListItem>
                <asp:ListItem>未生效</asp:ListItem>             
            </asp:DropDownList>
                   </td>
                   
                         <td>
   <asp:Button ID="btn_cx" runat="server" OnClick="btn_cx_Click" Text="查询" Width="50px" />
                   </td>
                   <td>
                         <asp:Button ID="btn_exit" runat="server" Text="退出" OnClick="btn_exit_Click" />
                   </td>
               </tr>
           </table>
          
            </div> 
        <div style="text-align:left;margin:0 auto;width:950px;float:none; height:50%;">
        <table style="text-align:left;margin:0 auto;float:none;width:30%">
            <tr>
                <td >
 <asp:Panel ID="pnl_bg_date" runat="server" Visible="False"  style="Z-INDEX: 140;width:269px;text-align:center;margin:0; POSITION: absolute; TOP:35px" HorizontalAlign="Center">
<asp:Calendar ID="cld_bg_date" runat="server" BackColor="White"  OnSelectionChanged="Button4_Click"></asp:Calendar>
             <asp:Button ID="Button4" runat="server" Text="确定" OnClick="Button4_Click" Visible="False" />
       </asp:Panel>  
                </td>
            </tr>
                  <tr>
                <td   >
   <asp:Panel ID="pnl_ed_date" runat="server" Visible="False" style="Z-INDEX: 141;width:269px;text-align:center;margin:0; POSITION: absolute; TOP: 45px" HorizontalAlign="Center">
  <asp:Calendar ID="cld_ed_date" runat="server" Visible="True" BackColor="White" OnSelectionChanged="Button3_Click"></asp:Calendar>
                <asp:Button ID="Button3" runat="server" Text="确定" OnClick="Button3_Click" Visible="False" />
            </asp:Panel>
                  
                </td>
            </tr>
            
        </table>
  </div>
       

    <div style="text-align:center;margin:0 auto;width:950px;float:none; height:760px;overflow:auto;">
        

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="12pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="611px" ShowBackButton="False" ShowFindControls="False" >
                     
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dzswConnectionString %>" 
            SelectCommand="SELECT * FROM [dzsw].[dbo].[Syl_AppWorkerinfo]" OnSelecting="SqlDataSource1_Selecting">
        </asp:SqlDataSource>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
          </div> 
    </form>
</body>
</html>
