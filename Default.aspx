<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NatureFramework._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自然框架支撑平台</title>
    <script type="text/javascript" src="http://LCNatureResource.nature.com/Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" language="javascript">
        window.onload = function() {
            //var formData = new FormData();

            //window.location = "/CommonMainJs";

            document.getElementById("TextBox1").focus();
        };
        
        function cc(dd) {
            //dd.innerHTML("父页面控制子页面");
            $(dd).html("父页面控制子页面");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a style="padding-right:20px;" href="http://wpa.qq.com/msgrd?v=3&amp;uin=369220123&amp;site=qq&amp;menu=yes" target="_blank">
            <img style="vertical-align:bottom; border:0" src="http://wpa.qq.com/pa?p=1:369220123:13" alt="点击这里给我发消息">
        </a>

     <div>
        
    用户名：<asp:TextBox runat="server" ID="txt_User"  /><br/>
    密 码：<asp:TextBox runat="server" ID="txt_Pwd" 
             TextMode="Password"/><br/>
             
             选择要管理的项目：<nature:MyDropDownList runat="server" ID="lstProject"></nature:MyDropDownList>
    <asp:Button runat="server" ID="btn_Logon" Text="登录"  />
    </div>

     <p>
         &nbsp;</p>
     <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     <br />
     <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
     <br />
     <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
     <br />
     <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />

        <br />
        <br />
        <br />
        =====<br />
        <br />
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <asp:Button ID="btn_JM" runat="server" OnClick="btn_JM_Click" Text="解密" />
        
        <iframe src="a.htm"></iframe>
        <br />
        <br />
        <asp:Button ID="btnToObject" runat="server" OnClick="btnToObject_Click" Text="json变成实体" />
        <asp:Button ID="btnToJson" runat="server" OnClick="btnToJson_Click" Text="实体变成json" />
        <br />
        <asp:TextBox ID="txtJson" runat="server" Columns="60" Rows="10" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtJsonMsg" runat="server" Columns="60" OnTextChanged="TextBox7_TextChanged" Rows="10" TextMode="MultiLine"></asp:TextBox>
    </form>
</body>
</html>
