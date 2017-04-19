<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="sylzyb_employer_mgr.Report" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="width:1000px;text-align:center; float:none;margin:auto;" >
  <form id="form2" runat="server"  style="text-align:center;width:auto 1000px;margin:0 auto;height:auto 65%;" >
        <div style="text-align:center;margin:0 auto;"  >
           <table style="text-align:center;width:950px;float:0;margin:auto;">
               <tr>
                   <td>
  <asp:TextBox ID="tbx_bg_date" runat="server" Enabled="False" Width="94px"></asp:TextBox> 
                   </td>
                    <td>
  <asp:Button ID="btn_bg_date" runat="server" Text="开始时间" OnClick="btn_bg_date_Click" />
                   </td>
                    <td>
  <asp:TextBox ID="tbx_ed_date" runat="server" Enabled="False" Width="94px"></asp:TextBox> 
                   </td>
                    <td>
<asp:Button ID="btn_ed_time" runat="server" Text="结束时间" OnClick="btn_ed_time_Click" />
                   </td>
                    <td>
  <asp:Label ID="Label1" runat="server" Text="流程类别：" ></asp:Label>
                   </td>
                    <td>
   <asp:DropDownList ID="ddl_lclb" runat="server" Height="16px" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="ddl_lclb_SelectedIndexChanged">
       <asp:ListItem>全部</asp:ListItem>
                 <asp:ListItem>日常考核</asp:ListItem>
                            <asp:ListItem>事故通报</asp:ListItem>
                            <asp:ListItem>厂部考核</asp:ListItem>
                             <asp:ListItem>自主改善</asp:ListItem>
            </asp:DropDownList>
                   </td>
                    <td>
   <asp:Label ID="Label2" runat="server" Text="流程状态："></asp:Label>
                   </td>
                    <td>
 
            <asp:DropDownList ID="ddl_lczt" runat="server" Height="16px" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="ddl_lczt_SelectedIndexChanged">
                <asp:ListItem>全部</asp:ListItem>
                <asp:ListItem>已升效</asp:ListItem>
                <asp:ListItem>流转中</asp:ListItem>
                <asp:ListItem>其它</asp:ListItem>
            </asp:DropDownList>
                   </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="班别："></asp:Label>
                   </td>
                    <td>
 
            <asp:DropDownList ID="ddl_banbie" runat="server" Height="16px" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="ddl_lczt_SelectedIndexChanged">
                <asp:ListItem>全部</asp:ListItem>
                <asp:ListItem>甲班</asp:ListItem>
                            <asp:ListItem>乙班</asp:ListItem>
                            <asp:ListItem>丙班</asp:ListItem>
                            <asp:ListItem>丁班</asp:ListItem>
                            <asp:ListItem>综合组</asp:ListItem>
                            <asp:ListItem>铸铁组</asp:ListItem>
                            <asp:ListItem>污泥组</asp:ListItem>
                            <asp:ListItem>机关</asp:ListItem>
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
        <div style="text-align:left;margin:0 auto;width:90%;float:none; height:50%;">
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
   <asp:Panel ID="pnl_ed_date" runat="server" Visible="False" style="Z-INDEX: 141;width:269px;text-align:center;margin:0; POSITION: absolute; TOP: 35px" HorizontalAlign="Center">
  <asp:Calendar ID="cld_ed_date" runat="server" Visible="True" BackColor="White" OnSelectionChanged="Button3_Click"></asp:Calendar>
                <asp:Button ID="Button3" runat="server" Text="确定" OnClick="Button3_Click" Visible="False" />
            </asp:Panel>
                  
                </td>
            </tr>
            
        </table>
  </div>
         

    <div style="text-align:center;margin:0 auto;width:90%;float:none; height:760px;overflow:auto;">
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="12pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="750px" ShowBackButton="False" ShowFindControls="False" >
           
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dzswDataSetTableAdapters.SJ2B_KH_KaoHe_infoTableAdapter"></asp:ObjectDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dzswConnectionString %>" 
            SelectCommand="SELECT  ,[AppID],[FS_DateTime] ,[ApplicantName],[ApplicantIDCard],[AppName],[AppIDCard],[AppLevel],[AppKind]
      ,[AppAmount],[AppContent],[AppBy],[App_State] FROM [dzsw].[dbo].[Syl_AppWorkerinfo]" OnSelecting="SqlDataSource1_Selecting">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbx_bg_date" Name="bg_date" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbx_ed_date" Name="ed_date" PropertyName="Text" />
              
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    </form>
</body>
</html>
