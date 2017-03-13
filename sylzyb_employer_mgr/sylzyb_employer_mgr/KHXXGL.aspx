﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KHXXGL.aspx.cs" Inherits="sylzyb_employer_mgr.KHGL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .sty_fm {
            text-align: center;
            margin: 0 auto;
            width: 980px;
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

        .sty_dv_usr_banner {
            text-align: right;
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

        .sty_gailan_dv_banner {
            text-align: center;
            margin: 0 auto;
            width: 100%;
            float: none;
        }

        .sty_gailan_dv {
            text-align: center;
            margin: 0 auto;
            width: 100%;
            float: none;
        }

        .sty_gailan_dv_tb_banner {
            width: 98%;
            text-align: right;
            margin: 0 auto;
        }

        .sty_gailan_dv {
            width: 98%;
            text-align: center;
        }

        .sty_khxd_dv {
            width: 100%;
            height: auto;
            text-align: center;
            float: none;
            margin: 0 auto;
        }

        .sty_khxd_dv_tb_banner {
            width: 98%;
        }

        .sty_khxd_dv_tb_tr_td_banner {
            width: 30%;
            text-align: center;
        }

        .sty_khxd_dv_tb {
            width: 100%;
            text-align: left;
        }

        .sty_khxd_dv_tb_tr_td_name {
            width: 15%;
            height: 20px;
            text-align: left;
        }

        .sty_khxd_dv_tb_tr_td_value {
            width: 10%;
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
            width: 15%;
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
    </style>
</head>
<body>
    <form id="fm" runat="server" class="sty_fm">
        <asp:Label ID="Label26" runat="server" Text="考核管理" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <div class="sty_dv_usr_banner">
            <hr />
            <asp:Label ID="Label25" runat="server" Text="用户名："></asp:Label>
            <asp:Label ID="login_user" runat="server" Text=""></asp:Label>

            <asp:Button ID="btn_exit" runat="server" Text="退出" OnClick="btn_exit_Click" />
        </div>
        <div class="sty_qckh_dv">
            <table id="tb1" class="sty_qckh_dv_tb1">
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="起草考核" Font-Bold="False" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="tb2">
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label15" runat="server" Text="编号:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_qckh_AppraiseID" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label28" runat="server" Text="流转状态:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_qckh_Flow_State" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label29" runat="server" Text="提出人姓名:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_qckh_ApplicantName" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label31" runat="server" Text="类型:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:DropDownList ID="ddl_qckh_AppKind" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>

                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label36" runat="server" Text="金额:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_qckh_" runat="server" Text="空"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label38" runat="server" Text="提出时间:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="Label40" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label41" runat="server" Text="事件发生时间:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">

                        <asp:TextBox ID="TextBox1" runat="server" Height="19px" Width="120px"></asp:TextBox>

                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label54" runat="server" Text="被考核人所在班组:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                    </td>

                </tr>

                <tr>

                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label56" runat="server" Text="被考核对象:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">

                        <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_name"></td>
                    <td class="sty_khxd_dv_tb_tr_td_value"></td>
                    <td class="sty_khxd_dv_tb_tr_td_name"></td>
                    <td class="sty_khxd_dv_tb_tr_td_value"></td>
                    <td class="sty_khxd_dv_tb_tr_td_name"></td>
                    <td class="sty_khxd_dv_tb_tr_td_value"></td>

                </tr>
            </table>
            <table id="tb3" class="sty_qckh_dv_tb3">
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_name">
                        <asp:Label ID="Label58" runat="server" Text="考核内容:"></asp:Label></td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                        <asp:TextBox ID="TextBox2" runat="server" Height="18px" Width="962px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_name">
                        <asp:Label ID="Label60" runat="server" Text="考核依据:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_qckh_dv_tb3_tr_td_value">
                        <asp:TextBox ID="TextBox3" runat="server" Height="141px" Width="962px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="确认转交下一步" Width="99px" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" Text="回退至上一步" />
                        <asp:Button ID="Button3" runat="server" Text="取消" Width="99px" OnClick="Button2_Click" />
                    </td>
                </tr>
            </table>

        </div>

        <div class="sty_dv_banner_ganlan">

            <asp:Label ID="Label2" runat="server" Text="相关考核概览" Font-Bold="False" Font-Size="Larger"></asp:Label>
            <hr />
        </div>


        <div class="sty_gailan_dv">
            <table class="sty_gailan_dv_tb_banner">
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rbl_cx" runat="server" RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">总览</asp:ListItem>
                            <asp:ListItem Value="1">待办理</asp:ListItem>
                            <asp:ListItem Value="2">已办结</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:Label ID="Label43" runat="server" Text="开始时间："></asp:Label>
                        <asp:TextBox ID="tbx_bg_time" runat="server" OnTextChanged="tbx_time_TextChanged" Width="120px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label44" runat="server" Text="结束时间："></asp:Label>
                        <asp:TextBox ID="tbx_ed_time" runat="server" OnTextChanged="tbx_time_TextChanged" Width="120px"></asp:TextBox>
                        <asp:Button ID="btn_reflash" runat="server" OnClick="btn_reflash_Click" Text="刷新" />
                    </td>
                    <td>
                        <asp:Button ID="btn_tckh" runat="server" Text="提出考核" OnClick="btn_tckh_Click" />
                    </td>
                </tr>
            </table>


        </div>
        <div class="sty_gailan_dv">
            <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" Width="100%" Height="200px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True" OnRowCreated="GridView1_RowCreated" Font-Size="Small">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="AppraiseID" HeaderText="编号" />
                    <asp:BoundField DataField="Flow_State" HeaderText="流转状态" />
                    <asp:BoundField DataField="ApplicantName" HeaderText="提出人姓名" />
                    <asp:BoundField DataField="ApplicantIDCard" HeaderText="提出人身份证号" />
                    <asp:BoundField DataField="AppKind" HeaderText="类型" />
                    <asp:BoundField DataField="AppAmount" HeaderText="金额" />
                    <asp:BoundField DataField="TC_DateTime" HeaderText="提出时间" />
                    <asp:BoundField DataField="FS_DateTime" HeaderText="事件发生时间" />
                    <asp:BoundField DataField="AppGroup" HeaderText="被考核人所在班组" />
                    <asp:BoundField DataField="AppName" HeaderText="被考核对象" />
                    <asp:BoundField DataField="AppContent" HeaderText="考核内容" />
                    <asp:BoundField DataField="AppBy" HeaderText="考核依据" />

                    <%--一级审批--%>
                    <asp:BoundField DataField="Styp_1_Oponion" HeaderText="意见汇总（组长）" />
                    <asp:BoundField DataField="Styp_1_Comment" HeaderText="评论汇总（组长）" Visible="False" />
                    <%--二级审批--%>
                    <asp:BoundField DataField="Styp_2_Oponion" HeaderText="意见汇总（工程师）" />
                    <asp:BoundField DataField="Styp_2_Comment" HeaderText="批评论汇总（工程师）" Visible="False" />
                    <%--三级审批--%>
                    <asp:BoundField DataField="Styp_3_Oponion" HeaderText="意见汇总（区域主管）" />
                    <asp:BoundField DataField="Styp_3_Comment" HeaderText="评论汇总（区域主管）" Visible="False" />
                    <%--四级审批--%>
                    <asp:BoundField DataField="Styp_4_Oponion" HeaderText="意见汇总（书记）" />
                    <asp:BoundField DataField="Styp_4_Comment" HeaderText="评论汇总（书记）" Visible="False" />
                    <%--五级审批--%>
                    <asp:BoundField DataField="Styp_5_Oponion" HeaderText="意见汇总（部长）" />
                    <asp:BoundField DataField="Styp_5_Comment" HeaderText="评论汇总（部长）" Visible="False" />
                </Columns>
            </asp:GridView>
        </div>


        <div id="div_khxd" runat="server" class="sty_khxd_dv">
            <table class="sty_khxd_dv_tb_banner">
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_banner"></td>
                    <td class="sty_khxd_dv_tb_tr_td_banner">
                        <asp:Label ID="Label3" runat="server" Text="考核详单" Font-Bold="False" Font-Size="Larger"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_banner" style="text-align: right;">
                        <asp:Button ID="BTN_BLLC" runat="server" Text="办理流程" OnClick="BTN_BLLC_Click" />

                    </td>
                </tr>
            </table>


            <hr />
            <table class="sty_khxd_dv_tb">
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label4" runat="server" Text="编号:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_AppraiseID" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label5" runat="server" Text="流转状态:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_Flow_State" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label6" runat="server" Text="提出人姓名:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_ApplicantName" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label7" runat="server" Text="提出人身份证号:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_ApplicantIDCard" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label8" runat="server" Text="类型:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_AppKind" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label47" runat="server" Text="金额:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="Label48" runat="server" Text="空"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label49" runat="server" Text="提出时间:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="Label50" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label51" runat="server" Text="事件发生时间:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="Label52" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label45" runat="server" Text="被考核人所在班组:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="Label46" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label9" runat="server" Text="被考核对象:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_AppAmount" runat="server" Text="空"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label12" runat="server" Text="考核内容:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_TC_DateTime" runat="server" Text="空"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_name">
                        <asp:Label ID="Label19" runat="server" Text="考核依据:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_FS_DateTime" runat="server" Text="空"></asp:Label>
                    </td>

                </tr>
            </table>
            <table style="text-align: left;" class="auto-style6">
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label39" runat="server" Text="意见汇总（组长）:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb2_tr_td_value">
                        <asp:Label ID="lb_Styp_1_Oponion" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label14" runat="server" Text="评论汇总（组长）:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_Styp_1_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label53" runat="server" Text="意见汇总（工程师）:" Visible="False"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_Styp_2_Oponion" runat="server" Text="空" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label35" runat="server" Text="批评论汇总（工程师）:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_Styp_2_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label16" runat="server" Text="意见汇总（区域主管）:" Visible="False"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_Styp_3_Oponion" runat="server" Text="空" Visible="False"></asp:Label>
                    </td>
                </tr>

                <tr>

                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label17" runat="server" Text="评论汇总（区域主管）:" Visible="False"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_Styp_3_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine" Visible="False"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label18" runat="server" Text="意见汇总（书记）:" Visible="False"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_Styp_4_Oponion" runat="server" Text="空" Visible="False"></asp:Label>

                    </td>
                </tr>

                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label20" runat="server" Text="评论汇总（书记）:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_Styp_4_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>

                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label21" runat="server" Text="意见汇总（部长）:"></asp:Label>
                    </td>
                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:Label ID="lb_Styp_5_Oponion" runat="server" Text="空"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sty_khxd_dv_tb2_tr_td_name">
                        <asp:Label ID="Label22" runat="server" Text="评论汇总（部长）:"></asp:Label>
                    </td>

                    <td class="sty_khxd_dv_tb_tr_td_value">
                        <asp:TextBox ID="tbx_Styp_5_Comment" runat="server" Width="100%" Hight="auto" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>

            </table>
            <asp:GridView ID="gv_detail_appworker" runat="server" HorizontalAlign="Center" Width="100%" Height="100px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True" OnRowCreated="GridView1_RowCreated" Font-Size="Small">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="AppID" HeaderText="考核编号" />
                    <asp:BoundField DataField="FS_DateTime" HeaderText="考核发生时间" />
                    <asp:BoundField DataField="ApplicantName" HeaderText="考核提出人姓名" />
                    <asp:BoundField DataField="ApplicantIDCard" HeaderText="考核提出人身份证号" />
                    <asp:BoundField DataField="AppName" HeaderText="被考核人姓名" />
                    <asp:BoundField DataField="AppIDCard" HeaderText="被考核人身份证号" />
                    <asp:BoundField DataField="AppKind" HeaderText="考核类型" />
                    <asp:BoundField DataField="AppAmount" HeaderText="考核金额" />
                    <asp:BoundField DataField="AppContent" HeaderText="考核内容" />
                    <asp:BoundField DataField="AppBy" HeaderText="考核依据" />
                    <asp:BoundField DataField="App_State" HeaderText="考核状态" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="dv_shenpidan" runat="server" class="sty_shenpi_dv">
            <asp:Label ID="Label1" runat="server" Text="审批单" Font-Bold="False" Font-Size="Larger"></asp:Label>
            <hr />
            <table class="sty_shenpi_dv_tb">
                <tr>
                    <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:Label ID="Label10" runat="server" Text="审批状态:"></asp:Label>

                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:DropDownList ID="ddl1_sp_zt" runat="server">
                            <asp:ListItem Selected="True">同意</asp:ListItem>
                            <asp:ListItem>不同意</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td"></td>
                    <td class="sty_shenpi_dv_tb_tr_td"></td>
                    <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:Label ID="Label27" runat="server" Text="考核总金额:"></asp:Label>
                    </td>
                    <td class="sty_shenpi_dv_tb_tr_td">
                        <asp:TextBox ID="tbx_kh_zhongjinger" runat="server" Height="16px" Width="100%" Enabled="False"></asp:TextBox>
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
                        <asp:TextBox ID="tb_sp_yj" runat="server" Height="225px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="sty_shenpi_dv_tb2_tr_td">
                        <asp:Button ID="btn_ok" runat="server" Text="确认转交下一步" Width="99px" OnClick="Button1_Click" />
                        <asp:Button ID="btn_back_step" runat="server" Text="回退至上一步" />
                        <asp:Button ID="btn_cancel" runat="server" Text="取消" Width="99px" OnClick="Button2_Click" />
                    </td>
                </tr>
            </table>
        </div>



    </form>
</body>

</html>
