<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilterPageView.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Role.FilterPageView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源过滤方案的适合的列表页面视图</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <script type="text/javascript" language="javascript">

        var dbid;
        var fwdbid = "1";

        window.onload = function () {
            loadBaseJs(function () {
                $.loadCss("cssList", "pager");
                //开始加载
                $.loadListJs(function () {
                    dbid = "1," + $.cookie("ServicesDataBaseID");
                    //myLoad();
                });
            });
        };

    </script>
</head>
<body style="background:#fff">
    <form id="form1" runat="server">
     
    <asp:GridView ID="GV" CssClass="cssTable" runat="server" AutoGenerateColumns="False" 
        EnableModelValidation="True" DataKeyNames="PVID">
        <Columns>
            <asp:BoundField DataField="PVID" HeaderText="视图ID" />
            <asp:BoundField DataField="ModuleName" HeaderText="模块名称" />
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkPVID" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PVTitle" HeaderText="视图名称" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnSave" runat="server" onclick="BtnSaveClick" Text="保存数据" />
    </form>
</body>
</html>
