<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NatureFramework.LeLianManage.Logins.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户登录</title>
    <style type="text/css" link=""></style>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <script type="text/javascript">
        function isLogin() {
            top.start();

            top.$("#divLoginBg",top.document).hide();
            top.$("#divLogin", top.document).hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="divLoginSSO" style="display:block;">
            
                <div >登录名称：<input id="txtUserCode" runat="server" name="userCode" type="text" class="cssTxt input_t1" /></div>
                <div >登录密码：<input id="txtUserPsw"  runat="server"  name="userPsw" type="password" class="cssTxt input_t1"  /></div>
                <div id="div_logo1"><table ><tr><td style="width:80px">选择项目：</td><td><iframe src="/SupportingPlatform/ProjectList.aspx"  allowtransparency="true" style="height: 25px;width:220px" scrolling="no" frameborder="0"  ></iframe></td></tr></table> </div>
              
                <div > &nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text=" 登 录 " />
                </div>
                <div ><input id="hdnWebappID"  name="webappID" type="hidden" value="a" /></div>
            
        </div>

        <div id="divMsg"></div>
    </form>
</body>
</html>
