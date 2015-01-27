<%@ Page validateRequest="false"  Language="C#" AutoEventWireup="true" CodeBehind="ModViewColum.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Meta.ModViewColum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>视图里的字段控件的修改</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <style type="text/css">
        div {
            clear: both;
        }
        .form {
            clear: none;
            float: left;
            padding: 3px;
        }
        fieldset {
            padding:5px;
            BORDER-RIGHT: #666 1px solid; 
            BORDER-TOP: #ddd 1px solid; 
            BORDER-LEFT: #ddd 1px solid; 
            BORDER-BOTTOM: #666 1px solid;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var fid = "";
        var myTxtID = "";
        function myCheck() {
            return CheckForm();
        }

        function jsReady() {
            loadscript.js("/Scripts/UIFunction.js", function () {
                FormFunction();
            });
            
            
        }
    </script>
</head>
<body  >
    <form id="form1" runat="server">
    <div class="form" style="width:34%">
    <fieldset title="表单信息，修改这里的信息，不会影响其他的地方"><legend style="font-size:9pt;">表单信息</legend>
        <Nature:DataForm runat="server" ID="FrmColumn"/></fieldset>
    </div>
    <div class="form"  style="width:64%">
    <fieldset title="字段基础信息，修改这里的信息，其他地方也会受到影响！"><legend style="font-size:9pt;">字段基础信息</legend> <Nature:DataForm runat="server" ID="FrmCommonForm"/></fieldset>
    </div>
    <div style="text-align:center;padding-top:20px;"><asp:Button  CssClass="btn" runat="server" ID="BtnSave" Text="保存后关闭" /> &nbsp; 
        <asp:Button CssClass="btn" runat="server" ID="BtnSaveContinue" Text="保存后继续" /> <asp:Label  runat="server" ID="LblMsg"/>
    </div>
    </form>
</body>
</html>
