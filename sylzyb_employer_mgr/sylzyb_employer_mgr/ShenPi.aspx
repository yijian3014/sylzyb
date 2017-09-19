<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShenPi.aspx.cs" Inherits="sylzyb_employer_mgr.ShenPi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="ShenPi.css" />
    <title>考核审批</title>
</head>
<body>
    <form runat="server">
               <div class="div_shenpi">
            <asp:Label ID="Label1" runat="server" Text="审批"  Font-Bold="False" Font-Size="Larger"></asp:Label>
            <hr />
            <table >
                <tr >
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="审批状态:"></asp:Label>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_shenpi_zt" runat="server">
                            <asp:ListItem Selected="True">同意</asp:ListItem>
                            <asp:ListItem>不同意</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td>
                        <asp:Label ID="Label27" runat="server" Text="考核总金额:"></asp:Label>
                    </td>
                    <td>
                         <asp:Label ID="lb_shenpi_kh_zhongjinger" runat="server" Text="空"></asp:Label>
                    </td>
                    
                     <td>

                        <asp:Label ID="Label24" runat="server" Text="审批模式："></asp:Label>

                    </td>
                    <td>

                        <asp:Label ID="lb_shenpi_shenpimoshi" runat="server" Text="空"></asp:Label>

                    </td>
                   
                    <td>

                        <asp:Label ID="Label61" runat="server" Text="未会签人员："></asp:Label>

                    </td>
                    <td>

                        <asp:Label ID="lb_shenpi_wei_huiqianren" runat="server" Text="空"></asp:Label>

                    </td>
                   
                </tr>
            </table>
            <table id="tb_shenpi">
                <tr>
                    <td >
                        <asp:Label ID="Label13" runat="server" Text="审批意见:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tbx_shenpi_yj"  runat="server" Height="225px" Width="100%" TextMode="MultiLine">数据如何加载</asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td>
                        <asp:CheckBox ID="cb_shenpi_qzzj" runat="server" Text="强制" /> </td>
                    </tr>
                    <tr>
                   <td>      
                   <asp:RadioButtonList ID="rbl_shenpi_nextORprevious" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_shenpi_nextORprevious_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                              <asp:ListItem>转交</asp:ListItem>
                              <asp:ListItem>回退</asp:ListItem>                            
                              <asp:ListItem>会签</asp:ListItem>
                        </asp:RadioButtonList>
</td>
                         </tr>
                    <tr>
                        <td >
                        <asp:CheckBox ID="cb_shenpi_is_huiqian" runat="server" Text="下一步允许多人会签" OnCheckedChanged="cb_shenpi_is_huiqian_CheckedChanged" AutoPostBack="True" />
                    </td>
               </tr>
                <tr>
                    <td >
                        <asp:RadioButtonList ID="rbl_shenpi_step" runat="server" OnSelectedIndexChanged="rbl_shenpi_step_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal"></asp:RadioButtonList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBoxList ID="cbl_shenpi_next_persion" runat="server" OnSelectedIndexChanged="cbl_shenpi_next_persion_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </td>
                </tr>


                <tr class="button_row">
                    <td>
                        <asp:Button ID="btn_shenpi_ok" runat="server" Text="确认并返回" Width="99px" OnClick="btn_shenpi_ok_Click" />
                        <asp:Button ID="btn_shenpi_cancel" runat="server" Text="取消并返回" Width="99px" OnClick="btn_shenpi_cancel_Click" />
                    </td>
                </tr>
                 
            </table>
        </div>
    </form>
</body>
</html>
