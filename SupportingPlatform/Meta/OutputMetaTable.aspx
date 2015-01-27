<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutputMetaTable.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Meta.OutputMetaTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>导出表、字段的元数据</title>
  <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <style type="text/css">
        .tdHide {
            display: none;
        }
    </style>
     <script type="text/javascript" language="javascript" >

         var dbid;
         var fwdbid = "1";
         var para;

         window.onload = function () {
             loadBaseJs(function () {
                 $.loadCss("cssList", "pager");
                 //开始加载
                 $.loadListJs(function () {
                     dbid = "1," + $.cookie("ServicesDataBaseID");
                     moduleLoad();
                 });
             });
         };

         function moduleLoad() {
             if (typeof (parent) != "undefined")
                 parent.spinStart();

             para = $.getUrlParameter();

             //创建表格（只有表头，没有数据）
             Nature.Page.Grid.CreateTable(para, fwdbid, function () {
                 //创建分页控件（加载分页模板）
                 Nature.Page.QuickPager.LoadPageTurn(parent.showLoginDiv, dbid, writeDebug, parent.spinStart, parent.spinStop, function () {

                     Nature.Page.QuickPager.LoadDataSource(1, undefined, function (dataSource) {
                         //绑定表格（显示数据）
                         Nature.Page.Grid.DataBind(dataSource, function () {
                             if (typeof (parent) != "undefined")
                                 parent.spinStop();
                         });
                     });
                 });

             });
         }

    </script>

</head>
<body style="background:#fff">
    <form id="form1" runat="server">
    <div>
        
        <asp:RadioButtonList ID="lstToKind" runat="server" RepeatDirection="Horizontal" Width="411px">
            <asp:ListItem Selected="True" Value="1">选择数据库</asp:ListItem>
            <asp:ListItem Value="2">自写连接字符串</asp:ListItem>
        </asp:RadioButtonList>
        <asp:DropDownList ID="lstSource" runat="server" DataTextField="txt" DataValueField="id">
        </asp:DropDownList>
&nbsp;<asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Text=" 检 查 " />
&nbsp;
        <asp:Button ID="btnStart" runat="server" Text="开始导入" OnClick="btnStart_Click" /><br />
        源数据库：<asp:GridView ID="GV_Source" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CssClass="cssTable">
            <Columns>
                <asp:BoundField DataField="TableID" HeaderText="TableID" />
                <asp:BoundField DataField="TableName" HeaderText="表名称"  />
                <asp:BoundField DataField="PKColumnID" HeaderText="主键字段ID"  />
                <asp:BoundField DataField="Content" HeaderText="说明"  />
                <asp:BoundField DataField="AddUserid" HeaderText="添加人"  />
                <asp:BoundField DataField="AddTime" HeaderText="添加时间"  />
            </Columns>
        </asp:GridView><br /><br />
        目标据库：<asp:GridView ID="GV_Target" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CssClass="cssTable">
            <Columns>
           <asp:BoundField DataField="TableID" HeaderText="TableID" />
                <asp:BoundField DataField="TableName" HeaderText="表名称"  />
                <asp:BoundField DataField="PKColumnID" HeaderText="主键字段ID"  />
                <asp:BoundField DataField="Content" HeaderText="说明"  />
                <asp:BoundField DataField="AddUserid" HeaderText="添加人"  />
                <asp:BoundField DataField="AddTime" HeaderText="添加时间"  />
            </Columns>
        </asp:GridView>
        
    </div>
        <asp:TextBox ID="txtMsg" runat="server" Columns="90" Rows="20" TextMode="MultiLine" Font-Size="12pt"></asp:TextBox>
    </form>
</body>
</html>
