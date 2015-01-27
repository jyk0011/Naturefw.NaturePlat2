<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddModule.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Meta.AddModule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加功能节点</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <style type="text/css">
        div{
            clear: both;
        }
        #divView,#divButton,#divWindow {
            float: left;
            width: 200px;
            border:0px solid #99bbe8;
            clear: none;

        }
        #divButton ,#divWindow {
            width: 150px;
        }
        fieldset {
            padding:10px;
            margin: 10px;
            BORDER-RIGHT: #666 1px solid; 
            BORDER-TOP: #ddd 1px solid; 
            BORDER-LEFT: #ddd 1px solid; 
            BORDER-BOTTOM: #666 1px solid;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var fid = "";

        var para;
        var dbid;
        var control;
        //
        var pageKind = "form";     //表单页面

        function jsReady() {
            dbid = "1," + $.cookie("ServicesDataBaseID");
            
        }
        
        function myCheck() {
            return CheckForm();
        }

        //function myLoad() {
        //    loadBaseJs(function() {
        //        $.loadFormJs(function() {
 
        //        });
        //    });
        //}
        
        function toChangeCol(url) {
            window.location = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label runat="server" ID="LblTitle"></asp:Label>
    <div style="width:90%; padding:10px;">
        新节点作为【<asp:Label runat="server" ID="lblModuleName"></asp:Label>】 的 <Nature:MyRadioButtonList runat="server" 
            ID="lstNewModuleType" AutoPostBack="True" CellPadding="0" CellSpacing="0" 
            onselectedindexchanged="LstNewModuleTypeSelectedIndexChanged" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="2">同级节点</asp:ListItem>
            <asp:ListItem Selected="True" Value="1">子节点</asp:ListItem>
        </Nature:MyRadioButtonList> &nbsp;注意：修改选项后，会重置表单，请慎重选择！<br /><br />
    <Nature:DataForm runat="server" ID="FrmCommonForm" class="table_default1"/>
        <br />
        请选择要操作的表：<Nature:MyDropDownList runat="server" ID="lstTableID"/>&nbsp;（会根据选中的表，设置视图的属性值）</div>
    <div><div id="divView"><fieldset title="创建哪些视图？"><legend style="font-size:9pt;">选中生成哪些视图？</legend>
             <Nature:MyCheckBoxList runat="server" ID="lstView"/></fieldset></div>
        <fieldset title="选中生成哪些按钮？"><legend style="font-size:9pt;">创建哪些按钮？</legend>
        <div id="divButton">    <Nature:MyCheckBoxList runat="server" ID="lstButton"/></div>
        <div id="divWindow">
            打开窗口宽度：<Nature:MyTextBox runat="server" ID="txtWindowWidth">800</Nature:MyTextBox>
            &nbsp; <br />
            打开窗口高度：<Nature:MyTextBox runat="server" ID="txtWindowHeight">500</Nature:MyTextBox>
            &nbsp;</div>
            </fieldset>
    </div>
    <div style="text-align:center"><asp:Button  CssClass="input_01" runat="server" ID="BtnSave" Text="保存后关闭" /> &nbsp; 
        <asp:Button CssClass="input_01" runat="server" ID="BtnSaveContinue" Text="保存后继续" /> <asp:Label  runat="server" ID="LblMsg"/>
    </div>

    </form>
</body>
</html>
