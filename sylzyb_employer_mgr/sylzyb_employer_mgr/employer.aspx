<%@ Page Language="C#" EnableEventValidation="false" validateRequest="false" AutoEventWireup="true" CodeBehind="EMPLOYER.aspx.cs" Inherits="sylzyb_employer_mgr.employer_mgr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .dv_gv
        {
            text-align:center;
            margin:0 auto; 
            width:95%;
            height:300px;
            overflow:auto;
        }
        .fm{
             text-align:center;
             width:1000px;
             margin:0 auto;

        }
        .dv_banner{
            text-align:right;
            margin:0 auto;
            float:none;
            width:100%;
        }
        .dv_detail{
            width:95%;
            text-align:center;
            float:none;
            margin:0 auto;
        }
    </style>
</head>
<body>
      <form id="form1" runat="server" class="fm">
        <asp:Label ID="Label26" runat="server" Text="原料作业部员工信息管理" Font-Bold="True" Font-Size="Larger"></asp:Label> 
        <div style="text-align:right;">            
               <hr  />
        <asp:Label ID="Label25" runat="server" Text="用户名：" ></asp:Label>
             
        <asp:Label ID="login_user" runat="server" Text=""></asp:Label>
  <asp:Button ID="btn_back" runat="server" Text="退出" OnClick="btn_back_Click" />
     
            </div>
        <div style="text-align:center;margin:0 auto;width:100%;float:none;" >
    
        <asp:Label ID="Label2" runat="server" Text="员工信息总览" Font-Bold="False" Font-Size="Larger"></asp:Label>
<hr  /> 
</div>

        <div class="dv_banner">
            <table style="width:100%">
                <tr>
                    <td style="width:30%;text-align:center;">
                        <asp:Button ID="btn_emp_add" runat="server" Text="添加员工信息" OnClick="btn_emp_add_Click" />
                         <asp:Button ID="btn_emp_del" runat="server" Text="删除员工信息" OnClick="btn_emp_del_Click" />
                         <asp:Button ID="btn_emp_edt" runat="server" Text="修改员工信息" OnClick="btn_emp_edt_Click" />
                    
                    
                    
                    </td>
                    <td style="width:20%;text-align:center;">
    <asp:Label ID="Label10" runat="server" Text="班别："></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>甲班</asp:ListItem>
                            <asp:ListItem>乙班</asp:ListItem>
                            <asp:ListItem>丙班</asp:ListItem>
                            <asp:ListItem>丁班</asp:ListItem>
                            <asp:ListItem>常白</asp:ListItem>
                            <asp:ListItem>管理</asp:ListItem>
                            <asp:ListItem>其它</asp:ListItem>
                            <asp:ListItem Value="*">全部</asp:ListItem>
                        </asp:DropDownList>

                    </td>
   <td style="width:25%;text-align:center;">
                        <asp:Label ID="Label27" runat="server" Text="姓名拼音简写："></asp:Label>

                        <asp:TextBox ID="tbx_xmsy" runat="server" Width="111px" AutoPostBack="true"  OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
       </td>
          <td style="width:5%;text-align:center;">
                        <asp:Button ID="btn_info_cx" runat="server" Text="查询" />

                    </td>
                </tr>
            </table>
         

        </div>
          <div class="dv_gv">
         <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True" Font-Size="Small">
             <Columns>
                 <asp:BoundField DataField="ID" HeaderText="序号" />
                 <asp:BoundField DataField="WorkerName" HeaderText="姓名" />
                 <asp:BoundField DataField="IDCard" HeaderText="身份证" />
                 <asp:BoundField DataField="GroupName" HeaderText="班组" />
                 <asp:BoundField DataField="Job" HeaderText="岗位" />
                  <asp:BoundField DataField="Duties" HeaderText="职务" />
                 
                  <asp:BoundField DataField="WagesFactor" HeaderText="系数" />
                  <asp:BoundField DataField="DutiesFactor" HeaderText="管理奖系数" />

   
             </Columns>
         </asp:GridView>
       </div>
 
        <div id="employer_detail" runat="server" class="dv_detail">
    <asp:Label ID="Label1" runat="server" Text="员工信息编辑" Font-Bold="False" Font-Size="Larger"></asp:Label>
<hr />
            <table style="width:100%">
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label3" runat="server" Text="序号："></asp:Label>
                     </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="tbx_id" runat="server"></asp:TextBox>
                    </td>
                  
               
                    <td class="auto-style1">
                        <asp:Label ID="Label4" runat="server" Text="姓名："></asp:Label>
                     </td>  
                     <td class="auto-style1">
                        <asp:TextBox ID="tbx_WorkerName" runat="server"></asp:TextBox>
                    </td>
                    </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label5" runat="server" Text="身份证："></asp:Label>
                     </td>  
                     <td class="auto-style1">
                          <asp:TextBox ID="tbx_IDCard" runat="server"></asp:TextBox>
                    </td>
                     <td class="auto-style1">
                        <asp:Label ID="Label6" runat="server" Text="班组："></asp:Label>
                     </td>  
                     <td class="auto-style1">
                        <asp:TextBox ID="tbx_GroupName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="岗位："></asp:Label>
                     </td>
                    <td>
                        <asp:TextBox ID="tbx_Job" runat="server"></asp:TextBox>
                    </td>
                  
                     <td>
                        <asp:Label ID="Label11" runat="server" Text="职务："></asp:Label>
                     </td>  
                     <td>
                        <asp:TextBox ID="tbx_Duties" runat="server"></asp:TextBox>
                    </td>
                    </tr>
                <tr>
                     <td>
                        <asp:Label ID="Label8" runat="server" Text="系数："></asp:Label>
                     </td>  
                     <td>
                        <asp:TextBox ID="tbx_WagesFactor" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="管理奖系数："></asp:Label>
                     </td>  
                     <td>
                           <asp:TextBox ID="tbx_DutiesFactor" runat="server"></asp:TextBox>
                    </td>
                    
                </tr>
            </table>
       <table style="width:100%">
         
            <tr>
               <td colspan="3">  
            <asp:Button ID="btn_ok" runat="server" Text="确认" Width="99px" OnClick="btn_ok_Click" />
                 
            <asp:Button ID="btn_cancel" runat="server" Text="取消" Width="99px" OnClick="btn_cancel_Click" />
</td> 
      </tr>
 </table>  
        </div>
        

    </form>
</body>
</html>

