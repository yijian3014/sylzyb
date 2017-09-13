<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="sylzyb_employer_mgr.Report" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
    <script type="text/javascript">
        window.history.forward(1);
       
    </script>
<body style="width:auto 1024px; text-align:center; float:none;margin:auto;" >
  <form id="form2" runat="server"  style="text-align:center;width:auto 1024px;height:auto 1000px;float:none;margin:auto;" >
       <div>
           <asp:Button ID="btn_rpt_khlc" runat="server" Text="考核流程信息报表" OnClick="btn_rpt_khlc_Click" />
           <asp:Button ID="btn_rpt_khzj" runat="server" Text="考核资金信息报表" OnClick="btn_rpt_khzj_Click" />
           <asp:Button ID="bgn_rpt_jiangjin" runat="server" Text="奖金核算信息报表" OnClick="bgn_rpt_jiangjin_Click" />
           
            <asp:Button ID="btn_exit" runat="server" Text="退出登陆" OnClick="btn_exit_Click" />
        
           </div> 
      <div style="text-align:center">
          <asp:Panel id="pnl_khlc" style="width:1024px auto;text-align:center;float:none;margin:auto;" runat="server" Visible="False">
         <iframe id="ifm_khlc" style="width:1024px;height:800px;"  src="rpt_khlc.aspx"></iframe> 
            </asp:Panel>
          </div> 
<div >
    <asp:Panel id="pnl_khzj" style="width:1024px auto; text-align:center;float:none;margin:auto;" runat="server"  Visible="False">
          <iframe id="ifm_khzj" style="width:1024px;height:800px;"  src="rpt_khzj.aspx"></iframe> 
            </asp:Panel>
         

      </div> 
               
 <div >
    <asp:Panel id="pnl_jiangjin" style="width:1024px auto;text-align:center;float:none;margin:auto;" runat="server" runat="server"  Visible="False">
          <iframe id="ifm_jiangjin" style="width:1024px;height:800px;"  src="rpt_jiangjin.aspx"></iframe> 
            </asp:Panel>
         

      </div> 
    </form>
</body>
</html>
