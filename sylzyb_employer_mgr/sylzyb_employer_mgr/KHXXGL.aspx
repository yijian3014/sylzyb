<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="KHXXGL.aspx.cs" Inherits="sylzyb_employer_mgr.KHGL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  

    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

  <meta http-equiv="Expires" content="0"/> 
<meta http-equiv="Cache-Control" content="no-cache"/> 
<meta http-equiv="Pragma" content="no-cache"/> 


    <title>员工考核项管理</title>
    <style type="text/css">
        .sty_fm {
            text-align: center;
            margin: 0 auto;
            width: 980px;
        }



        .sty_dv_usr_banner {
            text-align: right;
        }

        .sty_qckh_dv {
            width: 980px;
            text-align: center;
            margin: 0 auto;
            float: none;
        }

        .sty_qckh_dv_tb1 {
            width: 100%;
        }
         .sty_qckh_dv_tb2_tr_td_name {
            width: 10%;
            height: 20px;
            text-align: left;
        }

        .sty_qckh_dv_tb2_tr_td_value {
            width: 12%;
            height: 20px;
            text-align: left;
        }
        .sty_qckh_dv_tb3 {
            width: 100%;
        }

       

        .sty_qckh_dv_tb3_tr_td_name {
            width: 100%;
            text-align: left;
        }

        .sty_qckh_dv_tb3_tr_td_value {
            width: 100%;
            text-align: left;
        }



        .sty_gailan_dv {
            text-align: center;
            margin: 0 auto;
            width: 100%;
            float: none;
        }

        .sty_gailan_dv_tb {
            width: 100%;
        }
        .sty_gailan_dv_tb_td{
            width:20%
        }
        .sty_khxd_dv {
            width: 100%;
            height: auto;
            text-align: center;
            float: none;
            margin: 0 auto;
        }
       .sty_khxd_dv_tb{
            width: 100%;
            text-align:center;
        }
        .sty_khxd_dv_tb_tr_td_bllc{
            width: 30%;
           text-align:right;
        }

        .sty_khxd_dv_tb_tr_td {
            width: 30%;
            text-align: center;
        }

        .sty_khxd_dv_tb {
            width: 100%;
            text-align: left;
        }

        .sty_khxd_dv_tb_tr_td_name {
            width:15%;
            height: 20px;
            text-align: left;
        }

        .sty_khxd_dv_tb_tr_td_value {
            width:35%;
            height: 20px;
            text-align: left;
        }

        .sty_khxd_dv_tb2_tr_td_name {
            width: 15%;
            height: 20px;
            text-align: left;
        }

        .sty_khxd_dv_tb2_tr_td_value {
            width: 85%;
            height: 20px;
            text-align: left;
        }

        .sty_shenpi_dv {
            width: 100%;
            text-align: center;
            float: none;
            margin: 0 auto;
        }

        .sty_shenpi_dv_tb {
            width: 100%;
        }

        .sty_shenpi_dv_tb_tr_td {
            width: 11%;
        }

        .sty_shenpi_dv_tb2 {
            width: 100%;
        }

        .sty_shenpi_dv_tb2_tr {
            text-align: left;
            width: 100%;
        }

        .sty_shenpi_dv_tb2_tr_td {
            width: 100%;
        }
      
              
        .auto-style1 {
            width: 100%;
            height: 30px;
        }
      
              
        .auto-style2 {
            margin-bottom: 0px;
        }
      
              
        .auto-style3 {
            width: 100%;
            text-align: center;
        }
      
              
        </style>

</head>
    <script "JavaScript"> 
    //用于禁用IE的回退功能
        javascript: window.history.forward(1);       
</script> 

<body>
    <form id="fm" runat="server" class="sty_fm">
        <asp:Label ID="Label26" runat="server" Text="考核管理" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <div id="usr_banner" class="sty_dv_usr_banner">
            <hr />
            <asp:Label ID="Label25" runat="server" Text="用户名："></asp:Label>
            <asp:Label ID="login_user" runat="server" Text=""></asp:Label>

            <asp:Button ID="btn_exit" runat="server" Text="退出登陆" OnClick="btn_exit_Click" />
        </div>
        <div id="dv_qicaokaohe" runat="server"  class="sty_qckh_dv">
            <table id="tb1" class="sty_qckh_dv_tb1">
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="起草考核" Font-Bold="False" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="tb2">
                <tr>
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label15" runat="server" Text="编号:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_qckh_AppraiseID" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label28" runat="server" Text="流转状态:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_qckh_Flow_State" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label29" runat="server" Text="提出人姓名:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_qckh_ApplicantName" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label33" runat="server" Text="考核级别:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:DropDownList ID="ddl_qckh_Applevel" runat="server">
                            <asp:ListItem>厂部考核</asp:ListItem>
                            <asp:ListItem>作业部考核</asp:ListItem>
                            <asp:ListItem>班组考核</asp:ListItem>
                           
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
<td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label31" runat="server" Text="类型:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:DropDownList ID="ddl_qckh_AppKind" runat="server">
                            <asp:ListItem>日常考核</asp:ListItem>
                            <asp:ListItem>事故通报</asp:ListItem>                        
                             <asp:ListItem>自主改善</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label36" runat="server" Text="金额:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_qckh_AppAmount" runat="server" Text="空"></asp:Label>
                    </td>

                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label38" runat="server" Text="提出时间:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_qckh_TC_DateTime" runat="server" Text="空"></asp:Label>
                    
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label41" runat="server" Text="发生时间:"></asp:Label>
                    </td>
                    <td class="sty_qckh_dv_tb2_tr_td_value">

                        <asp:TextBox ID="tbx_qckh_FS_DateTime" runat="server" Height="19px" Width="111px" OnTextChanged="DateCheck"></asp:TextBox>

                    </td>
                    </tr>
                    <tr>
                      <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label32" runat="server" Text="涉及班组:"></asp:Label>
                         </td>
                         <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:DropDownList ID="ddl_qckh_AppGroup" runat="server" OnSelectedIndexChanged="ddl_qckh_AppGroup_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem></asp:ListItem>
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
                    <td class="sty_qckh_dv_tb2_tr_td_name">
                        <asp:Label ID="Label23" runat="server" Text="涉及核人员:"></asp:Label>
                        </td>
                        <td class="sty_qckh_dv_tb2_tr_td_value">
                  
                              <asp:CheckBox ID="cb_qckh_ksfz" runat="server" Text="快速值赋值：" OnCheckedChanged="cb_qckh_ksfz_CheckedChanged" AutoPostBack="True" />
                  
                        </td>
                        <td class="sty_qckh_dv_tb2_tr_td_value">
                        <asp:TextBox ID="tbx_qckh_ksfz" runat="server" Enabled="False" CssClass="auto-style2" >0</asp:TextBox>
                       
                        </td>
                        <td class="sty_qckh_dv_tb2_tr_td_value">
 <asp:Label ID="lb_qckh_yuan" runat="server" Text="元"></asp:Label>
                        </td>
                    
                     </tr>

           

            </table>
            <table id="tb3" class="sty_qckh_dv_tb3">
               
                <tr>
                    <td style="text-align: left;">
                        <asp:CheckBoxList ID="cbl_workers" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                          <asp:Button ID="btn_appworker_add" runat="server" OnClick="btn_appworker_add_Click" Text="添加并刷新" />
                     
                    </td>

                </tr>
                <tr>
                    <td class="auto-style3">                    
                        <asp:GridView ID="gv_AppWorker" runat="server" HorizontalAlign="Center" Width="100%" Height="100px" AutoGenerateColumns="False" EnableModelValidation="True" Font-Size="Small" PageSize="5" OnRowDataBound="gv_RowDataBound" OnSelectedIndexChanged="gv_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="AppID" HeaderText="考核编号" />
                                <asp:BoundField DataField="FS_DateTime" HeaderText="考核发生时间" />
                                <asp:BoundField DataField="ApplicantName" HeaderText="考核提出人姓名" Visible="False" />
                                <asp:BoundField DataField="ApplicantIDCard" HeaderText="考核提出人身份证号" Visible="False" />
                                <asp:BoundField DataField="AppName" HeaderText="被考核人姓名" />
                                <asp:BoundField DataField="AppIDCard" HeaderText="被考核人身份证号" />
                                <asp:BoundField DataField="Applevel" HeaderText="考核级别" />
                                <asp:BoundField DataField="AppKind" HeaderText="考核类型" />
                                <asp:TemplateField HeaderText="考核金额">
                                    <ItemTemplate>
                                       <asp:TextBox ID="tbx_gv_AppAmount" runat="server"  Width="175px" Visible="False" ></asp:TextBox>
                                        <asp:Button ID="btn_gv_AppMount_Update" runat="server" Text="更新" Visible="False" OnClick="qckh_update_AppMount"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="AppAmount" HeaderText="考核金额" />--%>
                                <asp:BoundField DataField="AppContent" HeaderText="考核内容" />
                                <asp:BoundField DataField="AppBy" HeaderText="考核依据" Visible="False" />
                                <asp:BoundField DataField="App_State" HeaderText="考核状态" />



                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_name">
                        <asp:Label ID="Label58" runat="server" Text="考核内容:"></asp:Label></td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                        <asp:TextBox ID="tbx_qckh_AppContent" runat="server" Height="18px" Width="962px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_name">
                        <asp:Label ID="Label60" runat="server" Text="考核依据:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                        <asp:TextBox ID="tbx_qckh_AppBy" runat="server" Height="141px" Width="962px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                        <asp:RadioButtonList ID="rbl_qckh_nextORprevious" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_qckh_nextORprevious_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>转交</asp:ListItem>
                        </asp:RadioButtonList>   
                      
                          </td>
                </tr> 

                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                           <asp:RadioButtonList ID="rbl_qckh_step" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbl_qckh_step_SelectedIndexChanged">
                            
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                       <asp:CheckBox ID="cb_qckh_is_huiqian" runat="server" Text="允许多人会签" OnCheckedChanged="cb_qckh_is_huiqian_CheckedChanged" AutoPostBack="True" />
                        <asp:CheckBoxList ID="cbl_qckh_next_persion" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="cbl_qckh_next_persion_SelectedIndexChanged">
                        </asp:CheckBoxList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_qckh_ok" runat="server" Text="确认并返回" Width="99px" OnClick="btn_qckh_ok_Click" />
                        <asp:Button ID="btn_xgkh_ok" runat="server" Text="修改并返回" Width="99px" OnClick="btn_xgkh_ok_Click" style="height: 21px" />
                        <asp:Button ID="btn_qckh_cancel" runat="server" Text="取消" Width="99px" OnClick="btn_qckh_cancel_Click" />
                    </td>
                </tr>
            </table>

        </div>

        <div id="dv_gailan" runat="server"  class="sty_gailan_dv">
            <table class="sty_gailan_dv_tb">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="相关考核概览" Font-Bold="False" Font-Size="Larger"></asp:Label>
                        <hr />
                    </td>
                </tr>
            </table>
            <table class="sty_gailan_dv_tb">
                <tr>
                    <td >
                        <asp:RadioButtonList ID="rbl_gailan_cx" runat="server" RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="True" OnSelectedIndexChanged="rbl_gailan_cx_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">总览</asp:ListItem>
                            <asp:ListItem Value="1">待办理</asp:ListItem>
                            <asp:ListItem Value="2">已办结</asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:Label ID="Label43" runat="server" Text="（提出）开始时间："></asp:Label>
                        <asp:TextBox ID="tbx_bg_time" runat="server" OnTextChanged="DateCheck" Width="120px"></asp:TextBox>
                    </td>
                    <td style="text-align:left;">
                        <asp:Label ID="Label44" runat="server" Text="（提出）结束时间："></asp:Label>
                        <asp:TextBox ID="tbx_ed_time" runat="server" OnTextChanged="DateCheck" Width="120px"></asp:TextBox>
                        <asp:Button ID="btn_reflash" runat="server" OnClick="btn_reflash_Click" Text="刷新" />
                    </td>
                  
                </tr>
                <tr >
                    <td style="text-align:left;">
                         <asp:Button ID="btn_qckh" runat="server" Text="提出" OnClick="btn_qckh_Click" Width="60px"/>
                      
                          <asp:Button ID="btn_xgkh" runat="server" Text="修改" OnClick="btn_xgkh_Click" Visible="False" Width="60px"/>
                         <asp:Button ID="btn_sckh" runat="server" Text="删除" OnClick="btn_sckh_Click" Visible="False" Width="60px"/>
                      
                        <asp:Button ID="btn_shenpikaohe" runat="server" Text="审批" OnClick="btn_shenpikaohe_Click" Visible="False" Width="60px"/>
                      
                        <asp:Button ID="btn_khgd" runat="server" Text="归档" OnClick="btn_khgd_Click" Visible="False" Width="60px"/>
                    </td>
                    
                     <td>
                          
                    </td>
                     <td style="text-align:left;">
                         <asp:Button ID="btn_qzzj" runat="server" Text="强制转交" OnClick="btn_qzzj_Click" Visible="False" Height="21px" />
                         <asp:Button ID="btn_qzsc" runat="server" Text="强制删除" OnClick="btn_qzsc_Click" Visible="False" />
                          <asp:Button ID="btn_qzxg" runat="server" Text="强制修改" OnClick="btn_qzxg_Click" Visible="False" />
                          <asp:Button ID="btn_qzsx" runat="server" Text="强制生效" OnClick="btn_qzsx_Click" Visible="False" />
                    </td>
                   

                </tr>
            </table>

            <table class="sty_qckh_dv_tb1">
                <tr>
                    <td>
                        <div style="width:980px;height:400px;overflow:auto;">
                        <asp:GridView ID="gv_App_gailan" runat="server" HorizontalAlign="Center" Width="960px" Height="200px" OnSelectedIndexChanged="gv_App_gailan_SelectedIndexChanged" OnRowDataBound="gv_App_gailan_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True" Font-Size="Small" AllowPaging="True" OnPageIndexChanging="gv_App_gailan_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="AppID" HeaderText="编号" />
                                <asp:BoundField DataField="Flow_State" HeaderText="流转状态" >
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ApplicantName" HeaderText="提出人" />
                                <asp:BoundField DataField="ApplicantIDCard" HeaderText="提出人身份证号" Visible="False"/>
                                <asp:BoundField DataField="Applevel" HeaderText="级别" Visible="False"/>
                                <asp:BoundField DataField="AppKind" HeaderText="类型" Visible="False" />
                                <asp:BoundField DataField="AppAmount" HeaderText="金额" />
                                <asp:BoundField DataField="TC_DateTime" HeaderText="提出时间" Visible="False"/>
                                <asp:BoundField DataField="FS_DateTime" HeaderText="发生时间" />
                                <asp:BoundField DataField="AppGroup" HeaderText="班组" />
                                <asp:BoundField DataField="AppNames" HeaderText="人员" Visible="False"/>
                                <asp:BoundField DataField="AppContent" HeaderText="考核内容" >                            
                                </asp:BoundField>
                                <asp:BoundField DataField="AppBy" HeaderText="考核依据" >
                                </asp:BoundField>

                                <%--一级审批--%>
                                <asp:BoundField DataField="step_1_Oponion" HeaderText="点检意见" >
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="step_1_Comment" HeaderText="点检评论" Visible="False" />
                                <%--二级审批--%>
                                <asp:BoundField DataField="step_2_Oponion" HeaderText="白班段长或工程师意见" >
                                <HeaderStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="step_2_Comment" HeaderText="白班段长或工程师评论" Visible="False" />
                                <%--三级审批--%>
                                <asp:BoundField DataField="step_3_Oponion" HeaderText="主管领导意见" >
                                <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="step_3_Comment" HeaderText="主管领导评论" Visible="False" />
                                <%--四级审批--%>
                                <asp:BoundField DataField="step_4_Oponion" HeaderText="书记意见" >
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="step_4_Comment" HeaderText="书记评论" Visible="False" />
                                <%--五级审批--%>
                                <asp:BoundField DataField="step_5_Oponion" HeaderText="部长意见" >
                                <HeaderStyle Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="step_5_Comment" HeaderText="部长评论" Visible="False" />
                            </Columns>
                            <RowStyle Wrap="True" />
                        </asp:GridView>
                            </div>
                    </td>
                </tr>
            </table>

        </div>


        <div id="dv_khxd" runat="server" class="sty_khxd_dv">
            <table class="sty_khxd_dv_tb">
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td"></td>
                    <td class="sty_khxd_dv_tb_tr_td">
                        <asp:Label ID="Label3" runat="server" Text="考核详单" Font-Bold="False" Font-Size="Larger"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_bllc">

                        &nbsp;</td>
                </tr>
            </table>


            <hr />
            <table class="sty_khxd_dv_tb">
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label4" runat="server" Text="编号:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_AppraiseID" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label5" runat="server" Text="流转状态:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_Flow_State" runat="server" Text="空"></asp:Label>
                    </td>
                   
                </tr>
                <tr>
                      <td class="sty_khxd_dv_tb_tr_td_name">
 <asp:Label ID="Label6" runat="server" Text="提出人姓名:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
 <asp:Label ID="lb_khxd_ApplicantName" runat="server" Text="空"></asp:Label>
                    </td>
                       <td class="sty_khxd_dv_tb_tr_td_name">
   <asp:Label ID="Label7" runat="server" Text="提出人身份证号:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
 <asp:Label ID="lb_khxd_ApplicantIDCard" runat="server" Text="空"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label30" runat="server" Text="考核等级:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_Applevel" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label8" runat="server" Text="类型:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_AppKind" runat="server" Text="空"></asp:Label>
                    </td>
                 

                  
                </tr>
                <tr>
                       <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label47" runat="server" Text="金额:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_AppAmount" runat="server" Text="空"></asp:Label>
                    </td>
                        <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label49" runat="server" Text="提出时间:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_TC_DateTime" runat="server" Text="空"></asp:Label>
                    </td>
                   
                     </tr>
                <tr>
                 <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label51" runat="server" Text="发生时间:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_FS_DateTime" runat="server" Text="空"></asp:Label>
                    </td>
                    
                   <td class="sty_khxd_dv_tb_tr_td_name">
                     <asp:Label ID="Label45" runat="server" Text="涉及班组:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                          <asp:Label ID="lb_khxd_AppGroup" runat="server" Text="空"></asp:Label>
                    </td>      
                </tr>
                
            </table>
            <table class="sty_khxd_dv_tb">
                
                 <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                      <asp:Label ID="Label9" runat="server" Text="涉及人员:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_khxd_AppNames" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                      <asp:Label ID="Label12" runat="server" Text="考核内容:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb2_tr_td_value">
                         <asp:Label ID="lb_khxd_AppContent" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label19" runat="server" Text="考核依据:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb2_tr_td_value">
                     
                        <asp:TextBox ID="tbx_khxd_AppBy" runat="server"  Width="100%" Hight="auto" TextMode="MultiLine" Height="104px"></asp:TextBox>
                    </td>
                </tr>

                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label39" runat="server" Text="点检意见:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_khxd_step_1_Oponion" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label14" runat="server" Text="点检评论:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_khxd_Step_1_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"  Height="104px"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label53" runat="server" Text="白班段长或工程师审批意见:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_step_2_Oponion" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label35" runat="server" Text="白班段长或工程师评论:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_khxd_step_2_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"  Height="104px"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label16" runat="server" Text="主管领导意见:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_step_3_Oponion" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>

                <tr>

                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label17" runat="server" Text="主管领导评论:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_khxd_step_3_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"  Height="104px"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label18" runat="server" Text="书记意见:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_step_4_Oponion" runat="server" Text="空"></asp:Label>

                    </td>
                </tr>

                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label20" runat="server" Text="书记评论:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_khxd_step_4_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"  Height="104px"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label21" runat="server" Text="部长意见:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_khxd_step_5_Oponion" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label22" runat="server" Text="部长评论:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_khxd_step_5_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine" Height="104px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label34" runat="server" Text="管理信息："></asp:Label>
                        </td>
                       <td class="sty_khxd_dv_tb2_tr_td_value">
                     
                        <asp:TextBox ID="tbx_khgl_info" runat="server"  Width="100%" Hight="auto" TextMode="MultiLine" Height="104px"></asp:TextBox>
                    </td>
                    </tr>
                </table>
            <asp:GridView ID="gv_detail_appworker" runat="server" HorizontalAlign="Center" Width="100%" Height="100px" AutoGenerateColumns="False" EnableModelValidation="True" Font-Size="Small">
                <Columns>
                    
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="AppID" HeaderText="考核编号" />
                    <asp:BoundField DataField="FS_DateTime" HeaderText="考核发生时间" />
                    <asp:BoundField DataField="ApplicantName" HeaderText="考核提出人姓名" />
                    <asp:BoundField DataField="ApplicantIDCard" HeaderText="考核提出人身份证号" Visible="False" />
                    <asp:BoundField DataField="AppName" HeaderText="相关人姓名" />
                    <asp:BoundField DataField="AppIDCard" HeaderText="相关人身份证号" Visible="False" />
                    <asp:BoundField DataField="Applevel" HeaderText="考核级别" />
                    <asp:BoundField DataField="AppKind" HeaderText="考核类型" />
                    <asp:BoundField DataField="AppAmount" HeaderText="考核金额" />
                    <asp:BoundField DataField="AppContent" HeaderText="考核内容" />
                    <asp:BoundField DataField="AppBy" HeaderText="考核依据" />
                    <asp:BoundField DataField="App_State" HeaderText="考核状态" />
                    
                </Columns>
              
            </asp:GridView>
        </div>
        <div id="dv_shenpi" runat="server" class="sty_shenpi_dv">
            <asp:Label ID="Label1" runat="server" Text="审批" Font-Bold="False" Font-Size="Larger"></asp:Label>
            <hr />
            <table class="sty_shenpi_dv_tb">
                <tr>
                    <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:Label ID="Label10" runat="server" Text="审批状态:"></asp:Label>

                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:DropDownList ID="ddl_shenpi_zt" runat="server">
                            <asp:ListItem Selected="True">同意</asp:ListItem>
                            <asp:ListItem>不同意</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:Label ID="Label27" runat="server" Text="考核总金额:"></asp:Label>
                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td">
                         <asp:Label ID="lb_shenpi_kh_zhongjinger" runat="server" Text="空"></asp:Label>
                    </td>
                    
                     <td class="sty_shenpi_dv_tb_tr_td">

                        <asp:Label ID="Label24" runat="server" Text="审批模式："></asp:Label>

                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td">

                        <asp:Label ID="lb_shenpi_shenpimoshi" runat="server" Text="空"></asp:Label>

                    </td>
                   
                    <td class="sty_shenpi_dv_tb_tr_td">

                        <asp:Label ID="Label61" runat="server" Text="未会签人员："></asp:Label>

                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td">

                        <asp:Label ID="lb_shenpi_wei_huiqianren" runat="server" Text="空"></asp:Label>

                    </td>
                   
                </tr>
            </table>
            <table class="sty_shenpi_dv_tb">
                <tr>
                    <td class="sty_shenpi_dv_tb2_tr">
                        <asp:Label ID="Label13" runat="server" Text="审批意见:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_shenpi_dv_tb2_tr_td">
                        <asp:TextBox ID="tbx_shenpi_yj" runat="server" Height="225px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td class="sty_shenpi_dv_tb2_tr_td" style="text-align:left;" >
                        <asp:CheckBox ID="cb_shenpi_qzzj" runat="server" Text="强制" />
                          <asp:RadioButtonList ID="rbl_shenpi_nextORprevious" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_shenpi_nextORprevious_SelectedIndexChanged" RepeatDirection="Horizontal">
                              <asp:ListItem>转交</asp:ListItem>
                              <asp:ListItem>回退</asp:ListItem>                            
                              <asp:ListItem>会签</asp:ListItem>
                        </asp:RadioButtonList>


                        <asp:CheckBox ID="cb_shenpi_is_huiqian" runat="server" Text="下一步允许多人会签" OnCheckedChanged="cb_shenpi_is_huiqian_CheckedChanged" AutoPostBack="True" />
                    </td>
               </tr>
                <tr>
                    <td class="auto-style1" style="text-align: left;">
                        <asp:RadioButtonList ID="rbl_shenpi_step" runat="server" OnSelectedIndexChanged="rbl_shenpi_step_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal"></asp:RadioButtonList>

                    </td>
                </tr>
                <tr>
                    <td class="sty_shenpi_dv_tb2_tr_td" style="text-align: left;">
                        <asp:CheckBoxList ID="cbl_shenpi_next_persion" runat="server" OnSelectedIndexChanged="cbl_shenpi_next_persion_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </td>
                </tr>


                <tr>
                    <td class="sty_shenpi_dv_tb2_tr_td">
                        <asp:Button ID="btn_shenpi_ok" runat="server" Text="确认并返回" Width="99px" OnClick="btn_shenpi_ok_Click" />
                        <asp:Button ID="btn_shenpi_cancel" runat="server" Text="取消并返回" Width="99px" OnClick="btn_shenpi_cancel_Click" />
                    </td>
                </tr>
                 
            </table>
        </div>
       


    </form>
</body>

</html>
