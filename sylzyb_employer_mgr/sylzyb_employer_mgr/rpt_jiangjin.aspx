<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt_jiangjin.aspx.cs" Inherits="sylzyb_employer_mgr.rpt_jiangjin" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
  <div id="dv_jjhs">
        <div style="text-align:center;margin:0 auto;"  >
           <table style="text-align:center;width:950px;float:0;margin:auto;">
               <tr>
                   <td>
  <asp:TextBox ID="tbx_bg_date" runat="server" Enabled="False" Width="94px"></asp:TextBox> 
                   </td>
                    <td>
  <asp:Button ID="Button10" runat="server" Text="开始时间" OnClick="btn_bg_date_Click" />
                   </td>
                    <td>
  <asp:TextBox ID="tbx_ed_date" runat="server" Enabled="False" Width="94px"></asp:TextBox> 
                   </td>
                    <td>
<asp:Button ID="Button11" runat="server" Text="结束时间" OnClick="btn_ed_time_Click" />
                   </td>
                  
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="班别："></asp:Label>
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
<asp:RadioButtonList ID="rbl_banzhuORgeren" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
    <asp:ListItem Selected="True">班组</asp:ListItem>
                                 <asp:ListItem>个人</asp:ListItem>
                             </asp:RadioButtonList> 
                   </td>
                         <td> 
                             
                                 
   <asp:Button ID="btn_cx" runat="server" OnClick="btn_cx_Click" Text="查询" Width="50px" Height="21px" />


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
             <asp:Button ID="Button14" runat="server" Text="确定" OnClick="Button4_Click" Visible="False" />
       </asp:Panel>  
                </td>
            </tr>
                  <tr>
                <td   >
   <asp:Panel ID="pnl_ed_date" runat="server" Visible="False" style="Z-INDEX: 141;width:269px;text-align:center;margin:0; POSITION: absolute; TOP: 45px" HorizontalAlign="Center">
  <asp:Calendar ID="cld_ed_date" runat="server" Visible="True" BackColor="White" OnSelectionChanged="Button3_Click"></asp:Calendar>
                <asp:Button ID="Button13" runat="server" Text="确定" OnClick="Button3_Click" Visible="False" />
            </asp:Panel>
                  
                </td>
            </tr>
            
        </table>
  </div>
       

    <div style="text-align:center;margin:0 auto;width:950px;float:none; height:auto;overflow:auto;">
          
        <rsweb:ReportViewer ID="rv_jiangjin" runat="server" Font-Names="Verdana" Font-Size="12pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="500px" ShowBackButton="False" ShowFindControls="False" >
           
        </rsweb:ReportViewer>
      


      <%--  <rsweb:ReportViewer ID="rv_banzhu_jiangjin" runat="server" Font-Names="Verdana" Font-Size="12pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="200px" ShowBackButton="False" ShowFindControls="False" >
           
        </rsweb:ReportViewer>
<rsweb:ReportViewer ID="rv_geren_jiangjin" runat="server" Font-Names="Verdana" Font-Size="12pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="200px" ShowBackButton="False" ShowFindControls="False">
         
            
        </rsweb:ReportViewer>     
--%>
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dzswConnectionString %>" 
            SelectCommand="SELECT * FROM [Syl_Bonus_Group]">
        </asp:SqlDataSource> 


    
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:dzswConnectionString %>" 
            SelectCommand="SELECT * FROM [Syl_Bonus_Person]">
         </asp:SqlDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    </div>
    </form>
</body>
</html>
