<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutputMeta.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Meta.OutputMeta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>导出指定模块及其相关信息的元数据</title>
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
         
         var pageKind = "list";     //列表页面

         function jsReady() {
             //开始加载
             dbid = "1," + $.cookie("ServicesDataBaseID");
             //moduleLoad();
         }

         function moduleLoad() {
             if (typeof (parent) != "undefined")
                 parent.spinStart();

             para = $.getUrlParameter();

             //创建表格（只有表头，没有数据）
             Nature.Page.Grid.CreateTable(para, fwdbid, function () {
                 //创建分页控件（加载分页模板）
                 Nature.Page.QuickPager.LoadPageTurn(parent.showLoginDiv, dbid,  parent.spinStart, parent.spinStop, function () {
                
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
                <asp:BoundField DataField="ModuleID" HeaderText="ModuleID" />
                <asp:BoundField DataField="ParentID" HeaderText="ParentID" ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="ParentIDAll" HeaderText="父节点路径" ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="ModuleName" HeaderText="模块名称"  />
                <asp:BoundField DataField="ModuleLevel" HeaderText="层数"  ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="IsHidden" HeaderText="隐藏?"  />
                <asp:BoundField DataField="IsLock" HeaderText="锁定?"  />
                <asp:BoundField DataField="DisOrder" HeaderText="排序"  ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="AddUserid" HeaderText="添加人"  />
                <asp:BoundField DataField="AddTime" HeaderText="添加时间"  />
                <asp:BoundField DataField="UpdateUserID" HeaderText="最后修改人"  />
                <asp:BoundField DataField="UpdateTime" HeaderText="最后修改时间"  />
            </Columns>
        </asp:GridView><br /><br />
        目标据库：<asp:GridView ID="GV_Target" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CssClass="cssTable">
            <Columns>
                <asp:BoundField DataField="ModuleID" HeaderText="ModuleID" />
                <asp:BoundField DataField="ParentID" HeaderText="ParentID" ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="ParentIDAll" HeaderText="父节点路径" ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="ModuleName" HeaderText="模块名称"  />
                <asp:BoundField DataField="ModuleLevel" HeaderText="层数"  ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="IsHidden" HeaderText="隐藏?"  />
                <asp:BoundField DataField="IsLock" HeaderText="锁定?"  />
                <asp:BoundField DataField="DisOrder" HeaderText="排序"  ><HeaderStyle CssClass="tdHide"></HeaderStyle><ItemStyle CssClass="tdHide"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="AddUserid" HeaderText="添加人"  />
                <asp:BoundField DataField="AddTime" HeaderText="添加时间"  />
                <asp:BoundField DataField="UpdateUserID" HeaderText="最后修改人"  />
                <asp:BoundField DataField="UpdateTime" HeaderText="最后修改时间"  />
            </Columns>
        </asp:GridView>
        
    </div>
        <asp:TextBox ID="txtMsg" runat="server" Columns="90" Rows="20" TextMode="MultiLine" Font-Size="12pt"></asp:TextBox>
    </form>
</body>
</html>
