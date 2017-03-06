<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employer_mgr.aspx.cs" Inherits="sylzyb_employer_mgr.employer_mgr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>
      <form id="form1" runat="server" style="text-align:center;width:95%;margin:0 auto;" >
        <asp:Label ID="Label26" runat="server" Text="原料作业部员工信息管理" Font-Bold="True" Font-Size="Larger"></asp:Label> 
        <div style="text-align:right;">            
               <hr  />
        <asp:Label ID="Label25" runat="server" Text="用户名："></asp:Label>
             
        <asp:Label ID="login_user" runat="server" Text=""></asp:Label>
  <asp:Button ID="btn_back" runat="server" Text="退出" OnClick="btn_back_Click" />
     
            </div>
        <div style="text-align:center;margin:0 auto;width:100%;float:none;" >
    
        <asp:Label ID="Label2" runat="server" Text="用户信息总览" Font-Bold="False" Font-Size="Larger"></asp:Label>
<hr  /> 
</div>

        <div style="text-align:right;margin:0 auto;float:none;width:100%;">
            <table style="width:100%">
                <tr>
                    <td style="width:50%;text-align:left;">
                        <asp:Button ID="btn_usr_add" runat="server" Text="添加用户" OnClick="btn_usr_add_Click" />
                         <asp:Button ID="btn_usr_del" runat="server" Text="删除用户" OnClick="btn_usr_del_Click" />
                         <asp:Button ID="btn_usr_edt" runat="server" Text="修改用户信息" OnClick="btn_usr_edt_Click" />
                    
                    
                    
                    </td>
                </tr>
            </table>
         

        </div>
          <div style="text-align:center;margin:0 auto;">
         <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True" Font-Size="Small">
             <Columns>
                 <asp:BoundField DataField="Id" HeaderText="序号" />
                 <asp:BoundField DataField="Name" HeaderText="姓名" />
                 <asp:BoundField DataField="sfz_id" HeaderText="身份证" />
                 <asp:BoundField DataField="class" HeaderText="班组" />
                 <asp:BoundField DataField="gangwei" HeaderText="岗位" />
                  <asp:BoundField DataField="xishu1" HeaderText="系数" />
                  <asp:BoundField DataField="xishu2_glj" HeaderText="管理奖系数" />

   
             </Columns>
         </asp:GridView>
       </div>
 
        <div id="employer_edit" runat="server" style="width:95%;text-align:center;float:none;margin:0 auto;">
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
                        <asp:TextBox ID="tbx_name" runat="server"></asp:TextBox>
                    </td>
                    </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label5" runat="server" Text="身份证："></asp:Label>
                     </td>  
                     <td class="auto-style1">
                          <asp:TextBox ID="tbx_sfz_id" runat="server"></asp:TextBox>
                    </td>
                     <td class="auto-style1">
                        <asp:Label ID="Label6" runat="server" Text="班组："></asp:Label>
                     </td>  
                     <td class="auto-style1">
                        <asp:TextBox ID="tbx_class" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="岗位："></asp:Label>
                     </td>
                    <td>
                        <asp:TextBox ID="tbx_gangwei" runat="server"></asp:TextBox>
                    </td>
                  
               
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="系数："></asp:Label>
                     </td>  
                     <td>
                        <asp:TextBox ID="tbx_xishu1" runat="server"></asp:TextBox>
                    </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="管理奖系数："></asp:Label>
                     </td>  
                     <td>
                           <asp:TextBox ID="tbx_xishu2_glj" runat="server"></asp:TextBox>
                    </td>
                     <td>
                      
                     </td>  
                     <td>
                     
                    </td>
                </tr>
            </table>
       <table style="width:100%">
         
            <tr>
               <td colspan="3">  
            <asp:Button ID="Button1" runat="server" Text="确认" Width="99px" OnClick="Button1_Click" />
                 
            <asp:Button ID="Button2" runat="server" Text="取消" Width="99px" OnClick="Button2_Click" />
</td> 
      </tr>
 </table>  
        </div>
        

    </form>
</body>
</html>

